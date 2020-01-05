using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{

    [SerializeField]
    private GameObject Canvas;

    

    private int repetitionCount;

    public bool isSoundOn;
    [SerializeField]
    Sprite[] imgSound;
    [SerializeField]
    Button btn_Sound;

    void Start()
    {
        repetitionCount = PlayerPrefs.GetInt("Repetition");
        repetitionCount += 1;
        PlayerPrefs.SetInt("Repetition", repetitionCount);

        Debug.Log(PlayerPrefs.GetInt("Level"));

        if (PlayerPrefs.GetInt("Level") < 1)
        {

            Canvas.transform.GetChild(0).GetChild(0).GetComponent<Button>().interactable = false;
            Canvas.transform.GetChild(0).GetComponent<Image>().color = new Color(0, 0, 0, 0);
        }

        if (PlayerPrefs.GetInt("Repetition") >= 5)
        {
            GameObject.FindGameObjectWithTag("AdmobInterstitial").GetComponent<AdmobInterstitial>().AdmobShow();
            repetitionCount = 0;
            PlayerPrefs.SetInt("Repetition", repetitionCount);
        }

        #region SoundAction

        if (PlayerPrefs.GetInt("Sound") == 1)
        {
            isSoundOn = true;
            btn_Sound.GetComponent<Image>().sprite = imgSound[0];
        }
        else
        {
            isSoundOn = false;
            btn_Sound.GetComponent<Image>().sprite = imgSound[1];
        }

        #endregion

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Quit();
    }
    public void Play()
    {
        SceneManager.LoadScene("Level_1");
    }

    public void Continue()
    {
        SceneManager.LoadScene("Level_" + (PlayerPrefs.GetInt("Level") + 1).ToString());
    }

    public void Quit()
    {
        repetitionCount = 0;
        PlayerPrefs.SetInt("Repetition", repetitionCount);
        Application.Quit();
    }
    public void LevelsScene()
    {
        SceneManager.LoadScene("Levels");
    }

  
    public void Sound_OnorOff()
    {
        if (isSoundOn)
        {
            isSoundOn = false;
            btn_Sound.GetComponent<Image>().sprite = imgSound[1];
            PlayerPrefs.SetInt("Sound", 0);
        }
        else
        {
            isSoundOn = true;
            btn_Sound.GetComponent<Image>().sprite = imgSound[0];
            PlayerPrefs.SetInt("Sound", 1);
        }
    }

}



