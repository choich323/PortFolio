using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankSystem : MonoBehaviour
{
    // 게임 랭크 변수
    public GameObject S;
    public GameObject A;
    public GameObject B;
    public GameObject C;
    public GameObject D;

    public void Rank(float minute, float second, int stage)
    {
        if (stage == 1) {
            if (minute == 0 && second < 25)         // 25초 이내
                S.SetActive(true);
            else if (minute == 0 && second < 40)    // 40초 이내
                A.SetActive(true);
            else if (minute == 0 && second < 60)    // 1분 이내
                B.SetActive(true);
            else if (minute == 1 && second < 20)    // 1분 20초 이내
                C.SetActive(true);
            else                                    // 그 외
                D.SetActive(true);
        } 
        else if(stage == 2)
        {
            if (minute == 0 && second < 60)         // 1분 이내
                S.SetActive(true);
            else if (minute == 1 && second < 20)    // 1분 20초 이내
                A.SetActive(true);
            else if (minute == 1 && second < 40)    // 1분 40초 이내
                B.SetActive(true);
            else if (minute == 1 && second < 60)    // 2분 이내
                C.SetActive(true);
            else                                    // 그 외
                D.SetActive(true);
        }
    }
}
