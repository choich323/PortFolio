// Fill out your copyright notice in the Description page of Project Settings.

#include "Components/BoxComponent.h"
#include "Components/StaticMeshComponent.h"
#include "PlayerPawn.h"
#include "Components/ArrowComponent.h"
#include "Bullet.h"
#include "Kismet/GamePlayStatics.h"

// Sets default values
APlayerPawn::APlayerPawn()
{
 	// Set this pawn to call Tick() every frame.  You can turn this off to improve performance if you don't need it.
	PrimaryActorTick.bCanEverTick = true;

	boxComp = CreateDefaultSubobject<UBoxComponent>(TEXT("My Box Component"));
	meshComp = CreateDefaultSubobject<UStaticMeshComponent>(TEXT("My mesh Component"));

	SetRootComponent(boxComp);
	meshComp->SetupAttachment(boxComp); // box의 컴포넌트로 설정

	FVector boxSize = FVector(50.0f, 50.0f, 50.0f);
	boxComp->SetBoxExtent(boxSize);

	firePosition = CreateDefaultSubobject<UArrowComponent>(TEXT("Fire Position"));
	firePosition->SetupAttachment(boxComp);

	boxComp->SetGenerateOverlapEvents(true);
	// 충돌 응답 처리 설정
	boxComp->SetCollisionEnabled(ECollisionEnabled::QueryAndPhysics);
	// 프로젝트 세팅의 콜리전: 1번 채널(Player)로 설정
	boxComp->SetCollisionObjectType(ECC_GameTraceChannel1);

	// 모든 채널에 대한 응답을 무시로 설정
	boxComp->SetCollisionResponseToAllChannels(ECR_Ignore);
	// enemy 채널에 대해 겹침 설정
	boxComp->SetCollisionResponseToChannel(ECC_GameTraceChannel2, ECR_Overlap);
	// 이미 이 클래스 기반의 블루프린트가 있을 때 생성자의 내용을 수정하면 반영되지 않는 경우가 있음
	// 따라서 일반적으로는 생성자의 내용을 모두 작성한 후에 블루프린트를 생성한다.
	boxComp->SetCollisionResponseToChannel(ECC_WorldStatic, ECR_Block);
}

// Called when the game starts or when spawned
void APlayerPawn::BeginPlay()
{
	Super::BeginPlay();
	
}

// Called every frame
void APlayerPawn::Tick(float DeltaTime)
{
	Super::Tick(DeltaTime);
	// 방향 벡터를 만든 후
	FVector dir = FVector(0, h, v);
	// 벡터의 길이가 1이 되도록 정규화
	dir.Normalize();
	// 이동할 위치 좌표: p = p0 + vt ; (속도)v = 벡터 * 속력
	FVector newLocation = GetActorLocation() + dir * moveSpeed * DeltaTime;

	// 뒤의 true는 위치를 조정할 때 중간에 충돌이 발생하는지 확인하는 것. 특별한 지정이 없으면 false가 기본값
	// true를 통해 검사하는 것으로 매 프레임마다 CPU 연산이 추가되므로 효율은 감소한다. 꼭 필요한 것이 아니라면 최소화해야할 부분.
	SetActorLocation(newLocation, true); 
}

// Called to bind functionality to input
void APlayerPawn::SetupPlayerInputComponent(UInputComponent* PlayerInputComponent)
{
	Super::SetupPlayerInputComponent(PlayerInputComponent);

	PlayerInputComponent->BindAxis("Horizontal", this, &APlayerPawn::MoveHorizontal); // axis 이름, 연결할 함수가 있는 클래스, 연결할 함수의 주소 값
	PlayerInputComponent->BindAxis("Vertical", this, &APlayerPawn::MoveVertical);
	
	PlayerInputComponent->BindAction("Fire", IE_Pressed, this, &APlayerPawn::Fire);
}

void APlayerPawn::MoveHorizontal(float value)
{
	h = value;
}

void APlayerPawn::MoveVertical(float value)
{
	v = value;
}

void APlayerPawn::Fire()
{
	ABullet* bullet = GetWorld()->SpawnActor<ABullet>(bulletFactory, firePosition->GetComponentLocation(), firePosition->GetComponentRotation());

	UGameplayStatics::PlaySound2D(GetWorld(), fireSound);
}

