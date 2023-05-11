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
    public int black; // �ٴ÷� �� ���� ��
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
            isTouch = true; // ��ġ ���θ� ���
            touchDalgonaUI.SetActive(false); // ���� UI ����
            BbobgiUI.SetActive(true);       // �̱� UI �ѱ�
            anim.SetBool("isTouch", true); // �ް��� ũ�⸦ Ű���
            board.GetComponent<MeshRenderer>().enabled = false; // �ް��� �÷��� ���� ���߱�
            cam.transform.position = new Vector3(0.3f, 1.8f, 0.3f);  // ī�޶��� ��ġ ����
            cam.transform.rotation = Quaternion.Euler(80, 0, 0);    // ī�޶��� ���� ����
            press.SetActive(false); // press ����
            frame.SetActive(false); // Ʋ ����
            needle.SetActive(true); // �ٴ� Ȱ��ȭ
        }
    }
}
