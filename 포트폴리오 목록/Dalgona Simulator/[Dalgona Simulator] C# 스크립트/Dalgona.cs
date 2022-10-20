using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dalgona : MonoBehaviour
{
    public GameObject cam;
    public GameObject press;
    public GameObject frame;
    public GameObject star;
    public GameObject board;
    public GameObject needle;

    public GameObject touchDalgonaUI;
    public GameObject BbobgiUI;

    public bool isTouch;
    public int black; // 바늘로 찔린 곳의 수
    SphereCollider sphere;
    Animator anim;

    void Awake()
    {
        sphere = GetComponent<SphereCollider>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        SphereOff();
    }

    void SphereOff()
    {
        if (touchDalgonaUI.activeInHierarchy)
            sphere.enabled = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Finger" && !sphere.enabled && !isTouch)
        {
            isTouch = true; // 터치 여부를 기록
            touchDalgonaUI.SetActive(false); // 기존 UI 끄기
            BbobgiUI.SetActive(true);       // 뽑기 UI 켜기
            anim.SetBool("isTouch", true); // 달고나의 크기를 키우기
            board.GetComponent<MeshRenderer>().enabled = false; // 달고나가 올려진 판을 감추기
            cam.transform.position = new Vector3(0.3f, 1.8f, 0.3f);  // 카메라의 위치 조정
            cam.transform.rotation = Quaternion.Euler(80, 0, 0);    // 카메라의 각도 조정
            press.SetActive(false); // press 제거
            frame.SetActive(false); // 틀 제거
            needle.SetActive(true); // 바늘 활성화
        }
    }
}
