// Fill out your copyright notice in the Description page of Project Settings.

#pragma once

#include "CoreMinimal.h"
#include "Components/ActorComponent.h"
#include "EnemyFSM.generated.h"

UENUM(BlueprintType)
enum class EEnemyState : uint8
{
	Idle,
	Move,
	Attack,
	Damage,
	Die,
};

UCLASS( ClassGroup=(Custom), meta=(BlueprintSpawnableComponent) )
class TPS_PROJECT_API UEnemyFSM : public UActorComponent
{
	GENERATED_BODY()

public:	
	// Sets default values for this component's properties
	UEnemyFSM();

protected:
	// Called when the game starts
	virtual void BeginPlay() override;

public:	
	// Called every frame
	virtual void TickComponent(float DeltaTime, ELevelTick TickType, FActorComponentTickFunction* ThisTickFunction) override;

public:
	// 초기 상태
	UPROPERTY(VisibleAnywhere, BlueprintReadOnly, Category = FSM)
	EEnemyState mState = EEnemyState::Idle;

	// 대기 시간
	UPROPERTY(EditDefaultsOnly, Category = FSM)
	float idleDelayTime = 1;
	// 현재 시간
	float currentTime = 0;

	UPROPERTY(VisibleAnywhere, Category = FSM)
	class ATPSPlayer* target;

	UPROPERTY()
	class AEnemy* me;

	// 공격 가능 사거리
	UPROPERTY(EditAnywhere, Category = FSM)
	float attackRange = 150.0f;

	// 공격 딜레이
	UPROPERTY(EditAnywhere, Category = FSM)
	float attackDelayTime = 2.0f;

	// 체력
	UPROPERTY(EditDefaultsOnly, BlueprintReadWrite, Category = FSM)
	int32 hp = 3;

	// 피격 대기 시간
	UPROPERTY(EditAnywhere, Category = FSM)
	float damageDelayTime = 2.0f;

	// 사망시 아래로 사라지는 속도
	UPROPERTY(EditAnywhere, Category = FSM)
	float dieSpeed = 50.0f;

	UPROPERTY()
	class UEnemyAnim* anim;

	UPROPERTY()
	class AAIController* ai;

	// 길 찾기 수행시 랜덤 위치
	FVector randomPos;

	// 랜덤 위치 가져오기: 검색 기준 위치, 검색 반경, 위치 저장 변수
	bool GetRandomPositionInNavMesh(FVector centerLocation, float radius, FVector& dest);

	void IdleState();

	void MoveState();

	void AttackState();

	void DamageState();

	void DieState();

	void OnDamageProcess();
};
