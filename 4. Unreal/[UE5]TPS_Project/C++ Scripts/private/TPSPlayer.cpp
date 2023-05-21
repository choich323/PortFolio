// Fill out your copyright notice in the Description page of Project Settings.


#include "TPSPlayer.h"
#include <GameFramework/SpringArmComponent.h>
#include <Camera/CameraComponent.h>
#include "Bullet.h"
#include <Blueprint/UserWidget.h>
#include <Kismet/GameplayStatics.h>
#include "EnemyFSM.h"
#include <GameFramework/CharacterMovementComponent.h>
#include "PlayerAnim.h"

// Sets default values
ATPSPlayer::ATPSPlayer()
{
 	// Set this character to call Tick() every frame.  You can turn this off to improve performance if you don't need it.
	PrimaryActorTick.bCanEverTick = true;
	// 스켈레탈 메시 설정
	ConstructorHelpers::FObjectFinder<USkeletalMesh> TempMesh(TEXT("/Script/Engine.SkeletalMesh'/Game/AnimStarterPack/UE4_Mannequin/Mesh/SK_Mannequin.SK_Mannequin'"));
	if (TempMesh.Succeeded()) {
		GetMesh()->SetSkeletalMesh(TempMesh.Object);
		// FRotator: Pitch - Y, Yaw - Z, Roll - X
		GetMesh()->SetRelativeLocationAndRotation(FVector(0, 0, -90), FRotator(0, -90, 0));
	}

	// TPS 카메라 암 붙이기
	springArmComp = CreateDefaultSubobject<USpringArmComponent>(TEXT("SpringArm"));
	springArmComp->SetupAttachment(RootComponent);
	// 플레이어 기준 카메라의 위치
	springArmComp->SetRelativeLocation(FVector(0, 70, 90));
	// 타겟과 카메라 사이에 충돌이 없을 경우 타겟과의 지정 거리 설정
	springArmComp->TargetArmLength = 400;
	// 플레이어가 회전하면 플레이어를 축으로 스프링 암도 회전
	springArmComp->bUsePawnControlRotation = true;

	tpsCamComp = CreateDefaultSubobject<UCameraComponent>(TEXT("TpsCamComp"));
	tpsCamComp->SetupAttachment(springArmComp);
	// 플레이어가 회전하고 스프링암이 회전할 때 카메라가 스프링암을 축으로 회전하지 않고 그냥 스프링 암을 따라감
	tpsCamComp->bUsePawnControlRotation = false;
	// 플레이어의 회전 허용
	bUseControllerRotationYaw = true;
	// 최대 점프 횟수
	JumpMaxCount = 2;

	// 유탄 총의 스켈레탈메시 컴포넌트 등록
	gunMeshComp = CreateDefaultSubobject<USkeletalMeshComponent>(TEXT("GunMeshComp"));
	// 부모 컴포넌트를 mesh컴포넌트로 설정
	gunMeshComp->SetupAttachment(GetMesh(), TEXT("hand_rSocket"));
	// 스켈레탈메시 데이터 로드
	ConstructorHelpers::FObjectFinder<USkeletalMesh> TempGunMesh(TEXT("/Script/Engine.SkeletalMesh'/Game/FPWeapon/Mesh/SK_FPGun.SK_FPGun'"));
	if (TempGunMesh.Succeeded()) {
		gunMeshComp->SetSkeletalMesh(TempGunMesh.Object);
		gunMeshComp->SetRelativeLocation(FVector(-17, 10, -3));
		gunMeshComp->SetRelativeRotation(FRotator(0, 90, 0));
	}
	
	// 스나이퍼 컴포넌트 등록
	sniperGunComp = CreateDefaultSubobject<UStaticMeshComponent>(TEXT("SniperGunComp"));
	sniperGunComp->SetupAttachment(GetMesh(), TEXT("hand_rSocket"));
	// 메시 데이터 로드
	ConstructorHelpers::FObjectFinder<UStaticMesh> TempSniperMesh(TEXT("/Script/Engine.StaticMesh'/Game/SniperGun/sniper1.sniper1'"));
	if (TempSniperMesh.Succeeded()) {
		// 메시 할당, 위치, 크기 조정
		sniperGunComp->SetStaticMesh(TempSniperMesh.Object);
		sniperGunComp->SetRelativeLocation(FVector(-42, 7, 1));
		sniperGunComp->SetRelativeRotation(FRotator(0, 90, 0));
		sniperGunComp->SetRelativeScale3D(FVector(0.15f));
	}

	ConstructorHelpers::FObjectFinder<USoundBase> tempSound(TEXT("/Script/Engine.SoundWave'/Game/SniperGun/Rifle.Rifle'"));
	if (tempSound.Succeeded()) {
		bulletSound = tempSound.Object;
	}

}

