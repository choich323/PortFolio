// Fill out your copyright notice in the Description page of Project Settings.

#pragma once

#include "CoreMinimal.h"
#include "Animation/AnimInstance.h"
#include "PlayerAnim.generated.h"

/**
 * 
 */
UCLASS()
class TPS_PROJECT_API UPlayerAnim : public UAnimInstance
{
	GENERATED_BODY()
	
public:
	UPROPERTY(EditDefaultsOnly, BlueprintReadWrite, Category = PlayerAnim)
	float speed = 0;

	UPROPERTY(EditDefaultsOnly, BlueprintReadWrite, Category = PlayerAnim)
	bool isInAir = false;

	// ����� ���� �ִϸ��̼� ��Ÿ��
	UPROPERTY(EditDefaultsOnly, Category = PlayerAnim)
	class UAnimMontage* attackAnimMontage;
	// ���� �ִϸ��̼� ��� �Լ�
	void PlayAttackAnim();

	UPROPERTY(EditDefaultsOnly, BlueprintReadWrite, Category = PlayerAnim)
	float direction = 0;

	virtual void NativeUpdateAnimation(float DeltaSeconds) override;

};