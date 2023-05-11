// Fill out your copyright notice in the Description page of Project Settings.

#pragma once

#include "CoreMinimal.h"
#include "GameFramework/SaveGame.h"
#include "Kismet/GameplayStatics.h"
#include "SaveData.generated.h"
/**
 * 
 */
UCLASS()
class SHOOTINGCPP_API USaveData : public USaveGame
{
	GENERATED_BODY()
	
public:
    UPROPERTY(VisibleAnywhere, Category = Basic)
    int32 scoreData;
};
