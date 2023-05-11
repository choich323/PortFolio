// Fill out your copyright notice in the Description page of Project Settings.


#include "Bullet.h"
#include "Components/BoxComponent.h"
#include "Components/StaticMeshComponent.h"
#include "Components/ArrowComponent.h"
#include "EnemyActor.h"
#include "Kismet/GameplayStatics.h"
#include "ShootingGameModeBase.h"
// Sets default values
ABullet::ABullet()
{
 	// Set this actor to call Tick() every frame.  You can turn this off to improve performance if you don't need it.
	PrimaryActorTick.bCanEverTick = true;

	boxComp = CreateDefaultSubobject<UBoxComponent>(TEXT("Box Collider"));
	SetRootComponent(boxComp);
	boxComp->SetBoxExtent(FVector(50.0f, 50.0f, 50.0f)); // box의 크기

	// 박스 컴포넌트의 크기를 배율 단위로 조정
	boxComp->SetWorldScale3D(FVector(0.75f, 0.25f, 1.0f));

	meshComp = CreateDefaultSubobject<UStaticMeshComponent>(TEXT("Static Mesh Component"));
	meshComp->SetupAttachment(boxComp);

	boxComp->SetCollisionProfileName(TEXT("Bullet"));
}

// Called when the game starts or when spawned
void ABullet::BeginPlay()
{
	Super::BeginPlay();
	// 델리게이트 호출: adddynamic을 통해 연결할 클래스, 연결할 함수를 지정한다.
	boxComp->OnComponentBeginOverlap.AddDynamic(this, &ABullet::OnBulletOverlap);
}

// Called every frame
void ABullet::Tick(float DeltaTime)
{
	Super::Tick(DeltaTime);

	FVector newLocation = GetActorLocation() + GetActorForwardVector() * moveSpeed * DeltaTime;

	SetActorLocation(newLocation);
}

void ABullet::OnBulletOverlap(UPrimitiveComponent* OverlappedComponent, AActor* OtherActor, UPrimitiveComponent* OtherComp, int32 OtherBodyIndex, bool bFromSweep, const FHitResult& SweepResult)
{
	// 충돌한 오브젝트를 변환(cast)해본다. 오브젝트가 EnemyActor 클래스면 변환에 성공해서 enemy 안에 OtherActor의 주소 값이 들어 있게 된다.
	// 변환에 실패하면 nullptr만이 남는다.
	AEnemyActor* enemy = Cast<AEnemyActor>(OtherActor);

	if (enemy != nullptr) { // 변환에 성공했다면 그 오브젝트를 제거
		OtherActor->Destroy();
		// 생성할 월드, 파티클 파일, 생성 위치, 생성할 회전 값
		UGameplayStatics::SpawnEmitterAtLocation(GetWorld(), explosionFX, GetActorLocation(), GetActorRotation());
		// 현재 게임 모드 호출
		AGameModeBase* currentMode = GetWorld()->GetAuthGameMode();
		// shooting game mode base로 변환
		// 단순히 이 게임이 shooting 게임에만 한정되면 상관 없겠지만, 확장의 가능성을 고려해서 코드를 짜야한다.
		// 다른 게임 모드가 있을 경우, 혹은 나중에 게임 모드를 추가할 수 있기 때문에 코드를 갈아 엎지 않도록 이렇게 작성하는 것.
		AShootingGameModeBase* currentGameModeBase = Cast<AShootingGameModeBase>(currentMode);

		if (currentGameModeBase != nullptr) { // cast 실패시 nullptr이므로
			currentGameModeBase->AddScore(1);
		}
	}

	// 그 이후 자기 자신(총알)도 제거
	Destroy();
}