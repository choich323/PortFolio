using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchBoundary : MonoBehaviour
{
    public isTouch touch1;
    public isTouch touch2;
    public isTouch touch3;
    public isTouch touch4;
    public GameObject star; // ���� ����
    public GameObject starBlack; // �ٴ÷� � ���� ���� ����
    public GameObject frame; // Ʋ
    public GameObject dalgona; // �ް��� ũ�⸦ �˱� ����

    bool allTouch = false;
    public bool isclear = false;
    public bool ispressed = false;

    void FixedUpdate()
    {
        BoundaryCheck();
        PressCheck();
        StarActive();
    }

    void BoundaryCheck()
    {
        if (touch1.istouch && touch2.istouch && touch3.istouch && touch4.istouch)
            allTouch = true;
        else
            allTouch = false;
    }

    void PressCheck()
    {
        if (dalgona.transform.localScale.y == 0.025f)
            ispressed = true;
    }

    void StarActive() // ���� ����� Ȱ��ȭ
    {
        if (allTouch && !isclear && ispressed) {
            star.SetActive(true);
            star.transform.position = frame.transform.position;
            star.transform.rotation = frame.transform.rotation;
            starBlack.transform.position = frame.transform.position;
            starBlack.transform.rotation = frame.transform.rotation;
            isclear = true;
        }
    }
}
