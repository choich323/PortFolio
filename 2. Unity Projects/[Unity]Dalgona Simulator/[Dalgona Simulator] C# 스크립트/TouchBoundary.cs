using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchBoundary : MonoBehaviour
{
    public isTouch touch1;
    public isTouch touch2;
    public isTouch touch3;
    public isTouch touch4;
    public GameObject star; // 눌린 흔적
    public GameObject starBlack; // 바늘로 찌른 후의 눌린 흔적
    public GameObject frame; // 틀
    public GameObject dalgona; // 달고나의 크기를 알기 위해

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

    void StarActive() // 찍힌 모양을 활성화
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
