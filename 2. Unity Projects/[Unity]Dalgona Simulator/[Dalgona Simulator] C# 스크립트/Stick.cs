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

    public GameObject resultUI;         // ���â
    public GameObject sugar_white;      // ������ �Ѹ� ���� ������Ʈ
    public GameObject sugar_white_mole; // ������ ���ٺ��� ������ ��ƼŬ
    public GameObject sugar_brown_mole; // ������ ����� ���� �� �� ������ ��ƼŬ
    public GameObject withBakingSoda;   // ����ŷ �Ҵٸ� ���� ������ ����
    public GameObject sugar_brown_middle; // ���� ������ ���� ����
    public GameObject sugar_brown_high;   // ���� ������ ���� ����
    public GameObject sugar_complete;     // �ϼ��� ���� ����
    public GameObject swtch;              // ����ġ

    public GameObject startMessage;       // ������ �ȳ�, �� ���� �Ѹ���� UI
    public GameObject swtchOnMessage;     // ����ġ�� �Ѷ�� UI
    public GameObject stirtoBrownMessage; // ������ ������� UI
    public GameObject swtchOffMessage;    // ����ġ�� ����� UI
    public GameObject bsPickupMessage;    // ����ŷ�Ҵٸ� ��� �Ѹ���� UI
    public GameObject stirMessage;        // ������� UI

    public GameObject overFlowing;        // ��ġ�� �����̶�� UI
    public GameObject clear;              // Ŭ���� UI
    public GameObject failed_overflow;    // ���ļ� �����ߴٴ� UI

    public int total_MixCount;            // ��ü �ͽ�ī��Ʈ

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

        if (stirtoBrownMessage.activeInHierarchy && !swtch.activeInHierarchy) // ���� ó��
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

        if(bsPickupMessage.activeInHierarchy && swtch.activeInHierarchy) // ���� ó��
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

        if(stirMessage.activeInHierarchy && swtch.activeInHierarchy) // ���� ó��
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
                stirMessage.SetActive(false); // ���� UI ��Ȱ��ȭ
                clear.SetActive(true);    // Ŭ���� UI Ȱ��ȭ
                manager.stopTImer = true; // �ð� ���߱�
                clearTime.text = manager.timer.text;  // �̹� ���� Ŭ���� �ð�
                rank.Rank(manager._minute, manager._second, 1); // ��ũ ����
                Invoke("GameResult", 1.5f);
            }
        }
    }

    void GameResult()
    {
        // ������ �ε�
        if (!PlayerPrefs.HasKey("stage1_BestSecond")) // ó������ ������ �÷������� ��, �� ���� ����� ������
        {
            stage1_BestTime.text = clearTime.text;                      // Ŭ����Ÿ���� �� ����ƮŸ��
            PlayerPrefs.SetFloat("stage1_BestSecond", manager._second); // ��� ����
            PlayerPrefs.SetFloat("stage1_BestMinute", manager._minute);
            PlayerPrefs.Save();
        }
        else // �ε��� �����Ͱ� ������
        {
            float bestSecond = PlayerPrefs.GetFloat("stage1_BestSecond"); // stage2 �ְ� �� �ε�
            float bestMinute = PlayerPrefs.GetFloat("stage1_BestMinute"); // stage2 �ְ� �� �ε�

            // ������ ���� ����
            // �к��� ���ؼ� ���� ���� ����Ʈ Ÿ��
            if (manager._minute < bestMinute) // ���� ����Ʈ Ÿ�Ӻ��� ª�� ���� ���������
            {
                bestMinute = manager._minute; // ��� ����
                bestSecond = manager._second;

                PlayerPrefs.SetFloat("stage1_BestSecond", manager._second); // ��� ����
                PlayerPrefs.SetFloat("stage1_BestMinute", manager._minute);
                PlayerPrefs.Save();
            }
            else if (manager._minute == bestMinute) // ���� ����Ʈ Ÿ�Ӱ� ���� ������
            {
                if (manager._second < bestSecond)    // ���� ����Ʈ Ÿ�Ӻ��� ª�� �ʸ� ����� ���
                {
                    bestSecond = manager._second; // ��� ����
                    PlayerPrefs.SetFloat("stage1_BestSecond", manager._second); // ��� ����
                    PlayerPrefs.Save();
                }
            }
            stage1_BestTime.text = string.Format("{0:00}:{1:00}", bestMinute, bestSecond); // ����Ʈ Ÿ�� ǥ��
        }

        resultUI.SetActive(true);  // ���â �ѱ�
    }
}
