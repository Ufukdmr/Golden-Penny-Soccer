using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;


    public static UIManager _Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<UIManager>();
            }
            return instance;
        }
    }

    public void SelectedCoin(Transform coin)
    {

        coin.GetChild(0).gameObject.SetActive(true);
        coin.GetChild(0).GetChild(0).GetComponent<PowerBarScript>().RotatePowerBar();
        coin.GetComponent<SpriteRenderer>().sortingOrder = 4;
        coin.GetChild(0).GetComponent<Canvas>().sortingOrder = 3;
        coin.GetChild(1).GetComponent<SpriteRenderer>().sortingOrder = 5;

    }

    public void DeselectedCoin(Transform selectedCoin)
    {

        if (selectedCoin != null)
        {
            selectedCoin.GetChild(0).gameObject.SetActive(false);
            selectedCoin.GetChild(0).GetChild(0).GetComponent<PowerBarScript>().RotatePowerBar();
            selectedCoin.GetChild(0).GetChild(0).transform.eulerAngles = new Vector3(0, 0, 0);
            selectedCoin.GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>().fillAmount = 1;
            selectedCoin.GetChild(0).GetChild(0).GetComponent<PowerBarScript>().lerpSpeed = 0;
            selectedCoin.GetComponent<SpriteRenderer>().sortingOrder = 1;
            selectedCoin.GetChild(0).GetComponent<Canvas>().sortingOrder = 2;
            selectedCoin.GetChild(1).GetComponent<SpriteRenderer>().sortingOrder = 2;
        }

        selectedCoin = null;


    }

    public void SetSpeed(Transform selectedCoin)
    {
        selectedCoin.GetChild(0).GetChild(0).GetComponent<PowerBarScript>().StartLerp();
    }
}
