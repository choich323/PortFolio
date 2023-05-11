using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankSystem : MonoBehaviour
{
    // ���� ��ũ ����
    public GameObject S;
    public GameObject A;
    public GameObject B;
    public GameObject C;
    public GameObject D;

    public void Rank(float minute, float second, int stage)
    {
        if (stage == 1) {
            if (minute == 0 && second < 25)         // 25�� �̳�
                S.SetActive(true);
            else if (minute == 0 && second < 40)    // 40�� �̳�
                A.SetActive(true);
            else if (minute == 0 && second < 60)    // 1�� �̳�
                B.SetActive(true);
            else if (minute == 1 && second < 20)    // 1�� 20�� �̳�
                C.SetActive(true);
            else                                    // �� ��
                D.SetActive(true);
        } 
        else if(stage == 2)
        {
            if (minute == 0 && second < 60)         // 1�� �̳�
                S.SetActive(true);
            else if (minute == 1 && second < 20)    // 1�� 20�� �̳�
                A.SetActive(true);
            else if (minute == 1 && second < 40)    // 1�� 40�� �̳�
                B.SetActive(true);
            else if (minute == 1 && second < 60)    // 2�� �̳�
                C.SetActive(true);
            else                                    // �� ��
                D.SetActive(true);
        }
    }
}
