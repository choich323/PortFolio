// Fill out your copyright notice in the Description page of Project Settings.


#include "EnemyActor.h"
#include "Components/BoxComponent.h"
#include "Components/StaticMeshComponent.h"
#include "EngineUtils.h"
#include "PlayerPawn.h"
#include "Kismet/GameplayStatics.h"
#include "ShootingGameModeBase.h"
#include "Kismet/KismetMathLibrary.h"
#include "SaveData.h"
// Sets default values
AEnemyActor::AEnemyActor()
{
 	// Set this actor to call Tick() every frame.  You can turn this off to improve performance if you don't need it.
	PrimaryActorTick.bCanEverTick = true;

	boxComp = CreateDefaultSubobject<UBoxComponent>(TEXT("Box Collider"));
	SetRootComponent(boxComp);
	boxComp->SetBoxExtent(FVector(50.0f, 50.0f, 50.0f));

	meshComp = CreateDefaultSubobject<UStaticMeshComponent>(TEXT("Static mesh"));
	meshComp->SetupAttachment(boxComp);
	// 충돌 프리셋 설정을 Enemy 프리셋으로 설정
	boxComp->SetCollisionProfileName(TEXT("Enemy"));
}

// Called when the game starts or when spawned
void AEnemyActor::BeginPlay()
{
	Super::BeginPlay();
	
	// 블루 프린트의 생성 시점에서 1~100 사이의 랜덤 값을 추출한다.
	int32 drawResult = FMath::RandRange(1, 100);

	if (drawResult <= traceRate) { // 기준 확률 변수보다 작다면
		// 월드 공간에서 APlayerPawn 클래스로 된 액터를 모두 검색한다.
		for (TActorIterator<APlayerPawn> player(GetWorld()); player; ++player) { // ++player로 작성하면 에러가 발생한다. 주의!
			if (player->GetName().Contains(TEXT("BP_PlayerPawn"))) {
				dir = player->GetActorLocation() - GetActorLocation(); // 플레이어의 위치 - 적의 위치
				dir.Normalize(); // 정규화

				SetActorRotation(UKismetMathLibrary::MakeRotFromXZ(dir, GetActorUpVector()));
			}
		}
	}
	else {
		dir = GetActorForwardVector(); // ForwardVector는 길이가 1인 단위 벡터이므로 정규화 불필요
	}

	boxComp->OnComponentBeginOverlap.AddDynamic(this, &AEnemyActor::OnEnemyOverlap);
}

// Called every frame
void AEnemyActor::Tick(float DeltaTime)
{
	Super::Tick(DeltaTime);

	SetActorLocation(GetActorLocation() + moveSpeed * dir * DeltaTime);
}

void AEnemyActor::OnEnemyOverlap(UPrimitiveComponent* OverlappedComponent, AActor* OtherActor, UPrimitiveComponent* OtherComp, int32 OtherBodyIndex, bool bFromSweep, const FHitResult& SweepResult)
{
	APlayerPawn* player = Cast<APlayerPawn>(OtherActor);

	if (player != nullptr) {
		UGameplayStatics::SpawnEmitterAtLocation(GetWorld(), explosionFX, GetActorLocation(), GetActorRotation());
		UGameplayStatics::SpawnEmitterAtLocation(GetWorld(), explosionFX, OtherActor->GetActorLocation(), OtherActor->GetActorRotation());
		OtherActor->Destroy();
		
		AShootingGameModeBase* currentGameMode = Cast<AShootingGameModeBase>(GetWorld()->GetAuthGameMode());

		if (currentGameMode != nullptr) {
			currentGameMode->SaveScoreData();
			currentGameMode->ShowMenu();
		}
	}
	Destroy();
}