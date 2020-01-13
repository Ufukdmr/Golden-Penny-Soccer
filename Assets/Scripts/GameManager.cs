using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] coins;

    [SerializeField]
    private GameObject[] stars;

    [SerializeField]
    private GameObject[] woods;

    [SerializeField]
    private TextMeshProUGUI txt_Hit;

    [SerializeField]
    private TextMeshProUGUI txt_Point;

    [SerializeField]
    private GameObject[] canvasStars;

    [SerializeField]
    private Image[] gameMenuStarts;

    [SerializeField]
    private GameObject gameMenu;

    [SerializeField]
    private GameObject borders;

    [SerializeField]
    private GameObject[] goalKeeper;

    [SerializeField]
    private GameObject goal;

    [SerializeField]
    private GameObject AdsMenu;

    private GameObject hitCoin;
    private GameObject selectedCoin;
    private Coin coinScript;

    private GameObject managerThrowedcoin;

    private int clickCount = 0;
    private int takedStarCount = 0;

    public int point = 0;

    public int totalHit;
    public int remainingHit;
    public int Hit;

    public bool isFinish = false;
    private bool isGoal = false;

    int repetition;

    [SerializeField]
    AudioSource sound;

    public bool isSoundOn;
    [SerializeField]
    Sprite[] imgSound;
    [SerializeField]
    Button btn_Sound;

    public bool usedUndo = false;

    #region UNDO Variable
    int move = 0;
    int savecontrol = 0;
    int Undo = 1;
    int a = 0;
    public bool undoCont = false;
    #endregion

    #region TimeBarVariable
    float maxTime = 10f;
    float activeTime = 0;
    float passedTime = 0;
    float b = 0;
    #endregion

    [SerializeField]
    private Button btn_Undo;

    [SerializeField]
    private TextMeshProUGUI txt_OverorContinue;


    private static GameManager instance;

    public static GameManager _Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
            }
            return instance;
        }
    }

    void Start()
    {
        PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "Pass", 1);
        if (PlayerPrefs.GetInt("Level") < SceneManager.GetActiveScene().buildIndex)
        {
            PlayerPrefs.SetInt("Level", SceneManager.GetActiveScene().buildIndex);
        }

        #region UndoAction
        btn_Undo.interactable = false;
        PlayerPrefs.SetInt("Undo", Undo);
        #endregion

        #region AdsAction

        repetition = PlayerPrefs.GetInt("Repetition");
        repetition += 1;
        PlayerPrefs.SetInt("Repetition", repetition);

        if (PlayerPrefs.GetInt("Repetition") >= 5)
        {
            GameObject.FindGameObjectWithTag("AdmobInterstitial").GetComponent<AdmobInterstitial>().AdmobShow();
            repetition = 0;
            PlayerPrefs.SetInt("Repetition", repetition);
        }
        #endregion
      
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

        remainingHit = totalHit;
        txt_Hit.text = "Hit: " + Hit + " / " + totalHit;
        txt_Point.text = "Point: " + point;

    }

    private void FixedUpdate()
    {
        b = Time.time;
        
        if (AdsMenu.activeSelf && AdsMenu.transform.GetChild(0).GetChild(4).gameObject.activeSelf)
        {
            if (b == (int)b)
            {
                activeTime += 1;

            }

            passedTime = activeTime / maxTime;

            AdsMenu.transform.GetChild(0).GetChild(4).GetChild(0).GetComponent<Image>().fillAmount = Mathf.Lerp(AdsMenu.transform.GetChild(0).GetChild(4).GetChild(0).GetComponent<Image>().fillAmount, 1, passedTime * Time.deltaTime);

            if (AdsMenu.transform.GetChild(0).GetChild(4).GetChild(0).GetComponent<Image>().fillAmount > 0.99f)
            {
                GameOverorContinue();
            }
            
        }

       
    }

    void Update()
    {
        ClickCoin();


        txt_Point.text = "Point: " + point;
        txt_Hit.text = "Hit: " + Hit + " / " + totalHit;

        SaveVariable();

        if (isSoundOn)
        {
            sound.mute = false;
        }
        else
        {
            sound.mute = true;
        }

        if (RayCastManager._Instance.isStopped(coins[0]) && RayCastManager._Instance.isStopped(coins[1]) &&RayCastManager._Instance.isStopped(coins[2])&&!isGoal)
        {
            if (Hit == totalHit)
            {
                isFinish = true;
                AdsShow();
                
            }
        }
        if (!RayCastManager._Instance.isStopped(coins[0]) || !RayCastManager._Instance.isStopped(coins[1]) || !RayCastManager._Instance.isStopped(coins[2]))
        {
            btn_Undo.interactable = false;
        }
        else if (Hit > 0 && !isFinish)
        {
            if (usedUndo == false)
            {
                btn_Undo.interactable = true;
            }

        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainMenu");
        }
        Debug.Log(PlayerPrefs.GetInt("Level"));
    }

    private void ClickCoin()
    {
        if (RayCastManager._Instance.isStopped(coins[0]) && RayCastManager._Instance.isStopped(coins[1]) && RayCastManager._Instance.isStopped(coins[2]))
        {
            for (int i = 0; i < coins.Length; i++)
            {
                if (coins[i].layer == 8)
                {
                    coins[i].transform.GetChild(1).gameObject.SetActive(true);
                }
                else
                {
                    coins[i].transform.GetChild(1).gameObject.SetActive(false);

                }

            }

            if (Input.GetMouseButtonDown(0))
            {

                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, 256);

                if (hit)
                {
                    if (clickCount > 0 && hit.collider.gameObject == hitCoin)
                    {
                        move = 0;

                        UIManager._Instance.SetSpeed(selectedCoin.transform);
                        usedUndo = false;
                    }

                    hitCoin = hit.collider.gameObject;

                }
                else
                {
                    hitCoin = null;
                    clickCount = 0;
                    if (selectedCoin != null)
                    {
                        UIManager._Instance.DeselectedCoin(selectedCoin.transform);
                    }

                }
            }
            if (Input.GetMouseButtonUp(0))
            {
                if (hitCoin != null)
                {

                    if (coinScript != null)
                    {

                        if (selectedCoin != null)
                        {

                            if (selectedCoin != hitCoin)
                            {
                                UIManager._Instance.DeselectedCoin(selectedCoin.transform);
                                clickCount = 0;
                            }
                        }

                        else
                        {
                            clickCount++;
                        }
                    }

                    coinScript = hitCoin.GetComponent<Coin>();
                    selectedCoin = hitCoin;

                    if (clickCount == 0)
                    {

                        UIManager._Instance.SelectedCoin(selectedCoin.transform);
                    }

                    clickCount++;
                }
                else
                {
                    clickCount = 0;
                    if (selectedCoin != null)
                    {
                        UIManager._Instance.DeselectedCoin(selectedCoin.transform);
                    }

                    hitCoin = null;
                }
            }

        }
        else
        {
            for (int i = 0; i < coins.Length; i++)
            {
                coins[i].transform.GetChild(1).gameObject.SetActive(false);
                if (coins[i].layer != 0)
                {
                    UIManager._Instance.DeselectedCoin(coins[i].gameObject.transform);
                    if (coinScript != null)
                    {
                        UIManager._Instance.DeselectedCoin(hitCoin.transform);
                    }
                    clickCount = 0;
                }

            }
        }

    }

    public float CoinsDistance(GameObject selectedCoin)
    {
        //Distance  For Raycast 

        if (coins[0].tag == selectedCoin.tag)
        {


            if (coins[1].transform.position.x == coins[2].transform.position.x && coins[1].transform.position.y != coins[2].transform.position.y)
            {
                if (coins[1].transform.position.y > coins[2].transform.position.y)
                {
                    return coins[1].transform.position.y - coins[2].transform.position.y;
                }
                else
                {
                    return coins[2].transform.position.y - coins[1].transform.position.y;
                }
            }
            else if (coins[1].transform.position.x != coins[2].transform.position.x && coins[1].transform.position.y == coins[2].transform.position.y)
            {
                if (coins[1].transform.position.x > coins[2].transform.position.x)
                {
                    return coins[1].transform.position.x - coins[2].transform.position.x;
                }
                else
                {
                    return coins[2].transform.position.x - coins[1].transform.position.x;
                }
            }
            else
            {
                float maxX = Mathf.Max(coins[1].transform.position.x, coins[2].transform.position.x);
                float maxY = Mathf.Max(coins[1].transform.position.y, coins[2].transform.position.y);
                if (maxX == coins[1].transform.position.x)
                {
                    if (maxY == coins[1].transform.position.y)
                    {
                        float a = Mathf.Abs(coins[1].transform.position.x - coins[2].transform.position.x);
                        float b = Mathf.Abs(coins[1].transform.position.y - coins[2].transform.position.y);
                        float c = (a * a) + (b * b);
                        return Mathf.Sqrt(c);
                    }
                    else
                    {
                        float a = Mathf.Abs(coins[1].transform.position.x - coins[2].transform.position.x);
                        float b = Mathf.Abs(coins[2].transform.position.y - coins[1].transform.position.y);
                        float c = (a * a) + (b * b);
                        return Mathf.Sqrt(c);
                    }
                }
                else
                {
                    if (maxY == coins[1].transform.position.y)
                    {
                        float a = Mathf.Abs(coins[2].transform.position.x - coins[1].transform.position.x);
                        float b = Mathf.Abs(coins[1].transform.position.y - coins[2].transform.position.y);
                        float c = (a * a) + (b * b);
                        return Mathf.Sqrt(c);
                    }
                    else
                    {
                        float a = Mathf.Abs(coins[2].transform.position.x - coins[1].transform.position.x);
                        float b = Mathf.Abs(coins[2].transform.position.y - coins[1].transform.position.y);
                        float c = (a * a) + (b * b);
                        return Mathf.Sqrt(c);
                    }
                }

            }


        }
        else if (coins[1].tag == selectedCoin.tag)
        {
            if (coins[2].transform.position.x == coins[0].transform.position.x && coins[2].transform.position.y != coins[0].transform.position.y)
            {
                if (coins[2].transform.position.y > coins[0].transform.position.y)
                {
                    return coins[2].transform.position.y - coins[0].transform.position.y;
                }
                else
                {
                    return coins[0].transform.position.y - coins[2].transform.position.y;
                }
            }
            else if (coins[2].transform.position.x != coins[0].transform.position.x && coins[2].transform.position.y == coins[0].transform.position.y)
            {
                if (coins[2].transform.position.x > coins[0].transform.position.x)
                {
                    return coins[2].transform.position.x - coins[0].transform.position.x;
                }
                else
                {
                    return coins[0].transform.position.x - coins[2].transform.position.x;
                }
            }
            else
            {
                float maxX = Mathf.Max(coins[2].transform.position.x, coins[0].transform.position.x);
                float maxY = Mathf.Max(coins[2].transform.position.y, coins[0].transform.position.y);
                if (maxX == coins[2].transform.position.x)
                {
                    if (maxY == coins[2].transform.position.y)
                    {
                        float a = Mathf.Abs(coins[2].transform.position.x - coins[0].transform.position.x);
                        float b = Mathf.Abs(coins[2].transform.position.y - coins[0].transform.position.y);
                        float c = (a * a) + (b * b);
                        return Mathf.Sqrt(c);
                    }
                    else
                    {
                        float a = Mathf.Abs(coins[2].transform.position.x - coins[0].transform.position.x);
                        float b = Mathf.Abs(coins[0].transform.position.y - coins[2].transform.position.y);
                        float c = (a * a) + (b * b);
                        return Mathf.Sqrt(c);
                    }
                }
                else
                {
                    if (maxY == coins[2].transform.position.y)
                    {
                        float a = Mathf.Abs(coins[0].transform.position.x - coins[2].transform.position.x);
                        float b = Mathf.Abs(coins[2].transform.position.y - coins[0].transform.position.y);
                        float c = (a * a) + (b * b);
                        return Mathf.Sqrt(c);
                    }
                    else
                    {
                        float a = Mathf.Abs(coins[0].transform.position.x - coins[2].transform.position.x);
                        float b = Mathf.Abs(coins[0].transform.position.y - coins[2].transform.position.y);
                        float c = (a * a) + (b * b);
                        return Mathf.Sqrt(c);
                    }
                }

            }

        }
        else if (coins[2].tag == selectedCoin.tag)
        {
            if (coins[0].transform.position.x == coins[1].transform.position.x && coins[0].transform.position.y != coins[1].transform.position.y)
            {
                if (coins[0].transform.position.y > coins[1].transform.position.y)
                {
                    return coins[0].transform.position.y - coins[1].transform.position.y;
                }
                else
                {
                    return coins[1].transform.position.y - coins[0].transform.position.y;
                }
            }
            else if (coins[0].transform.position.x != coins[1].transform.position.x && coins[0].transform.position.y == coins[1].transform.position.y)
            {
                if (coins[0].transform.position.x > coins[1].transform.position.x)
                {
                    return coins[0].transform.position.x - coins[1].transform.position.x;
                }
                else
                {
                    return coins[1].transform.position.x - coins[0].transform.position.x;
                }
            }
            else
            {
                float maxX = Mathf.Max(coins[0].transform.position.x, coins[1].transform.position.x);
                float maxY = Mathf.Max(coins[0].transform.position.y, coins[1].transform.position.y);
                if (maxX == coins[0].transform.position.x)
                {
                    if (maxY == coins[0].transform.position.y)
                    {
                        float a = Mathf.Abs(coins[0].transform.position.x - coins[1].transform.position.x);
                        float b = Mathf.Abs(coins[0].transform.position.y - coins[1].transform.position.y);
                        float c = (a * a) + (b * b);
                        return Mathf.Sqrt(c);
                    }
                    else
                    {
                        float a = Mathf.Abs(coins[0].transform.position.x - coins[1].transform.position.x);
                        float b = Mathf.Abs(coins[1].transform.position.y - coins[0].transform.position.y);
                        float c = (a * a) + (b * b);
                        return Mathf.Sqrt(c);
                    }
                }
                else
                {
                    if (maxY == coins[0].transform.position.y)
                    {
                        float a = Mathf.Abs(coins[1].transform.position.x - coins[0].transform.position.x);
                        float b = Mathf.Abs(coins[0].transform.position.y - coins[1].transform.position.y);
                        float c = (a * a) + (b * b);
                        return Mathf.Sqrt(c);
                    }
                    else
                    {
                        float a = Mathf.Abs(coins[1].transform.position.x - coins[0].transform.position.x);
                        float b = Mathf.Abs(coins[1].transform.position.y - coins[0].transform.position.y);
                        float c = (a * a) + (b * b);
                        return Mathf.Sqrt(c);
                    }
                }

            }


        }
        else
        {
            return 0;
        }

    }

    public void CoinsLayer(GameObject throwedCoin)
    {
        //Coins change Layer
        if (!isFinish)
        {
            if (coins[0].tag == throwedCoin.tag)
            {

                throwedCoin.layer = 9;
                coins[1].layer = 8;
                coins[2].layer = 8;


            }
            else if (coins[1].tag == throwedCoin.tag)
            {
                coins[0].layer = 8;
                throwedCoin.layer = 9;
                coins[2].layer = 8;
            }
            else if (coins[2].tag == throwedCoin.tag)
            {
                coins[0].layer = 8;
                coins[1].layer = 8;
                throwedCoin.layer = 9;
            }

        }
        else
        {
            for (int i = 0; i < coins.Length; i++)
            {

                coins[i].layer = 9;

            }
        }

    }

    public void HitPointCount(GameObject throwedCoin)
    {
        if (RayCastManager._Instance.isPassed && throwedCoin.GetComponent<Coin>().isBounce)
        {
            point += 500;
        }
        remainingHit--;
        Hit = totalHit - remainingHit;
        
    }

    public void TakeStar(GameObject throwedCoin)
    {
        sound.Play();

        if (throwedCoin.GetComponent<Coin>().isBounce)
        {

            canvasStars[(takedStarCount)].GetComponent<Image>().fillAmount = 1;
            takedStarCount++;
            //Canvas Star Show
            point += (2000 * throwedCoin.GetComponent<Coin>().hitTakedStarCaunt);

        }
        else
        {

            canvasStars[(takedStarCount)].GetComponent<Image>().fillAmount = 1;
            takedStarCount++;
            //Canvas Star Show
            point += (1000 * throwedCoin.GetComponent<Coin>().hitTakedStarCaunt);

        }


    }

    public void Again()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void AdsShow()
    {

        for (int i = 0; i < coins.Length; i++)
        {
            coins[i].GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            coins[i].GetComponent<Rigidbody2D>().angularDrag = 100;

        }
        for (int i = 0; i < woods.Length; i++)
        {
            woods[i].GetComponent<Wood>().moveSpeed = 0;
            woods[i].GetComponent<Wood>().rotateSpeed = 0;
        }


        if (totalHit - remainingHit >= 1 && !gameMenu.activeSelf)
        {
            txt_OverorContinue.text = "Game Over";
            AdsMenu.transform.GetChild(0).GetChild(4).gameObject.SetActive(true);

            if (Undo == 1)
            {
                AdsMenu.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Game Over";
                AdsMenu.transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text = "You have one undo right. Do you want to use ? ";
                AdsMenu.transform.GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Use It";


            }
            else
            {
                AdsMenu.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Game Over";
                AdsMenu.transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text = "You don't have any undo's left. Would you like to earn one ? ";
                AdsMenu.transform.GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Watch Ads";
            }
            btn_Undo.interactable = false;
            AdsMenu.SetActive(true);            
            goalKeeper[0].GetComponent<GoalKeeper>().rotateSpeed = 0;
            goal.SetActive(false);
            isFinish = true;
            AdsMenu.transform.GetChild(0).GetChild(4).GetChild(0).GetComponent<Image>().fillAmount = 0;


        }
        else
        {
            Gameover();
        }


    }

    public void Gameover()
    {
        btn_Undo.interactable = false;

        if (AdsMenu.activeSelf)
        {
            AdsMenu.SetActive(false);
        }

        gameMenu.SetActive(true);
        gameMenu.transform.GetChild(0).GetChild(6).GetChild(0).GetComponent<Button>().interactable = false;
        gameMenu.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Game Over";
        gameMenu.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().fontSize = 176;
        gameMenu.transform.GetChild(0).GetChild(4).GetComponent<TextMeshProUGUI>().text = "Hit: " + Hit;
        gameMenu.transform.GetChild(0).GetChild(5).GetComponent<TextMeshProUGUI>().text = "Point: " + 0;
        goalKeeper[0].GetComponent<GoalKeeper>().rotateSpeed = 0;
        goal.SetActive(false);
        isFinish = true;
    }

    public void Goal(GameObject throwedCoin)
    {
        btn_Undo.interactable = false;
        isGoal = true;

        throwedCoin.GetComponent<Coin>().hitTakedStarCaunt += 1;

        borders.transform.GetChild(2).GetComponent<BoxCollider2D>().isTrigger = false;

        for (int i = 0; i < takedStarCount; i++)
        {

            gameMenuStarts[i].gameObject.GetComponent<Image>().fillAmount = 1;

        }

        if (throwedCoin.GetComponent<Coin>().isBounce)
        {
            point += (2000 * throwedCoin.GetComponent<Coin>().hitTakedStarCaunt);
        }

        else
        {
            point += (1000 * throwedCoin.GetComponent<Coin>().hitTakedStarCaunt);
        }

        gameMenu.SetActive(true);
        point = point + (remainingHit * 500);

        gameMenu.transform.GetChild(0).GetChild(4).GetComponent<TextMeshProUGUI>().text = "Hit: " + Hit;
        gameMenu.transform.GetChild(0).GetChild(5).GetComponent<TextMeshProUGUI>().text = "Point: " + point;

        goalKeeper[0].GetComponent<GoalKeeper>().rotateSpeed = 0;
        goalKeeper[0].GetComponent<BoxCollider2D>().enabled = true;
        goalKeeper[0].GetComponentInChildren<BoxCollider2D>().isTrigger = true;
        isFinish = true;
        SaveData();


    }

    public void SaveVariable()
    {

        if (move == 0)
        {
            if (RayCastManager._Instance.isStopped(coins[0]) && RayCastManager._Instance.isStopped(coins[1]) && RayCastManager._Instance.isStopped(coins[2]) && savecontrol == 0)
            {
                if (a == 0)
                {
                    UndoAction._Instance.SaveVariableArray(coins);
                    UndoAction._Instance.SaveVariableArray(stars);
                    UndoAction._Instance.SaveVariableArray(woods);
                    UndoAction._Instance.SaveVariableArray(canvasStars);
                    UndoAction._Instance.SaveVariableArray(goalKeeper);

                    UndoAction._Instance.SaveVariable(point, takedStarCount, Hit, remainingHit);
                    a = 1;
                }

            }
            else if (!RayCastManager._Instance.isStopped(coins[0]) || !RayCastManager._Instance.isStopped(coins[1]) || !RayCastManager._Instance.isStopped(coins[2]))
            {

                savecontrol++;

            }

            else if (savecontrol > 0 && RayCastManager._Instance.isStopped(coins[0]) && RayCastManager._Instance.isStopped(coins[1]) && RayCastManager._Instance.isStopped(coins[2]))
            {

                if (Hit != 0)
                {
                    btn_Undo.interactable = true;
                }

                savecontrol = 0;
                move = 1;
                a = 0;
            }

        }
    }

    public void LoadVariable()
    {
        Undo = PlayerPrefs.GetInt("Undo");
        undoCont = true;
        if (Undo >= 1)
        {
            if (AdsMenu.activeSelf)
            {
                AdsMenu.SetActive(false);
            }


            UndoAction._Instance.LoadVariableArray(coins);
            UndoAction._Instance.LoadVariableArray(stars);
            UndoAction._Instance.LoadVariableArray(woods);
            UndoAction._Instance.LoadVariableArray(canvasStars);
            UndoAction._Instance.LoadVariableArray(goalKeeper);
            goalKeeper[0].GetComponent<GoalKeeper>().rotateSpeed = 50;



            point = UndoAction._Instance.LoadVariable(point, takedStarCount, Hit, remainingHit)[0];
            if (takedStarCount != UndoAction._Instance.LoadVariable(point, takedStarCount, Hit, remainingHit)[1])
            {
                for (int i = 0; i < takedStarCount; i++)
                {
                    canvasStars[i].GetComponent<Image>().fillAmount = 0;
                }
                takedStarCount = UndoAction._Instance.LoadVariable(point, takedStarCount, Hit, remainingHit)[1];


            }
            Hit = UndoAction._Instance.LoadVariable(point, takedStarCount, Hit, remainingHit)[2];
            remainingHit = UndoAction._Instance.LoadVariable(point, takedStarCount, Hit, remainingHit)[3];

            Undo -= 1;
            PlayerPrefs.SetInt("Undo", Undo);
            btn_Undo.interactable = false;
            usedUndo = true;
        }
        else
        {
            txt_OverorContinue.text = "Continue";



            AdsMenu.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "UNDO";
            AdsMenu.transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text = "You don't have any undo's left. Would you like to earn one ? ";
            AdsMenu.transform.GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Watch Ads";


            AdsMenu.SetActive(true);
            Time.timeScale = 0;
            AdsMenu.transform.GetChild(0).GetChild(4).gameObject.SetActive(false);
            AdsMenu.transform.GetChild(0).GetChild(4).GetChild(0).GetComponent<Image>().fillAmount = 0;
           
        }

        if (!goal.activeSelf)
        {
            goal.SetActive(true);
        }

    }

    public void GameOverorContinue()
    {
        if (txt_OverorContinue.text == "Game Over")
        {
            Gameover();
        }
        else
        {
            Time.timeScale = 1;
            AdsMenu.SetActive(false);
        }

    }

    public void WatchAds()
    {
        if (Undo == 0)
        {
            AdsMenu.transform.GetChild(0).GetChild(3).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Ads Waiting";
            AdsMenu.transform.GetChild(0).GetChild(3).GetChild(0).GetComponent<Button>().interactable = false;
            GameObject.FindGameObjectWithTag("AdmobReward").GetComponent<AdmobReward>().RequestRewardAds();
            AdsMenu.transform.GetChild(0).GetChild(3).GetChild(0).GetComponent<Button>().interactable = true;

        }
        else
        {
            LoadVariable();

        }


    }

    public void Sorry()
    {
        AdsMenu.SetActive(true);
        AdsMenu.transform.GetChild(1).GetComponent<Text>().text = "Sorry";
        AdsMenu.transform.GetChild(2).GetComponent<Text>().text = "Ads Not Loading";
        AdsMenu.transform.GetChild(3).GetChild(0).GetChild(0).GetComponent<Text>().text = "Try Again";

    }

    public void Continue()
    {
        Time.timeScale = 1;
        AdsMenu.SetActive(false);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void NextLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex != 25)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            SceneManager.LoadScene("Level_1");
        }
    }

    public void SaveData()
    {
        PlayerPrefs.SetInt(SceneManager.GetActiveScene().name+"Pass", 1);
        if (PlayerPrefs.GetInt("Level") <= SceneManager.GetActiveScene().buildIndex)
        {
            PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);
        }

        if (PlayerPrefs.GetInt(SceneManager.GetActiveScene().name + "_Point") <= point)
        {
            PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_Point", point);
            PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_Hit", Hit);
            PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_TakedStar", takedStarCount);
        }

    }

    public void Sound_On_Off(Button btn)
    {
        if (isSoundOn)
        {
            isSoundOn = false;
            btn.GetComponent<Image>().sprite = imgSound[1];
            PlayerPrefs.SetInt("Sound", 0);
        }
        else
        {
            isSoundOn = true;
            btn.GetComponent<Image>().sprite = imgSound[0];
            PlayerPrefs.SetInt("Sound", 1);
        }

    }

    public void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("Repetition", 0);
        PlayerPrefs.SetInt("Sound", 1);
    }
}
