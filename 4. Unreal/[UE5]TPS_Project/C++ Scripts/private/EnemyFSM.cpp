// Fill out your copyright notice in the Description page of Project Settings.


#include "EnemyFSM.h"
#include "TPSPlayer.h"
#include "Enemy.h"
#include <Kismet/GameplayStatics.h>
#include "TPS_Project.h"
#include <Components/CapsuleComponent.h>
#include "EnemyAnim.h"
#include <AIController.h>
#include <NavigationSystem.h>

// Sets default values for this component's properties
UEnemyFSM::UEnemyFSM()
{
	// Set this component to be initialized when the game starts, and to be ticked every frame.  You can turn these features
	// off to improve performance if you don't need them.
	PrimaryComponentTick.bCanEverTick = true;

	// ...
}


// Called when the game starts
void UEnemyFSM::BeginPlay()
{
	Super::BeginPlay();
	
	// 월드에서 타겟(플레이어) 가져오기
	auto actor = UGameplayStatics::GetActorOfClass(GetWorld(), ATPSPlayer::StaticClass());
	// 플레이어 클래스 형변환
	target = Cast<ATPSPlayer>(actor);

	// 소유 객체 가져오기: 이 클래스를 가지고 있는 객체(enemy)
	me = Cast<AEnemy>(GetOwner());

	// UEnemyAnim* 할당
	anim = Cast<UEnemyAnim>(me->GetMesh()->GetAnimInstance());

	// AAIController 할당
	ai = Cast<AAIController>(me->GetController());
}


// Called every frame
void UEnemyFSM::TickComponent(float DeltaTime, ELevelTick TickType, FActorComponentTickFunction* ThisTickFunction)
{
	Super::TickComponent(DeltaTime, TickType, ThisTickFunction);

	switch (mState) {
	case EEnemyState::Idle:
		IdleState();
		break;
	case EEnemyState::Move:
		MoveState();
		break;
	case EEnemyState::Attack:
		AttackState();
		break;
	case EEnemyState::Damage:
		DamageState();
		break;
	case EEnemyState::Die:
		DieState();
		break;
	}
}

// 대상을 찾지 못할 경우 랜덤 위치를 패트롤 하도록 랜덤 위치 가져오는 함수
bool UEnemyFSM::GetRandomPositionInNavMesh(FVector centerLocation, float radius, FVector& dest)
{
	auto ns = UNavigationSystemV1::GetNavigationSystem(GetWorld());
	FNavLocation loc;
	// 정상적인 결과면 true, 아니면 false
	bool result = ns->GetRandomReachablePointInRadius(centerLocation, radius, loc);
	dest = loc.Location;
	return result;
}

void UEnemyFSM::IdleState()
{
	// 일정 시간 대기 후 이동 상태로 전환
	currentTime += GetWorld()->DeltaTimeSeconds;
	if (currentTime > idleDelayTime) {
		mState = EEnemyState::Move;
		currentTime = 0;
		
		// 랜덤 위치 정하기
		GetRandomPositionInNavMesh(me->GetActorLocation(), 500, randomPos);

		// 애니메이션 상태 동기화
		anim->animState = mState;
	}
}

void UEnemyFSM::MoveState()
{
	// 목적지(타깃;플레이어 위치)
	FVector destination = target->GetActorLocation();
	// 현재 내 위치(적의 위치)
	FVector dir = destination - me->GetActorLocation();
	// 아래 코드를 남겨 놓으면 랜덤 위치 이동이 안된다
	//me->AddMovementInput(dir.GetSafeNormal());

	// NavigateSystem 객체 얻어오기
	auto ns = UNavigationSystemV1::GetNavigationSystem(GetWorld());

	// 목적지 길 찾기 경로 데이터 검색
	FPathFindingQuery query;
	FAIMoveRequest req;
	
	// 목적지에서 인지할 수 있는 범위
	req.SetAcceptanceRadius(3); // 도착지에서 3cm 근방이면 이동 가능으로 판단
	req.SetGoalLocation(destination); // 플레이어를 찾는다

	// 길 찾기를 위한 쿼리 생성
	ai->BuildPathfindingQuery(req, query);
	// 길 찾기 결과 가져오기
	FPathFindingResult r = ns->FindPathSync(query);

	// 길 찾기에 성공했을 경우(플레이어가 있는 경우)
	if (r.Result == ENavigationQueryResult::Success) {
		ai->MoveToLocation(destination);
	}
	// 실패한 경우
	else {
		// 랜덤 위치로 이동
		auto result = ai->MoveToLocation(randomPos);
		// 도달하면
		if (result == EPathFollowingRequestResult::AlreadyAtGoal) {
			// 책은 새로운 위치를 할당해주라고 되어있지만 패트롤 느낌을 내기 위해 대기 상태로 전환
			mState = EEnemyState::Idle;
			currentTime = 0;
			anim->animState = mState;
		}
	}

	// 가까워지면 공격 상태로 전환
	if (dir.Size() < attackRange) {
		// 길 찾기 기능 정지
		ai->StopMovement();

		mState = EEnemyState::Attack;

		anim->animState = mState;
		anim->bAttackPlay = true;

		// 공격 상태 전환 시 대기 시간이 바로 끝나도록 처리
		currentTime = attackDelayTime;
	}
}

void UEnemyFSM::AttackState()
{
	// 시간이 흐르면
	currentTime += GetWorld()->DeltaTimeSeconds;
	if (currentTime > attackDelayTime) {
		// 공격
		currentTime = 0;
		anim->bAttackPlay = true;
	}
	// 거리 계산
	float distance = FVector::Distance(target->GetActorLocation(), me->GetActorLocation());
	// 거리가 멀어지면 이동 상태로 전환
	if (distance > attackRange) {
		mState = EEnemyState::Move;

		anim->animState = mState;

		GetRandomPositionInNavMesh(me->GetActorLocation(), 500, randomPos);
	}
}

void UEnemyFSM::DamageState()
{
	currentTime += GetWorld()->DeltaTimeSeconds;
	if (currentTime > damageDelayTime) {
		mState = EEnemyState::Idle;
		currentTime = 0.2f;

		anim->animState = mState;
	}
}

void UEnemyFSM::DieState()
{
	// 아직 죽음 애니메이션이 다 재생되지 않았다면
	if (anim->bDieDone == false) {
		return;
	}

	// 등속으로 하강
	me->SetActorLocation(me->GetActorLocation() + dieSpeed * FVector::DownVector * GetWorld()->DeltaTimeSeconds);

	// 2미터 이상 아래로 내려가면 제거
	if (me->GetActorLocation().Z < -200.0f) {
		me->Destroy();
	}
}

void UEnemyFSM::OnDamageProcess()
{
	hp--;
	ai->StopMovement();
	if (hp > 0) {
		mState = EEnemyState::Damage;

		currentTime = 0;

		// 피격 애니메이션 재생
		// 몽타주 섹션 2개 중 하나를 랜덤으로 재생
		int32 index = FMath::RandRange(0, 1);
		FString sectionName = FString::Printf(TEXT("Damage%d"), index);
		anim->PlayDamageAnim(FName(*sectionName)); // 스트링을 형변환해서 전달
	}
	else {
		mState = EEnemyState::Die;
		// 캡슐 충돌체 비활성화
		me->GetCapsuleComponent()->SetCollisionEnabled(ECollisionEnabled::NoCollision);
		// 죽음 애니메이션 재생(섹션의 이름을 매개변수로 넣는다)
		anim->PlayDamageAnim(TEXT("Die"));
	}
	// 애니메이션 상태 동기화
	anim->animState = mState;
}

