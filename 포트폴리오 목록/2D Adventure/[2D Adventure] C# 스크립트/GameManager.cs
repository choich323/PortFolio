using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int totalPoint;
    public int stagePoint;
    public int stageIndex;
    public int health;
    public Player_Walk player;
    public GameObject[] Stages;

    public Image[] UIHealth;
    public Text UIPoint;
    public Text UIStage;
    public GameObject UIRestartBtn;


    void Update()
    {
        UIPoint.text = (totalPoint + stagePoint).ToString();    
    }

    public void NextStage()
    {
        // next stage
        if (stageIndex < Stages.Length - 1)
        {
            Stages[stageIndex].SetActive(false); // 기존 스테이지 off
            stageIndex++;                       // 다음 스테이지로
            Stages[stageIndex].SetActive(true); // 신규 스테이지 on
            PlayerReposition();

            UIStage.text = "Stage" + (stageIndex + 1);
        }
        else // Game Clear
        {
            // Player Control Lock
            Time.timeScale = 0;

            // Result UI


            // Restart Button UI
            Text btnText = UIRestartBtn.GetComponentInChildren<Text>();
            btnText.text = "Game Clear!";
            UIRestartBtn.SetActive(true);
        }



        // 포인트 계산
        totalPoint += stagePoint;
        stagePoint = 0;
    }

    public void HealthDown()
    {
        if (health > 1)
        {
            health--;
            UIHealth[health].color = new Color(1, 0, 0, 0.4f);
        }
        else
        {
            //All Health UI off
            UIHealth[0].color = new Color(1, 0, 0, 0.4f);

            //Player die effect
            player.OnDie();

            //Result UI


            //Retry Button UI
            UIRestartBtn.SetActive(true);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {

            // Player Reposition
            if (health > 1)
            {
                PlayerReposition();
            }

            // Health down
            HealthDown();
        }
    }

    void PlayerReposition()
    {
        player.transform.position = new Vector3(-10, 2, -1);
        player.VelocitiyZero();
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
