using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UndoAction : MonoBehaviour
{
    //Coins
    Vector2[] coinsFirstPosition;
    Quaternion[] coinsFirstRotation;
    int[] coinsFirstLayer;
    //Coins

    //Stars
    Vector2[] starsFirstPosition;
    Quaternion[] starsFirstRotation;
    bool[] starsSetActive;
    float[] canvasStarsFill;
    int[] starsDirection;
    //Stars

    //Wood
    Vector2[] woodFirstPosition;
    Quaternion[] woodFirstRotation;
    int[] woodFirstFirstDirection;
    int[] woodFirstDirection;
    bool[] wood_isMoving_x_value;
    bool[] wood_isMoving_y_value;
    bool[] wood_isRotate_value;
    bool[] wood_moveAround_value;
    bool[] wood_halfMoveAround_value;
    bool[] wood_rigthBottom_value;
    bool[] wood_leftTop_value;
    bool[] wood_isRotateBack_value;
    int[] wood_firstRotateCount;
    int[] wood_firstmoveCount;
    float[] wood_MoveSpeed;
    float[] wood_RotateSpeed;
    //Wood

    //GoalKeeper
    Vector2 goalKeeperFirstPosition;
    Quaternion goalKeeperFirstRotation;
    int goalKeeperFirstDirection;
    int goalKeeperFirstSpeed;
    //GoalKeeper

    private int firstPoint;
    private int firstTakedStarCount;
    int firstHit;
    int firstremaningHit;

    bool firstIsFinish;

    private static UndoAction instance;

    public static UndoAction _Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<UndoAction>();
            }
            return instance;
        }
    }

    public void SaveVariableArray(GameObject[] gameObjects)
    {
        if (gameObjects.Length != 0)
        {
            if (gameObjects[0].tag == "Coin_1")
            {
                coinsFirstLayer = new int[gameObjects.Length];
                coinsFirstPosition = new Vector2[gameObjects.Length];
                coinsFirstRotation = new Quaternion[gameObjects.Length];

                for (int i = 0; i < gameObjects.Length; i++)
                {
                    coinsFirstLayer[i] = gameObjects[i].layer;
                    coinsFirstPosition[i] = gameObjects[i].transform.position;
                    coinsFirstRotation[i] = gameObjects[i].transform.rotation;

                }
            }
            else if (gameObjects[0].tag == "Receivable")
            {
                starsFirstPosition = new Vector2[gameObjects.Length];
                starsFirstRotation = new Quaternion[gameObjects.Length];
                starsDirection = new int[gameObjects.Length];
                starsSetActive = new bool[gameObjects.Length];
                for (int i = 0; i < gameObjects.Length; i++)
                {

                    starsFirstPosition[i] = gameObjects[i].transform.position;
                    starsFirstRotation[i] = gameObjects[i].transform.rotation;
                    starsDirection[i] = gameObjects[i].GetComponent<Receivable>().direction;
                    starsSetActive[i] = gameObjects[i].activeSelf;

                }
            }
            else if (gameObjects[0].tag == "Wood")
            {
                woodFirstPosition = new Vector2[gameObjects.Length];
                woodFirstRotation = new Quaternion[gameObjects.Length];
                woodFirstFirstDirection = new int[gameObjects.Length];
                woodFirstDirection = new int[gameObjects.Length];
                wood_isMoving_x_value = new bool[gameObjects.Length];
                wood_isMoving_y_value = new bool[gameObjects.Length];
                wood_isRotate_value = new bool[gameObjects.Length];
                wood_rigthBottom_value = new bool[gameObjects.Length];
                wood_leftTop_value = new bool[gameObjects.Length];
                wood_isRotateBack_value = new bool[gameObjects.Length];
                wood_firstRotateCount = new int[gameObjects.Length];
                wood_firstmoveCount = new int[gameObjects.Length];
                wood_MoveSpeed = new float[gameObjects.Length];
                wood_RotateSpeed = new float[gameObjects.Length];
                for (int i = 0; i < gameObjects.Length; i++)
                {

                    woodFirstPosition[i] = gameObjects[i].transform.position;
                    woodFirstRotation[i] = gameObjects[i].transform.rotation;
                    woodFirstDirection[i] = gameObjects[i].GetComponent<Wood>().direction;
                    woodFirstFirstDirection[i] = gameObjects[i].GetComponent<Wood>().firstdirection;
                    wood_firstmoveCount[i] = gameObjects[i].GetComponent<Wood>().movecount;
                    wood_firstRotateCount[i] = gameObjects[i].GetComponent<Wood>().rotatecount;
                    wood_isMoving_x_value[i] = gameObjects[i].GetComponent<Wood>().isMoving_x;
                    wood_isMoving_y_value[i] = gameObjects[i].GetComponent<Wood>().isMoving_y;
                    wood_isRotateBack_value[i] = gameObjects[i].GetComponent<Wood>().isRotateBack;
                    wood_isRotate_value[i] = gameObjects[i].GetComponent<Wood>().isRotate;
                    wood_leftTop_value[i] = gameObjects[i].GetComponent<Wood>().leftTop;
                    wood_rigthBottom_value[i] = gameObjects[i].GetComponent<Wood>().rightBottom;
                    wood_MoveSpeed[i] = gameObjects[i].GetComponent<Wood>().moveSpeed;
                    wood_RotateSpeed[i] = gameObjects[i].GetComponent<Wood>().rotateSpeed;

                }
            }
            else if (gameObjects[0].tag == "CanvasStar")
            {
                canvasStarsFill = new float[gameObjects.Length];
                for (int i = 0; i < gameObjects.Length; i++)
                {
                    canvasStarsFill[i] = gameObjects[i].GetComponent<Image>().fillAmount;
                }
            }

            else if (gameObjects[0].tag == "GoalKeeper")
            {

                goalKeeperFirstPosition = gameObjects[0].transform.position;
                goalKeeperFirstRotation = gameObjects[0].transform.rotation;
                goalKeeperFirstDirection = gameObjects[0].GetComponent<GoalKeeper>().rotateDirection;
                goalKeeperFirstSpeed = gameObjects[0].GetComponent<GoalKeeper>().rotateSpeed;

            }
        }

    }
    public void SaveVariable(int point, int takedStarCount, int hit, int remaningHit)
    {
        firstPoint = point;
        firstTakedStarCount = takedStarCount;
        firstHit = hit;
        firstremaningHit = remaningHit;
        firstIsFinish = GameManager._Instance.isFinish;

    }

    public void LoadVariableArray(GameObject[] gameObjects)
    {
        if (gameObjects.Length != 0)
        {
            if (gameObjects[0].tag == "Coin_1")
            {
                for (int i = 0; i < gameObjects.Length; i++)
                {
                    gameObjects[i].layer = coinsFirstLayer[i];
                    gameObjects[i].transform.position = coinsFirstPosition[i];
                    gameObjects[i].transform.rotation = coinsFirstRotation[i];
                    gameObjects[i].GetComponent<Rigidbody2D>().angularDrag = 0.7f;
                }
            }
            else if (gameObjects[0].tag == "Receivable")
            {

                for (int i = 0; i < gameObjects.Length; i++)
                {

                    gameObjects[i].transform.position = starsFirstPosition[i];
                    gameObjects[i].transform.rotation = starsFirstRotation[i];
                    gameObjects[i].GetComponent<Receivable>().direction = starsDirection[i];
                    gameObjects[i].SetActive(starsSetActive[i]);

                }
            }
            else if (gameObjects[0].tag == "Wood")
            {
                for (int i = 0; i < gameObjects.Length; i++)
                {

                    gameObjects[i].transform.position = woodFirstPosition[i];
                    gameObjects[i].transform.rotation = woodFirstRotation[i];
                    gameObjects[i].GetComponent<Wood>().direction = woodFirstDirection[i];
                    gameObjects[i].GetComponent<Wood>().firstdirection = woodFirstFirstDirection[i];
                    gameObjects[i].GetComponent<Wood>().movecount = wood_firstmoveCount[i];
                    gameObjects[i].GetComponent<Wood>().rotatecount = wood_firstRotateCount[i];
                    gameObjects[i].GetComponent<Wood>().isMoving_x = wood_isMoving_x_value[i];
                    gameObjects[i].GetComponent<Wood>().isMoving_y = wood_isMoving_y_value[i];
                    gameObjects[i].GetComponent<Wood>().isRotateBack = wood_isRotateBack_value[i];
                    gameObjects[i].GetComponent<Wood>().isRotate = wood_isRotate_value[i];
                    gameObjects[i].GetComponent<Wood>().leftTop = wood_leftTop_value[i];
                    gameObjects[i].GetComponent<Wood>().rightBottom = wood_rigthBottom_value[i];
                    gameObjects[i].GetComponent<Wood>().moveSpeed = wood_MoveSpeed[i];
                    gameObjects[i].GetComponent<Wood>().rotateSpeed = wood_RotateSpeed[i];

                }
            }
            else if (gameObjects[0].tag == "CanvasStar")
            {
                canvasStarsFill = new float[gameObjects.Length];
                for (int i = 0; i < gameObjects.Length; i++)
                {
                    gameObjects[i].GetComponent<Image>().fillAmount = canvasStarsFill[i];
                }
            }

            else if (gameObjects[0].tag == "GoalKeeper")
            {
                Debug.Log("Rotation:" + gameObjects[0].transform.rotation.z + "firstrotaation:" + goalKeeperFirstRotation.z);
                gameObjects[0].transform.position = goalKeeperFirstPosition;
                gameObjects[0].transform.rotation = goalKeeperFirstRotation;
                gameObjects[0].GetComponent<GoalKeeper>().rotateDirection = goalKeeperFirstDirection;
                gameObjects[0].GetComponent<GoalKeeper>().rotateSpeed = goalKeeperFirstSpeed;
            }

        }

    }
    public int[] LoadVariable(int point, int takedStarCount, int hit, int remaningHit)
    {
        point = firstPoint;
        takedStarCount = firstTakedStarCount;
        hit = firstHit;
        remaningHit = firstremaningHit;
        GameManager._Instance.isFinish = firstIsFinish;

        int[] variables = new int[4] { point, takedStarCount, hit, remaningHit };


        return variables;

    }
}
