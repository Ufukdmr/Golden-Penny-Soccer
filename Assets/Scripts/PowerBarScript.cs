using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerBarScript : MonoBehaviour
{
    [SerializeField]
    private Image Img;

    private float rotatespeed;
    private int lerpValue_b;
    public int lerpSpeed;
    private float fillCurrentValue;

    [SerializeField]
    private GameObject coin;


    void Update()
    {
        transform.Rotate(new Vector3(0, 0, -360) * rotatespeed * Time.deltaTime);

        Img.fillAmount = Mathf.Lerp(Img.fillAmount, lerpValue_b, lerpSpeed * Time.deltaTime);

        if (Img.fillAmount < 0.15f)
        {
            lerpValue_b = 1;
        }
        else if (Img.fillAmount > 0.95)
        {
            lerpValue_b = 0;
        }

        if (Input.GetMouseButtonUp(0))
        {
            rotatespeed = 0;
            lerpSpeed = 0;
            fillCurrentValue = Img.fillAmount;
            coin.GetComponent<Coin>().ThrowCoin(fillCurrentValue);

            Img.fillAmount = 0;
        }

    }

    public void RotatePowerBar()
    {
        rotatespeed = 0.8f;
    }

    public void StartLerp()
    {
        rotatespeed = 0;
        lerpSpeed = 2;
        Img.fillAmount = 0;
    }
}
