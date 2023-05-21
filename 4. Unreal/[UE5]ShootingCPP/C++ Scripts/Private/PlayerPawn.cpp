// Fill out your copyright notice in the Description page of Project Settings.

#include "Components/BoxComponent.h"
#include "Components/StaticMeshComponent.h"
#include "PlayerPawn.h"
#include "Components/ArrowComponent.h"
#include "Bullet.h"
#include "Kismet/GamePlayStatics.h"

// Sets default values
APlayerPawn::APlayerPawn()
{
 	// Set this pawn to call Tick() every frame.  You can turn this off to improve performance if you don't need it.
	PrimaryActorTick.bCanEverTick = true;

	boxComp = CreateDefaultSubobject<UBoxComponent>(TEXT("My Box Component"));
	meshComp = CreateDefaultSubobject<UStaticMeshComponent>(TEXT("My mesh Component"));

	SetRootComponent(boxComp);
	meshComp->SetupAttachment(boxComp); // box�� ������Ʈ�� ����

	FVector boxSize = FVector(50.0f, 50.0f, 50.0f);
	boxComp->SetBoxExtent(boxSize);

	firePosition = CreateDefaultSubobject<UArrowComponent>(TEXT("Fire Position"));
	firePosition->SetupAttachment(boxComp);

	boxComp->SetGenerateOverlapEvents(true);
	// �浹 ���� ó�� ����
	boxComp->SetCollisionEnabled(ECollisionEnabled::QueryAndPhysics);
	// ������Ʈ ������ �ݸ���: 1�� ä��(Player)�� ����
	boxComp->SetCollisionObjectType(ECC_GameTraceChannel1);

	// ��� ä�ο� ���� ������ ���÷� ����
	boxComp->SetCollisionResponseToAllChannels(ECR_Ignore);
	// enemy ä�ο� ���� ��ħ ����
	boxComp->SetCollisionResponseToChannel(ECC_GameTraceChannel2, ECR_Overlap);
	// �̹� �� Ŭ���� ����� ��������Ʈ�� ���� �� �������� ������ �����ϸ� �ݿ����� �ʴ� ��찡 ����
	// ���� �Ϲ������δ� �������� ������ ��� �ۼ��� �Ŀ� ��������Ʈ�� �����Ѵ�.
	boxComp->SetCollisionResponseToChannel(ECC_WorldStatic, ECR_Block);
}

// Called when the game starts or when spawned
void APlayerPawn::BeginPlay()
{
	Super::BeginPlay();
	
}

// Called every frame
void APlayerPawn::Tick(float DeltaTime)
{
	Super::Tick(DeltaTime);
	// ���� ���͸� ���� ��
	FVector dir = FVector(0, h, v);
	// ������ ���̰� 1�� �ǵ��� ����ȭ
	dir.Normalize();
	// �̵��� ��ġ ��ǥ: p = p0 + vt ; (�ӵ�)v = ���� * �ӷ�
	FVector newLocation = GetActorLocation() + dir * moveSpeed * DeltaTime;

	// ���� true�� ��ġ�� ������ �� �߰��� �浹�� �߻��ϴ��� Ȯ���ϴ� ��. Ư���� ������ ������ false�� �⺻��
	// true�� ���� �˻��ϴ� ������ �� �����Ӹ��� CPU ������ �߰��ǹǷ� ȿ���� �����Ѵ�. �� �ʿ��� ���� �ƴ϶�� �ּ�ȭ�ؾ��� �κ�.
	SetActorLocation(newLocation, true); 
}

// Called to bind functionality to input
void APlayerPawn::SetupPlayerInputComponent(UInputComponent* PlayerInputComponent)
{
	Super::SetupPlayerInputComponent(PlayerInputComponent);

	PlayerInputComponent->BindAxis("Horizontal", this, &APlayerPawn::MoveHorizontal); // axis �̸�, ������ �Լ��� �ִ� Ŭ����, ������ �Լ��� �ּ� ��
	PlayerInputComponent->BindAxis("Vertical", this, &APlayerPawn::MoveVertical);
	
	PlayerInputComponent->BindAction("Fire", IE_Pressed, this, &APlayerPawn::Fire);
}

void APlayerPawn::MoveHorizontal(float value)
{
	h = value;
}

void APlayerPawn::MoveVertical(float value)
{
	v = value;
}

void APlayerPawn::Fire()
{
	ABullet* bullet = GetWorld()->SpawnActor<ABullet>(bulletFactory, firePosition->GetComponentLocation(), firePosition->GetComponentRotation());

	UGameplayStatics::PlaySound2D(GetWorld(), fireSound);
}
