// Fill out your copyright notice in the Description page of Project Settings.

#pragma once

#include "CoreMinimal.h"
#include "Blueprint/UserWidget.h"
#include "MainWidget.generated.h"

/**
 * 
 */
UCLASS()
class SHOOTINGCPP_API UMainWidget : public UUserWidget
{
	GENERATED_BODY()
	
public:
	// meta는 메타데이터 지정자라는 옵션. 언리얼 에디터에 대한 제어를 할 때 사용
	// 아래의 경우 위젯 변수를 실제 블루프린트와 연동하는 역할
	// 이 지정자가 있는 변수는 클래스를 상속한 위젯 블루프린트에서 반드시 구현해야 한다.(안하면 컴파일 에러) + 코드에서 변경된 것이 블루프린트에 반영되는 역할도 함
	UPROPERTY(EditAnywhere, meta = (BindWidget))
	class UTextBlock* scoreText;

	UPROPERTY(EditAnywhere, meta = (BindWidget))
	class UTextBlock* scoreData;

	UPROPERTY(EditAnywhere, meta = (BindWidget))
	class UTextBlock* bestScoreText;

	UPROPERTY(EditAnywhere, meta = (BindWidget))
	class UTextBlock* bestScoreData;
};
