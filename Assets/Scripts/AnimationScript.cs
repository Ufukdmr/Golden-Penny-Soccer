using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AnimationScript : MonoBehaviour
{
   
    [SerializeField]
    GameObject tutorialPanel;

    int moveCount;


    [SerializeField]
    TextMeshProUGUI text;
    [SerializeField]
    Animator anim;

    [SerializeField]
    Button btn_Right;

    [SerializeField]
    Button btn_Left;

  
 
    void Start()
    {
        
    }

   
    void Update()
    {
        if (moveCount == 1)
        {
            btn_Left.interactable = false;
        }
        else if (moveCount == 4)
        {
            btn_Right.interactable = false;
        }
        else
        {
            btn_Left.interactable = true;
            btn_Right.interactable = true;
        }
       
    }

    public void OpenAnimWindow()
    {
        tutorialPanel.SetActive(true);
        anim.SetInteger("Level", 1);
        text.fontSize = 115;
        text.text = "Touch On The Penny Once To Select It";
        anim.ResetTrigger("Close");
        anim.SetTrigger("Open");
        moveCount = 1;
       
    }

    public void RightorLeft(Button btn)
    {
       
        if (btn.name == "btn_Right")
        {
            moveCount++;
            anim.ResetTrigger("Undo");
        }
        else if (btn.name == "btn_Left")
        {
            moveCount--;
            anim.SetTrigger("Undo");
        }
        switch (moveCount)
        {
            case 1:
                anim.ResetTrigger("Throwed");
                anim.ResetTrigger("Selected");
                anim.ResetTrigger("Open");
                anim.SetInteger("Level", 1);
                text.fontSize = 115;
                text.text = "Touch On The Penny Once To Select It";
                break;
            case 2:
                anim.SetTrigger("Selected");
                anim.ResetTrigger("Throwed");
                anim.ResetTrigger("Open");
                anim.SetInteger("Level", 1);
                text.fontSize = 88;
                text.text = "Touch And Hold On The Penny To Show The Direction Arrow,When You're Happy With It Release To Shoot.";
                break;
            case 3:
                anim.SetTrigger("Throwed");
                anim.ResetTrigger("Selected");
                anim.ResetTrigger("Open");
                anim.SetInteger("Level", 1);
                text.fontSize = 96;
                text.text = "If You  didn't Like Where The Penny Landed You Can Always Use The Undo Function.";
                break;
            case 4:
                anim.SetInteger("Level", 2);
                anim.ResetTrigger("Selected");
                anim.ResetTrigger("Throwed");
                anim.ResetTrigger("Open");
                text.fontSize = 81;
                text.text = "If You Bounce The Penny From An Obstacle Before Getting A Goal, Collecting Stars Or Successful Shot You'll Get More Points.";
                break;

            default:
                break;
        }
    }
    public void Close()
    {
        tutorialPanel.SetActive(false);
        anim.ResetTrigger("Selected");
        anim.ResetTrigger("Undo");
        anim.ResetTrigger("Throwed");
        anim.SetTrigger("Close");
        anim.ResetTrigger("Open");
    }

}


