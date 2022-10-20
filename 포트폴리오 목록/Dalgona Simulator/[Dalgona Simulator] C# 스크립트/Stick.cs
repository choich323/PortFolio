using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class Stick : MonoBehaviour
{
    [SerializeField] private Text clearTime;
    [SerializeField] private Text stage1_BestTime;
 
    public GameManger manager;
    public RankSystem rank;

    public GameObject resultUI;         // 결과창
    public GameObject sugar_white;      // 설탕을 뿌린 후의 오브젝트
    public GameObject sugar_white_mole; // 설탕을 젓다보면 나오는 파티클
    public GameObject sugar_brown_mole; // 설탕을 충분히 젓고 난 후 나오는 파티클
    public GameObject withBakingSoda;   // 베이킹 소다를 넣은 갈색의 설탕
    public GameObject sugar_brown_middle; // 조금 높아진 설탕 반죽
    public GameObject sugar_brown_high;   // 많이 높아진 설탕 반죽
    public GameObject sugar_complete;     // 완성된 설탕 반죽
    public GameObject swtch;              // 스위치

    public GameObject startMessage;       // 최초의 안내, 즉 설탕 뿌리라는 UI
    public GameObject swtchOnMessage;     // 스위치를 켜라는 UI
    public GameObject stirtoBrownMessage; // 설탕을 저으라는 UI
    public GameObject swtchOffMessage;    // 스위치를 끄라는 UI
    public GameObject bsPickupMessage;    // 베이킹소다를 집어서 뿌리라는 UI
    public GameObject stirMessage;        // 저으라는 UI

    public GameObject overFlowing;        // 넘치기 직전이라는 UI
    public GameObject clear;              // 클리어 UI
    public GameObject failed_overflow;    // 넘쳐서 실패했다는 UI

    public int total_MixCount;            // 전체 믹스카운트

    bool isStart = false;
    bool isbrown = false;
    int mixCount1 = 0;
    int mixCount2 = 0;
    int mixCount3 = 0;
    int mixCount4 = 0;
    float time = 0;
    float time1 = 0;
    float time2 = 0;
    float time3 = 0;

    void OnTriggerEnter(Collider other)
    {
        if (swtch.activeInHierarchy && sugar_white.activeInHierarchy && !sugar_white_mole.activeInHierarchy)
            MixCount(other, 6);
        else if (swtch.activeInHierarchy && sugar_white_mole.activeInHierarchy)
            MixCount(other, 12);
        else if (!swtch.activeInHierarchy && withBakingSoda.activeInHierarchy)
            MixCount(other, 17);
        else if (!swtch.activeInHierarchy && sugar_brown_middle.activeInHierarchy)
            MixCount(other, 22);
        else if (!swtch.activeInHierarchy && sugar_brown_high.activeInHierarchy)
            MixCount(other, 27);
    }

    void MixCount(Collider other, int value)
    {
        if (other.gameObject.tag == "B1" && mixCount1 < value)
            mixCount1++;
        else if (other.gameObject.tag == "B2" && mixCount2 < value)
            mixCount2++;
        else if (other.gameObject.tag == "B3" && mixCount3 < value)
            mixCount3++;
        else if (other.gameObject.tag == "B4" && mixCount4 < value)
            mixCount4++;
    }

    void Update()
    {
        UI();
        Sum();
        SugarChangetoWhiteMole();
        SugarChangetoBrownMole();
        SugarRise();
        SugarChangeBrown_Middle();
        SugarChangeBrown_High();
        SugarChangeBrown_Complete();
        Clear();
    }

    void UI()
    {
        if (!isStart && sugar_white.activeInHierarchy) {
            startMessage.SetActive(false);
            isStart = true;
            swtchOnMessage.SetActive(true);
        }

        if (swtch.activeInHierarchy && isStart && !isbrown)
        {
            swtchOnMessage.SetActive(false);
            stirtoBrownMessage.SetActive(true);
            manager._burning.SetActive(true);
        }

        if (stirtoBrownMessage.activeInHierarchy && !swtch.activeInHierarchy) // 예외 처리
        {
            stirtoBrownMessage.SetActive(false);
            swtchOnMessage.SetActive(true);
        }
        
        if(sugar_brown_mole.activeInHierarchy && swtch.activeInHierarchy)
        {
            stirtoBrownMessage.SetActive(false);
            swtchOffMessage.SetActive(true);
        }

        if (swtchOffMessage.activeInHierarchy && !swtch.activeInHierarchy)
        {
            swtchOffMessage.SetActive(false);
            manager._burning.SetActive(false);
            if(withBakingSoda.activeInHierarchy || sugar_brown_middle.activeInHierarchy || sugar_brown_high.activeInHierarchy)
                stirMessage.SetActive(true);
            else
                bsPickupMessage.SetActive(true);
        }

        if(bsPickupMessage.activeInHierarchy && swtch.activeInHierarchy) // 예외 처리
        {
            bsPickupMessage.SetActive(false);
            swtchOffMessage.SetActive(true);
            manager._burning.SetActive(true);
        }

        if (bsPickupMessage.activeInHierarchy && withBakingSoda.activeInHierarchy)
        {
            bsPickupMessage.SetActive(false);
            stirMessage.SetActive(true);
        }

        if(stirMessage.activeInHierarchy && swtch.activeInHierarchy) // 예외 처리
        {
            stirMessage.SetActive(false);
            manager._burning.SetActive(true);
            swtchOffMessage.SetActive(true);
        }
    }

    void Sum()
    {
        total_MixCount = mixCount1 + mixCount2 + mixCount3 + mixCount4;
        manager.Mix(total_MixCount);
    }

    void SugarChangetoWhiteMole()
    {
        if (total_MixCount == 24 && swtch.activeInHierarchy && sugar_white.activeInHierarchy)
        {
            sugar_white_mole.SetActive(true);
        }
    }

    void SugarChangetoBrownMole()
    {
        if (total_MixCount == 48 && swtch.activeInHierarchy && sugar_white_mole.activeInHierarchy)
        {
            sugar_brown_mole.SetActive(true);
            isbrown = true;
        }
    }

    void SugarRise()
    {
        if (withBakingSoda.activeInHierarchy)
        {
            time1 += Time.deltaTime;
            if (time1 > 10)
            {
                withBakingSoda.SetActive(false);
                sugar_brown_middle.SetActive(true);
                overFlowing.SetActive(true);
            }
        }
        else if (sugar_brown_middle.activeInHierarchy)
        {
            time2 += Time.deltaTime;
            if(time2 > 10)
            {
                sugar_brown_middle.SetActive(false);
                sugar_white_mole.SetActive(false);
                sugar_brown_mole.SetActive(false);
                sugar_brown_high.SetActive(true);
                overFlowing.SetActive(true);
            }
        }
        else if (sugar_brown_high.activeInHierarchy)
        {
            time3 += Time.deltaTime;
            if(time3 > 10)
            {
                if (overFlowing.activeInHierarchy)
                    overFlowing.SetActive(false);
                stirMessage.SetActive(false);
                manager._mixBar_2.SetActive(false);
                failed_overflow.SetActive(true);
            }
        }
    }

    void SugarChangeBrown_Middle()
    {
        if (withBakingSoda.activeInHierarchy)
        {
            if (!sugar_brown_middle.activeInHierarchy && total_MixCount == 68)
            {
                withBakingSoda.SetActive(false);
                sugar_brown_middle.SetActive(true);
            }
        }
    }

    void SugarChangeBrown_High()
    {
        if (sugar_brown_middle.activeInHierarchy)
        {
            if (!sugar_brown_high.activeInHierarchy && total_MixCount == 88)
            {
                sugar_brown_middle.SetActive(false);
                sugar_white_mole.SetActive(false);
                sugar_brown_mole.SetActive(false);
                sugar_brown_high.SetActive(true);
                overFlowing.SetActive(false);
            }
        }
    }

    void SugarChangeBrown_Complete()
    {
        if (sugar_brown_high.activeInHierarchy)
        {
            if (!sugar_complete.activeInHierarchy && total_MixCount == 108)
            {
                sugar_brown_high.SetActive(false);
                sugar_complete.SetActive(true);
                overFlowing.SetActive(false);
            }
        }
    }

    void Clear()
    {
        if (sugar_complete.activeInHierarchy && !failed_overflow.activeInHierarchy)
        {
            time += Time.deltaTime;
            if (time > 0.5f) {
                stirMessage.SetActive(false); // 젓기 UI 비활성화
                clear.SetActive(true);    // 클리어 UI 활성화
                manager.stopTImer = true; // 시간 멈추기
                clearTime.text = manager.timer.text;  // 이번 게임 클리어 시간
                rank.Rank(manager._minute, manager._second, 1); // 랭크 판정
                Invoke("GameResult", 1.5f);
            }
        }
    }

    void GameResult()
    {
        // 데이터 로드
        if (!PlayerPrefs.HasKey("stage1_BestSecond")) // 처음으로 게임을 플레이했을 때, 즉 저장 기록이 없으면
        {
            stage1_BestTime.text = clearTime.text;                      // 클리어타임이 곧 베스트타임
            PlayerPrefs.SetFloat("stage1_BestSecond", manager._second); // 기록 저장
            PlayerPrefs.SetFloat("stage1_BestMinute", manager._minute);
            PlayerPrefs.Save();
        }
        else // 로드할 데이터가 있으면
        {
            float bestSecond = PlayerPrefs.GetFloat("stage1_BestSecond"); // stage2 최고 초 로드
            float bestMinute = PlayerPrefs.GetFloat("stage1_BestMinute"); // stage2 최고 분 로드

            // 데이터 저장 과정
            // 분부터 비교해서 작은 쪽이 베스트 타임
            if (manager._minute < bestMinute) // 기존 베스트 타임보다 짧은 분을 기록했으면
            {
                bestMinute = manager._minute; // 기록 갱신
                bestSecond = manager._second;

                PlayerPrefs.SetFloat("stage1_BestSecond", manager._second); // 기록 저장
                PlayerPrefs.SetFloat("stage1_BestMinute", manager._minute);
                PlayerPrefs.Save();
            }
            else if (manager._minute == bestMinute) // 기존 베스트 타임과 분이 같으면
            {
                if (manager._second < bestSecond)    // 기존 베스트 타임보다 짧은 초를 기록한 경우
                {
                    bestSecond = manager._second; // 기록 갱신
                    PlayerPrefs.SetFloat("stage1_BestSecond", manager._second); // 기록 저장
                    PlayerPrefs.Save();
                }
            }
            stage1_BestTime.text = string.Format("{0:00}:{1:00}", bestMinute, bestSecond); // 베스트 타임 표기
        }

        resultUI.SetActive(true);  // 결과창 켜기
    }
}
