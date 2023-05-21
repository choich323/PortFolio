// Fill out your copyright notice in the Description page of Project Settings.


#include "EnemyManager.h"
#include "Enemy.h"
#include <EngineUtils.h>
#include <Kismet/GameplayStatics.h>

// Sets default values
AEnemyManager::AEnemyManager()
{
 	// Set this actor to call Tick() every frame.  You can turn this off to improve performance if you don't need it.
	// 틱 함수가 매 프레임마다 실행되지 않도록 설정(기본 값은 true)
	PrimaryActorTick.bCanEverTick = false;

}

// Called when the game starts or when spawned
void AEnemyManager::BeginPlay()
{
	Super::BeginPlay();
	
	// 랜덤 시간 생성
	float createTime = FMath::RandRange(minTime, maxTime);

	// 이런 방식을 Tick 방식보다 더 자주 쓴다.
	// 타이머 매니저에 알림 등록(시계와 알림 시간을 넘겨서 적절한 타이밍에 알림을 받는 방식)
	// (알림 시계, 알림 받을 객체(자기 자신), 알림 받을 함수, 알림 시간)

	FindSpawnPoints();

	GetWorld()->GetTimerManager().SetTimer(spawnTimerHandle, this, &AEnemyManager::CreateEnemy, createTime);
}

// Called every frame
void AEnemyManager::Tick(float DeltaTime)
{
	Super::Tick(DeltaTime);

}

void AEnemyManager::CreateEnemy()
{
	// 랜덤 위치
	int index = FMath::RandRange(0, spawnPoints.Num() - 1);
	// 적 생성 및 배치
	GetWorld()->SpawnActor<AEnemy>(enemyFactory, spawnPoints[index]->GetActorLocation(), FRotator(0));

	// 다시 타이머 설정
	float createTime = FMath::RandRange(minTime, maxTime);
	GetWorld()->GetTimerManager().SetTimer(spawnTimerHandle, this, &AEnemyManager::CreateEnemy, createTime);
}

void AEnemyManager::FindSpawnPoints()
{
	/*
	// 월드의 액터마다
	for (TActorIterator<AActor> it(GetWorld()); it; ++it) {
		AActor* spawn = *it;
		// 액터의 이름에 원하는 내용이 들어가 있으면 추가
		if (spawn->GetName().Contains(TEXT("BP_EnemySpawnPoint"))) {
			spawnPoints.Add(spawn);
		}
	}*/

	// 배열을 만들고 모든 액터 객체를 배열에 넣어서 검사를 돌리는 방식
	TArray<AActor*> allActors;
	UGameplayStatics::GetAllActorsOfClass(GetWorld(), AActor::StaticClass(), allActors);
	for (auto spawn : allActors) {
		if (spawn->GetName().Contains(TEXT("BP_EnemySpawnPoint"))) {
			spawnPoints.Add(spawn);
		}
	}
}

