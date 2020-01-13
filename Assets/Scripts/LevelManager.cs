using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    int repetition;
    int leftRight;

    [SerializeField]
    GameObject Levels;

    [SerializeField]
    Button btn_Left;

    [SerializeField]
    Button btn_Right;
    void Start()
    {
        repetition = PlayerPrefs.GetInt("Repetition");
        repetition += 1;
        PlayerPrefs.SetInt("Repetition", repetition);

        for (int i = 0; i < Levels.transform.childCount; i++)
        {
            for (int k = 0; k < Levels.transform.GetChild(i).childCount; k++)
            {
                Levels.transform.GetChild(i).GetChild(k).GetChild(0).GetChild(2).GetChild(0).GetComponent<Image>().fillAmount = 0;
                Levels.transform.GetChild(i).GetChild(k).GetChild(0).GetChild(3).GetChild(0).GetComponent<Image>().fillAmount = 0;
                Levels.transform.GetChild(i).GetChild(k).GetChild(0).GetChild(4).GetChild(0).GetComponent<Image>().fillAmount = 0;

                if (Levels.transform.GetChild(i).GetChild(k).gameObject.tag == "FourStar")
                {
                    Levels.transform.GetChild(i).GetChild(k).GetChild(0).GetChild(5).GetChild(0).GetComponent<Image>().fillAmount = 0;
                }

            }
        }

        Buttons();

        if (PlayerPrefs.GetInt("Repetition") >= 5)
        {
            GameObject.FindGameObjectWithTag("AdmobInterstitial").GetComponent<AdmobInterstitial>().AdmobShow();
            repetition = 0;
            PlayerPrefs.SetInt("Repetition", repetition);
        }
       
    }

    void Buttons()
    {
      
        for (int i = 0; i < PlayerPrefs.GetInt("Level"); i++)
        {
          
            if (i <= 5 && Levels.transform.GetChild(0).gameObject.activeSelf)
            {

                EditButtonContent(0, i);
            }
            else if (i > 5 && i <= 11 && Levels.transform.GetChild(1).gameObject.activeSelf)
            {
                EditButtonContent(1, (i - 6));

            }
            else if (i > 11 && i <= 17 && Levels.transform.GetChild(2).gameObject.activeSelf)
            {
                EditButtonContent(2, i - 12);
            }
            else if (i > 17 && i <= 22 && Levels.transform.GetChild(3).gameObject.activeSelf)
            {
                EditButtonContent(3, i - 18);
            }
        }


    }

    void EditButtonContent(int i, int k)
    {


        Levels.transform.GetChild(i).GetChild(k).GetComponent<Image>().color = new Color(0, 0, 0, 0.392f);
        Levels.transform.GetChild(i).GetChild(k).GetChild(0).GetComponent<Button>().interactable = true;

        if (Levels.transform.GetChild(i).GetChild(k).gameObject.tag == "FourStar")
        {
            Levels.transform.GetChild(i).GetChild(k).GetChild(0).GetChild(7).GetComponent<TextMeshProUGUI>().text = "Hit: " + PlayerPrefs.GetInt(Levels.transform.GetChild(i).GetChild(k).GetChild(0).GetChild(0).GetComponent<Text>().text + "_Hit").ToString();
            Levels.transform.GetChild(i).GetChild(k).GetChild(0).GetChild(6).GetComponent<TextMeshProUGUI>().text = "Point: " + PlayerPrefs.GetInt(Levels.transform.GetChild(i).GetChild(k).GetChild(0).GetChild(0).GetComponent<Text>().text + "_Point").ToString();
        }
        else
        {
            Levels.transform.GetChild(i).GetChild(k).GetChild(0).GetChild(6).GetComponent<TextMeshProUGUI>().text = "Hit: " + PlayerPrefs.GetInt(Levels.transform.GetChild(i).GetChild(k).GetChild(0).GetChild(0).GetComponent<Text>().text + "_Hit").ToString();
            Levels.transform.GetChild(i).GetChild(k).GetChild(0).GetChild(5).GetComponent<TextMeshProUGUI>().text = "Point: " + PlayerPrefs.GetInt(Levels.transform.GetChild(i).GetChild(k).GetChild(0).GetChild(0).GetComponent<Text>().text + "_Point").ToString();
        }

        for (int j = 0; j < PlayerPrefs.GetInt(Levels.transform.GetChild(i).GetChild(k).GetChild(0).GetChild(0).GetComponent<Text>().text + "_TakedStar"); j++)
        {
            Levels.transform.GetChild(i).GetChild(k).GetChild(0).GetChild(j + 2).GetChild(0).GetComponent<Image>().fillAmount = 1;
        }

    }

   
    void Update()
    {
        if (leftRight == 0)
        {
            btn_Left.interactable = false;
        }
        else if (leftRight == 3)
        {
            btn_Right.interactable = false;
        }
        else
        {
            btn_Left.interactable = true;
            btn_Right.interactable = true;
        }

    }

    public void SelectedLevel(Button btn)
    {
        SceneManager.LoadScene(btn.transform.GetChild(0).GetComponent<Text>().text);
    }

    public void Back()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void RightorLeft(Button btn)
    {
        if (btn.name == "btn_Right")
        {
            leftRight++;
        }
        else if (btn.name == "btn_Left")
        {
            leftRight--;
        }


        for (int i = 0; i < Levels.transform.childCount; i++)
        {
            if (i == leftRight)
            {
                Levels.transform.GetChild(i).gameObject.SetActive(true);

            }
            else
            {
                Levels.transform.GetChild(i).gameObject.SetActive(false);
            }

        }
        Buttons();
    }
}

