using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class success : MonoBehaviour
{
    [SerializeField] private Text clearTime;
    [SerializeField] private Text stage2_BestTime;

    public GameManger manager;          // 게임매니저
    public RankSystem rank;             // 랭크 시스템
    public Dalgona dalgona;             // 달고나 스크립트
    public GameObject dalgonaObject;    // 달고나 오브젝트
    public GameObject needle;           // 바늘
    public GameObject cam;              // 카메라
    public GameObject bbogiUI;          // 뽑기 방법 안내 UI
    public GameObject successUI;        // 성공시 UI
    public GameObject resultUI;         // 결과창
    public GameObject caution;          // 뽑기 중 부서짐 경고

    Renderer render;

    void Awake()
    {
        render = GetComponent<Renderer>();
    }

    void Update()
    {
        if(dalgona.black == 1)  // 라인을 하나라도 긋고 나면
        {
            bbogiUI.SetActive(false); // 게임 방법 안내 UI를 비활성화
        }
        if (dalgona.black == 50) // 라인을 모두 완성하면
        {
            render.enabled = true;  // 완성된 별의 렌더러를 활성화
            // 달고나와 바늘 오브젝트를 끄고
            // 카메라 조정
            cam.transform.position = new Vector3(0.298f, 1.567f, 0.107f); // 카메라 조정
            cam.transform.rotation = Quaternion.identity;
            dalgonaObject.SetActive(false); // 기존 달고나 비활성화
            needle.SetActive(false);    // 바늘 비활성화
            caution.SetActive(false);   // 혹시나 켜져있을 경고창을 비활성화
            dalgona.board.GetComponent<MeshRenderer>().enabled = true; // 보드 그래픽 활성화
            successUI.SetActive(true); // 성공 UI
            manager.stopTImer = true; // 시간 멈추기

            clearTime.text = manager.timer.text;  // 이번 게임 클리어 시간

            rank.Rank(manager._minute, manager._second, 2); // 랭크 판정

            Invoke("GameResult", 1.5f);
        }
    }

    void GameResult()
    {
        // 데이터 로드
        if (!PlayerPrefs.HasKey("stage2_BestSecond")) // 처음으로 게임을 플레이했을 때, 즉 저장 기록이 없으면
        {
            stage2_BestTime.text = clearTime.text;                      // 클리어타임이 곧 베스트타임
            PlayerPrefs.SetFloat("stage2_BestSecond", manager._second); // 기록 저장
            PlayerPrefs.SetFloat("stage2_BestMinute", manager._minute);
            PlayerPrefs.Save();
        }
        else // 로드할 데이터가 있으면
        {
            float bestSecond = PlayerPrefs.GetFloat("stage2_BestSecond"); // stage2 최고 초 로드
            float bestMinute = PlayerPrefs.GetFloat("stage2_BestMinute"); // stage2 최고 분 로드

            // 데이터 저장 과정
            // 분부터 비교해서 작은 쪽이 베스트 타임
            if (manager._minute < bestMinute) // 기존 베스트 타임보다 짧은 분을 기록했으면
            {
                bestMinute = manager._minute; // 기록 갱신
                bestSecond = manager._second;

                PlayerPrefs.SetFloat("stage2_BestSecond", manager._second); // 기록 저장
                PlayerPrefs.SetFloat("stage2_BestMinute", manager._minute);
                PlayerPrefs.Save();
            }
            else if (manager._minute == bestMinute) // 기존 베스트 타임과 분이 같으면
            {
                if (manager._second < bestSecond)    // 기존 베스트 타임보다 짧은 초를 기록한 경우
                {
                    bestSecond = manager._second; // 기록 갱신
                    PlayerPrefs.SetFloat("stage2_BestSecond", manager._second); // 기록 저장
                    PlayerPrefs.Save();
                }
            }
            stage2_BestTime.text = string.Format("{0:00}:{1:00}", bestMinute, bestSecond); // 베스트 타임 표기
        }

        resultUI.SetActive(true);  // 결과창 켜기
    }
}