// Called when the game starts or when spawned
void ATPSPlayer::BeginPlay()
{
	Super::BeginPlay();

	GetCharacterMovement()->MaxWalkSpeed = walkSpeed;

	// 스나이퍼 UI 위젯 인스턴스 생성
	_sniperUI = CreateWidget(GetWorld(), sniperUIFactory);
	// 일반 조준 UI 인스턴스 생성
	_crosshairUI = CreateWidget(GetWorld(), crosshairUIFactory);
	// UI등록(기본 상태이므로)
	_crosshairUI->AddToViewport();

	// 기본으로 스나이퍼 총 사용하도록
	ChangeToSniperGun();
}

// Called every frame
void ATPSPlayer::Tick(float DeltaTime)
{
	Super::Tick(DeltaTime);
	
	Move();
}

// Called to bind functionality to input
void ATPSPlayer::SetupPlayerInputComponent(UInputComponent* PlayerInputComponent)
{
	Super::SetupPlayerInputComponent(PlayerInputComponent);

	// 시야 조종
	PlayerInputComponent->BindAxis(TEXT("Turn"), this, &ATPSPlayer::Turn);
	PlayerInputComponent->BindAxis(TEXT("LookUp"), this, &ATPSPlayer::LookUp);
	// 위치 조종
	PlayerInputComponent->BindAxis(TEXT("Horizontal"), this, &ATPSPlayer::InputHorizontal);
	PlayerInputComponent->BindAxis(TEXT("Vertical"), this, &ATPSPlayer::InputVertical);
	// 점프
	PlayerInputComponent->BindAction(TEXT("Jump"), IE_Pressed, this, &ATPSPlayer::InputJump);
	// 사격
	PlayerInputComponent->BindAction(TEXT("Fire"), IE_Pressed, this, &ATPSPlayer::InputFire);
	// 총 종류 변경
	PlayerInputComponent->BindAction(TEXT("Grenade Gun"), IE_Pressed, this, &ATPSPlayer::ChangeToGrenadeGun);
	PlayerInputComponent->BindAction(TEXT("Sniper Gun"), IE_Pressed, this, &ATPSPlayer::ChangeToSniperGun);
	// 스나이퍼 조준
	PlayerInputComponent->BindAction(TEXT("SniperMode"), IE_Pressed, this, &ATPSPlayer::SniperAim);
	PlayerInputComponent->BindAction(TEXT("SniperMode"), IE_Released, this, &ATPSPlayer::SniperAim);
	// 달리기 입력 이벤트 처리 함수 바인딩
	PlayerInputComponent->BindAction(TEXT("Run"), IE_Pressed, this, &ATPSPlayer::InputRun);
	PlayerInputComponent->BindAction(TEXT("Run"), IE_Released, this, &ATPSPlayer::InputRun);
}

void ATPSPlayer::InputRun()
{
	auto movement = GetCharacterMovement();
	// 현재 달리기 모드라면
	if (movement->MaxWalkSpeed > walkSpeed) {
		movement->MaxWalkSpeed = walkSpeed;
	}
	else { // 걷기 모드라면
		movement->MaxWalkSpeed = runSpeed;
	}
}

void ATPSPlayer::Turn(float value)
{
	AddControllerYawInput(value);
}

void ATPSPlayer::LookUp(float value)
{
	AddControllerPitchInput(value);
}

void ATPSPlayer::InputHorizontal(float value)
{
	// 언리얼 뷰포트에서는 좌우가 Y축
	direction.Y = value;
}

void ATPSPlayer::InputVertical(float value)
{
	// 언리얼 뷰포트에서는 전후가 X축
	direction.X = value;
}

void ATPSPlayer::InputJump()
{
	Jump();
}

void ATPSPlayer::Move()
{
	// 절대 좌표를 상대 좌표로 바꾸기 위해 TransformVector 함수를 사용.
	// TransformVector함수는 특정한 벡터를 자신의 기준으로 변환시켜준다.
	direction = FTransform(GetControlRotation()).TransformVector(direction);
	//SetActorLocation(GetActorLocation() + direction * walkSpeed * DeltaTime);
	// 위의 방식을 써도 되지만, CharacterMovement 컴포넌트에 들어있는 함수를 사용하는 것이 더 편리하다.
	// 해당 컴포넌트에 방향만 넣어주면 알아서 계산해서 캐릭터가 이동한다. 점프 역시 동일한 원리
	AddMovementInput(direction);
	direction = FVector::ZeroVector;
}

