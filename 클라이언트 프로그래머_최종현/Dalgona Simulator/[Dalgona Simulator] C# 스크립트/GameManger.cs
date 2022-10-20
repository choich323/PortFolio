using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManger : MonoBehaviour
{
    [SerializeField] public Text timer;

    public GameObject menu;

    // stage 1 UI
    public GameObject untilBrownUI;
    public GameObject bsPickupUI;
    public GameObject stirUI;
    public GameObject clearUI;

    // stage 2 UI
    public GameObject pressUI;
    public GameObject frameUI;

    public GameObject touchDalgonaUI;

    public TouchBoundary touchBoundaries;
    public Dalgona dalgona;

    // stage 1 slider
    public GameObject _burning;
    public GameObject _mixBar_1;
    public GameObject _mixBar_2;
    public Slider burning;
    public Slider mixBar_1;
    public Slider mixBar_2;

    // stage & timer
    float time;
    public float _second;
    public float _minute;
    public bool stopTImer = false;
    public bool stage2 = false;
    public int stage;

    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if (menu.activeSelf)
                menu.SetActive(false);
            else
                menu.SetActive(true);
        }
        if (menu.activeSelf)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;

        if (!stage2) // stage 1일때만
            BarOnoffUI();

        if (stage2){ // stage 2일때만
            PressFrame();
            
        }
        Timer();
    }

    void BarOnoffUI()
    {
        if (untilBrownUI.activeInHierarchy)
        {
            _mixBar_1.SetActive(true);
        }
        else if (bsPickupUI.activeInHierarchy)
        {
            _mixBar_1.SetActive(false);
        }

        if (stirUI.activeInHierarchy)
            _mixBar_2.SetActive(true);
        else if (clearUI.activeInHierarchy)
            _mixBar_2.SetActive(false);
    }

    void PressFrame()
    {
        if (!dalgona.isTouch)
        {
            if (touchBoundaries.ispressed)
            {
                pressUI.SetActive(false);
                frameUI.SetActive(true);
            }
            if (touchBoundaries.isclear)
            {
                frameUI.SetActive(false);
                touchDalgonaUI.SetActive(true);
            }
        }
    }

    void Timer()
    {
        if(!stopTImer)
          time += Time.deltaTime;

        _second = (int)(time % 60);
        _minute = (int)(time / 60 % 60);

        timer.text = string.Format("{0:00}:{1:00}", _minute, _second);      // 현재 시간
    }

    public void Mix(int count)
    {
        mixBar_1.value = count;
        mixBar_2.value = count;
    }

    public void Burning(int count)
    {
        burning.value = count;
    }

    public void Next()
    {
        SceneManager.LoadScene("Grip1");
    }

    public void Previous()
    {
        SceneManager.LoadScene("Grip0");
    }

    public void Retry()
    {
        SceneManager.LoadScene("Grip" + stage);
    }

    public void GameExit()
    {
        Application.Quit();
    }
}
