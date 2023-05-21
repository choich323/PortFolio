// Fill out your copyright notice in the Description page of Project Settings.


#include "ShootingGameModeBase.h"
#include "Blueprint/UserWidget.h"
#include "MainWidget.h"
#include "Components/TextBlock.h" // 이 헤더가 없으면 불완전한 클래스 형식으로 인식해서 에러 발생. 비슷한 상황이 날 경우 적절한 헤더를 찾아서 추가해줘야 한다.
#include "MenuWidget.h"
#include "Kismet/GameplayStatics.h"
#include "SaveData.h"
void AShootingGameModeBase::AddScore(int32 point)
{
	currentScore += point;
	PrintScore();
}

void AShootingGameModeBase::ShowMenu()
{
	if (menuWidget != nullptr) {
		menuUI = CreateWidget<UMenuWidget>(GetWorld(), menuWidget);
		if (menuUI != nullptr) {
			// 메뉴 활성화
			menuUI->AddToViewport();

			// 게임 일시 정지
			UGameplayStatics::SetGamePaused(GetWorld(), true);

			// 마우스 커서 on
			GetWorld()->GetFirstPlayerController()->SetShowMouseCursor(true);
		}
	}
}

void AShootingGameModeBase::BeginPlay()
{
	Super::BeginPlay(); // 부모클래스의 함수를 먼저 실행한다.

	LoadScoreData();

	if (mainWidget != nullptr) {
		mainUI = CreateWidget<UMainWidget>(GetWorld(), mainWidget);
		// 메모리에 제대로 로드 되었다면 뷰포트에 출력
		if (mainUI != nullptr) {
			mainUI->AddToViewport();
			mainUI->bestScoreData->SetText(FText::AsNumber(bestScore));
		}
	}
	PrintScore();
}

void AShootingGameModeBase::PrintScore()
{
	if (mainUI != nullptr) {
		mainUI->scoreData->SetText(FText::AsNumber(currentScore));
		if (currentScore >= bestScore) {
			bestScore = currentScore;
			mainUI->bestScoreData->SetText(FText::AsNumber(bestScore));
		}
	}
}

void AShootingGameModeBase::SaveScoreData()
{
	USaveData* saveInstance = Cast<USaveData>(UGameplayStatics::CreateSaveGameObject(USaveData::StaticClass()));
	if (saveInstance) {
		if (bestScore >= preBestScore || bestScore != 0) {
			saveInstance->scoreData = bestScore;
			UGameplayStatics::SaveGameToSlot(saveInstance, TEXT("BestScoreData"), 0);
		}
	}
}

void AShootingGameModeBase::LoadScoreData()
{
	if (!UGameplayStatics::DoesSaveGameExist(TEXT("BestScoreData"), 0))
		return;
	USaveData* loadInstance = Cast<USaveData>(UGameplayStatics::LoadGameFromSlot(TEXT("BestScoreData"), 0));

	if (loadInstance) {
		preBestScore = loadInstance->scoreData;
		bestScore = preBestScore;
	}
}
