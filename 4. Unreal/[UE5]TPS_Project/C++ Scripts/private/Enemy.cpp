// Fill out your copyright notice in the Description page of Project Settings.


#include "Enemy.h"
#include "EnemyFSM.h"
#include "GameFramework/CharacterMovementComponent.h"

// Sets default values
AEnemy::AEnemy()
{
 	// Set this character to call Tick() every frame.  You can turn this off to improve performance if you don't need it.
	PrimaryActorTick.bCanEverTick = true;
	
	// 스켈레탈 메시 데이터 로드
	ConstructorHelpers::FObjectFinder<USkeletalMesh> tempMesh(TEXT(" / Script / Engine.SkeletalMesh'/Game/Enemy/Model/vampire_a_lusth.vampire_a_lusth'"));

	if (tempMesh.Succeeded()) {
		// 데이터 할당 및 기본 설정
		GetMesh()->SetSkeletalMesh(tempMesh.Object);
		GetMesh()->SetRelativeLocationAndRotation(FVector(0, 0, -88), FRotator(0, -90, 0));
		GetMesh()->SetRelativeScale3D(FVector(0.84f));
	}

	// 캡슐 컴포넌트가 아닌 메시를 기준으로 히트시 채널을 관리해야한다. 사격시 Visibility에 반응하도록 설정했으므로 block으로 설정하는 것
	GetMesh()->SetCollisionProfileName(TEXT("Custom..."));
	GetMesh()->SetCollisionResponseToChannel(ECC_Visibility, ECR_Block);

	// 적(플레이어)을 추적할 때 자동으로 에셋이 그 방향을 향하도록 설정
	GetCharacterMovement()->bOrientRotationToMovement = true;

	fsm = CreateDefaultSubobject<UEnemyFSM>(TEXT("FSM"));

	// 경로 뒤에 _C를 붙여야 블루프린트 클래스로 인식해서 에러가 나지 않는다.
	ConstructorHelpers::FClassFinder<UAnimInstance> tempClass(TEXT("/Script/Engine.AnimBlueprint'/Game/Blueprints/ABP_Enemy.ABP_Enemy_C'"));
	if (tempClass.Succeeded()) {
		GetMesh()->SetAnimInstanceClass(tempClass.Class);
	}

	// 월드에 배치되거나 스폰될 때 자동으로
	// AIController로부터 Possess 될 수 있도록 설정
	AutoPossessAI = EAutoPossessAI::PlacedInWorldOrSpawned;
}

// Called when the game starts or when spawned
void AEnemy::BeginPlay()
{
	Super::BeginPlay();
	
}

// Called every frame
void AEnemy::Tick(float DeltaTime)
{
	Super::Tick(DeltaTime);

}

// Called to bind functionality to input
void AEnemy::SetupPlayerInputComponent(UInputComponent* PlayerInputComponent)
{
	Super::SetupPlayerInputComponent(PlayerInputComponent);

}

