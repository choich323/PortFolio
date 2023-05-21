// Fill out your copyright notice in the Description page of Project Settings.


#include "PlayerAnim.h"
#include "TPSPlayer.h"
#include <GameFramework/CharacterMovementComponent.h>

void UPlayerAnim::PlayAttackAnim()
{
	Montage_Play(attackAnimMontage);
}

void UPlayerAnim::NativeUpdateAnimation(float DeltaSeconds)
{
	// 소유 폰 얻어 오기
	// 플레이어로 캐스팅
	// 이동 속도
	// 플레이어의 전방 벡터
	// speed에 값 할당
	Super::NativeUpdateAnimation(DeltaSeconds);

	auto ownerPawn = TryGetPawnOwner();
	auto player = Cast<ATPSPlayer>(ownerPawn);
	// 캐스트에 성공했으면
	if (player) {
		FVector velocity = player->GetVelocity();
		// 전후 속도 할당
		FVector forwardvector = player->GetActorForwardVector();
		speed = FVector::DotProduct(forwardvector, velocity);
		// 좌우 속도 할당
		FVector rightVector = player->GetActorRightVector();
		direction = FVector::DotProduct(rightVector, velocity);
		// 플레이어가 공중에 있는지 확인
		auto movement = player->GetCharacterMovement();
		isInAir = movement->IsFalling();
	}
}