void ATPSPlayer::InputFire()
{
	// 총소리 재생
	UGameplayStatics::PlaySound2D(GetWorld(), bulletSound);

	// 카메라 셰이크로 총 반동 주기
	auto controller = GetWorld()->GetFirstPlayerController();
	controller->PlayerCameraManager->StartCameraShake(cameraShake);

	// 공격 몽타주 재생
	auto anim = Cast<UPlayerAnim>(GetMesh()->GetAnimInstance());
	anim->PlayAttackAnim();

	// 유탄총 장착 중이면
	if (bUsingGrenadeGun) {
		FTransform firePosition = gunMeshComp->GetSocketTransform(TEXT("FirePosition"));
		GetWorld()->SpawnActor<ABullet>(bulletFactory, firePosition);
	}
	// 스나이퍼 총 장착 중이면
	else {
		// LineTrace의 시작 위치
		FVector startPos = tpsCamComp->GetComponentLocation();
		// LineTrace의 종료 위치
		FVector endPos = tpsCamComp->GetComponentLocation() + tpsCamComp->GetForwardVector() * 5000;
		// LineTrace의 충돌 정보를 담을 변수
		FHitResult hitInfo;
		// 충돌 옵션 설정 변수
		FCollisionQueryParams params;
		// 자기 자신(플레이어)는 충돌에서 제외
		params.AddIgnoredActor(this);
		// Channel 필터를 이용한 LineTrace 충돌 검출
		bool bHit = GetWorld()->LineTraceSingleByChannel(hitInfo, startPos, endPos, ECC_Visibility, params);
		// 충돌했을 때 > 그 위치에 효과를 발생시켜야 함
		if (bHit) {
			// 부딪힌 위치
			FTransform bulletTrans;
			// 위치 할당
			bulletTrans.SetLocation(hitInfo.ImpactPoint);
			// 총알 파편 효과 인스턴스 생성(월드에 이펙트를 transformation에 생성)
			UGameplayStatics::SpawnEmitterAtLocation(GetWorld(), bulletEffectFactory, bulletTrans);
			
			// 부딪힌 물체의 컴포넌트
			auto hitComp = hitInfo.GetComponent();
			// 컴포넌트에 물리 처리가 되어 있으면
			if (hitComp && hitComp->IsSimulatingPhysics()) {
				// 부딪힌 지점의 면의 노멀 벡터를 활용, 물체의 무게에 맞게 조정.
				FVector force = -1 * hitInfo.ImpactNormal * hitComp->GetMass() * 500000;
				hitComp->AddForce(force);
			}

			// 부딪힌 대상이 적인지 확인: 에네미의 생성자에서 TEXT-"FSM"으로 생성된 컴포넌트를 찾아서 반환.
			auto enemy = hitInfo.GetActor()->GetDefaultSubobjectByName(TEXT("FSM"));
			if (enemy) {
				auto enemyFSM = Cast<UEnemyFSM>(enemy);
				enemyFSM->OnDamageProcess();
			}
		}
	}
}

void ATPSPlayer::ChangeToGrenadeGun()
{
	// 스나이퍼 조준 중에 총을 바꾸는 경우에 조준 해제를 먼저 적용
	SniperRelease();

	bUsingGrenadeGun = true;
	sniperGunComp->SetVisibility(false);
	gunMeshComp->SetVisibility(true);
}

void ATPSPlayer::ChangeToSniperGun()
{
	bUsingGrenadeGun = false;
	sniperGunComp->SetVisibility(true);
	gunMeshComp->SetVisibility(false);
}

void ATPSPlayer::SniperAim()
{
	if (bUsingGrenadeGun)
		return;
	if (!bSniperAim) {
		// 조준 상태로 변경, 뷰포트에 출력, 시야각 설정
		bSniperAim = true;
		_sniperUI->AddToViewport();
		tpsCamComp->SetFieldOfView(45.0f);
		_crosshairUI->RemoveFromParent(); // Parent()로 제거해야지 '뷰포트에서 제거'를 하면 안된다.
	}
	else {
		bSniperAim = false;
		_sniperUI->RemoveFromParent();
		tpsCamComp->SetFieldOfView(90.0f);
		_crosshairUI->AddToViewport();
	}
}

void ATPSPlayer::SniperRelease()
{
	// 유탄총, 비조준 중이면 실행하지 않음
	if (bUsingGrenadeGun || !bSniperAim)
		return;
	if(bSniperAim) {
		bSniperAim = false;
		_sniperUI->RemoveFromParent();
		tpsCamComp->SetFieldOfView(90.0f);
		_crosshairUI->AddToViewport();
	}
}
