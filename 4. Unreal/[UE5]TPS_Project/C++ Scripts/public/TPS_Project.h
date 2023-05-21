// Copyright Epic Games, Inc. All Rights Reserved.

#pragma once

#include "CoreMinimal.h"

DECLARE_LOG_CATEGORY_EXTERN(TPS, Log, All);

// 디버그시 로그를 표기하기 위한 매크로
#define CALLINFO (FString(__FUNCTION__) + TEXT("(") + FString::FromInt(__LINE__) + TEXT(")"))

#define PRINT_CALLINFO() UE_LOG(TPS, Warning, TEXT("%s"), *CALLINFO);

// 매크로의 줄바꿈은 \를 이용해서 표현한다.
#define PRINT_LOG(fmt, ...) UE_LOG(TPS, Warning, TEXT("%s %s"), *CALLINFO, *FString::Printf(fmt, ##__VA_ARGS__))