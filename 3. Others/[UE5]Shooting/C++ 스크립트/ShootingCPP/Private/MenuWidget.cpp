// Fill out your copyright notice in the Description page of Project Settings.


#include "MenuWidget.h"
#include "Components/Button.h"
#include "Kismet/GameplayStatics.h"
#include "Kismet/KismetSystemLibrary.h"

void UMenuWidget::Restart()
{
	// 배치할 월드에 지정 맵을 다시 로드한다.
	UGameplayStatics::OpenLevel(GetWorld(), "ShootingMap");
}

void UMenuWidget::Quit()
{
	UWorld* currentWorld = GetWorld();
	// 실행중인 월드, 플레이어, 종료 방식, 플랫폼 제한
	UKismetSystemLibrary::QuitGame(currentWorld, currentWorld->GetFirstPlayerController(), EQuitPreference::Quit, false);
}

// 액터 클래스를 상속한 것이 아니라 beginPlay를 가져올 수 없다. 대신 위젯 클래스의 비슷한 함수를 가져오는 것
void UMenuWidget::NativeConstruct()
{
	Super::NativeConstruct();
	// 델리게이트 연결
	button_Restart->OnClicked.AddDynamic(this, &UMenuWidget::Restart);
	button_Quit->OnClicked.AddDynamic(this, &UMenuWidget::Quit);
}
