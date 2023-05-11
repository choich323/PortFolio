using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class success : MonoBehaviour
{
    [SerializeField] private Text clearTime;
    [SerializeField] private Text stage2_BestTime;

    public GameManger manager;          // ���ӸŴ���
    public RankSystem rank;             // ��ũ �ý���
    public Dalgona dalgona;             // �ް� ��ũ��Ʈ
    public GameObject dalgonaObject;    // �ް� ������Ʈ
    public GameObject needle;           // �ٴ�
    public GameObject cam;              // ī�޶�
    public GameObject bbogiUI;          // �̱� ��� �ȳ� UI
    public GameObject successUI;        // ������ UI
    public GameObject resultUI;         // ���â
    public GameObject caution;          // �̱� �� �μ��� ���

    Renderer render;

    void Awake()
    {
        render = GetComponent<Renderer>();
    }

    void Update()
    {
        if(dalgona.black == 1)  // ������ �ϳ��� �߰� ����
        {
            bbogiUI.SetActive(false); // ���� ��� �ȳ� UI�� ��Ȱ��ȭ
        }
        if (dalgona.black == 50) // ������ ��� �ϼ��ϸ�
        {
            render.enabled = true;  // �ϼ��� ���� �������� Ȱ��ȭ
            // �ް��� �ٴ� ������Ʈ�� ����
            // ī�޶� ����
            cam.transform.position = new Vector3(0.298f, 1.567f, 0.107f); // ī�޶� ����
            cam.transform.rotation = Quaternion.identity;
            dalgonaObject.SetActive(false); // ���� �ް� ��Ȱ��ȭ
            needle.SetActive(false);    // �ٴ� ��Ȱ��ȭ
            caution.SetActive(false);   // Ȥ�ó� �������� ���â�� ��Ȱ��ȭ
            dalgona.board.GetComponent<MeshRenderer>().enabled = true; // ���� �׷��� Ȱ��ȭ
            successUI.SetActive(true); // ���� UI
            manager.stopTImer = true; // �ð� ���߱�

            clearTime.text = manager.timer.text;  // �̹� ���� Ŭ���� �ð�

            rank.Rank(manager._minute, manager._second, 2); // ��ũ ����

            Invoke("GameResult", 1.5f);
        }
    }

    void GameResult()
    {
        // ������ �ε�
        if (!PlayerPrefs.HasKey("stage2_BestSecond")) // ó������ ������ �÷������� ��, �� ���� ����� ������
        {
            stage2_BestTime.text = clearTime.text;                      // Ŭ����Ÿ���� �� ����ƮŸ��
            PlayerPrefs.SetFloat("stage2_BestSecond", manager._second); // ��� ����
            PlayerPrefs.SetFloat("stage2_BestMinute", manager._minute);
            PlayerPrefs.Save();
        }
        else // �ε��� �����Ͱ� ������
        {
            float bestSecond = PlayerPrefs.GetFloat("stage2_BestSecond"); // stage2 �ְ� �� �ε�
            float bestMinute = PlayerPrefs.GetFloat("stage2_BestMinute"); // stage2 �ְ� �� �ε�

            // ������ ���� ����
            // �к��� ���ؼ� ���� ���� ����Ʈ Ÿ��
            if (manager._minute < bestMinute) // ���� ����Ʈ Ÿ�Ӻ��� ª�� ���� ���������
            {
                bestMinute = manager._minute; // ��� ����
                bestSecond = manager._second;

                PlayerPrefs.SetFloat("stage2_BestSecond", manager._second); // ��� ����
                PlayerPrefs.SetFloat("stage2_BestMinute", manager._minute);
                PlayerPrefs.Save();
            }
            else if (manager._minute == bestMinute) // ���� ����Ʈ Ÿ�Ӱ� ���� ������
            {
                if (manager._second < bestSecond)    // ���� ����Ʈ Ÿ�Ӻ��� ª�� �ʸ� ����� ���
                {
                    bestSecond = manager._second; // ��� ����
                    PlayerPrefs.SetFloat("stage2_BestSecond", manager._second); // ��� ����
                    PlayerPrefs.Save();
                }
            }
            stage2_BestTime.text = string.Format("{0:00}:{1:00}", bestMinute, bestSecond); // ����Ʈ Ÿ�� ǥ��
        }

        resultUI.SetActive(true);  // ���â �ѱ�
    }
}
