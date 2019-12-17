using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] coins;

    private GameObject selectedCoin;

    public bool isPassed;

    private static RayCastManager instance;

    public static RayCastManager _Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<RayCastManager>();
            }
            return instance;
        }
    }

    public bool isStopped(GameObject throwedCoin)
    {
        if (throwedCoin.GetComponent<Rigidbody2D>().IsSleeping())
        {
            return true;
        }
        else
        {
            if (throwedCoin.GetComponent<Rigidbody2D>().velocity == Vector2.zero)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }


    private void Update()
    {
        if (isStopped(coins[0]) && isStopped(coins[1]) && isStopped(coins[2]))

        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, 256);
                if (hit)
                {
                    selectedCoin = hit.collider.gameObject;
                }

            }
        }

        if (selectedCoin != null)
        {

            if (coins[0].tag == selectedCoin.tag)
            {
                //1024
                Vector2 Startpos = coins[1].transform.position;
                Vector2 direction = (coins[2].transform.position - coins[1].transform.position);
                Ray2D ray = new Ray2D(Startpos, direction);
                Debug.DrawRay(ray.origin, direction, Color.red);
                RaycastHit2D hit = Physics2D.Raycast(ray.origin, direction, GameManager._Instance.CoinsDistance(selectedCoin), layerMask: (1 << 10) | (1 << 256));

                if (hit)
                {


                    if (hit.collider.tag == coins[0].tag)
                    {


                        isPassed = true;

                        GameManager._Instance.HitPointCount(coins[0]);
                        selectedCoin = hit.collider.gameObject;
                        selectedCoin = null;

                    }

                }

                else if (!hit)
                {
                    if (isStopped(coins[0]) && coins[0].layer == 9)
                    {
                        isPassed = false;
                        GameManager._Instance.HitPointCount(coins[0]);
                        hit = new RaycastHit2D();
                        selectedCoin = null;
                        GameManager._Instance.AdsShow();
                    }
                }

            }
            else if (coins[1].tag == selectedCoin.tag)
            {
                Vector2 Startpos = coins[2].transform.position;
                Vector2 direction = (coins[0].transform.position - coins[2].transform.position);
                Ray2D ray = new Ray2D(Startpos, direction);
                Debug.DrawRay(ray.origin, direction, Color.red);
                RaycastHit2D hit = Physics2D.Raycast(ray.origin, direction, GameManager._Instance.CoinsDistance(selectedCoin), layerMask: (1 << 10) | (1 << 256));

                if (hit)
                {

                    if (hit.collider.tag == coins[1].tag)
                    {

                        isPassed = true;

                        GameManager._Instance.HitPointCount(coins[1]);
                        selectedCoin = hit.collider.gameObject;
                        selectedCoin = null;

                    }

                }

                else if (!hit)
                {
                    if (isStopped(coins[1]) && coins[1].layer == 9)
                    {
                        isPassed = false;
                        GameManager._Instance.HitPointCount(coins[1]);
                        hit = new RaycastHit2D();
                        selectedCoin = null;
                        GameManager._Instance.AdsShow();
                    }
                }
            }
            else if (coins[2].tag == selectedCoin.tag)
            {

                Vector2 Startpos = coins[0].transform.position;
                Vector2 direction = (coins[1].transform.position - coins[0].transform.position);
                Ray2D ray = new Ray2D(Startpos, direction);
                Debug.DrawRay(ray.origin, direction, Color.red);
                RaycastHit2D hit = Physics2D.Raycast(ray.origin, direction, GameManager._Instance.CoinsDistance(selectedCoin), layerMask: (1 << 10) | (1 << 256));

                if (hit)
                {

                    if (hit.collider.tag == coins[2].tag)
                    {

                        isPassed = true;

                        GameManager._Instance.HitPointCount(coins[2]);
                        selectedCoin = hit.collider.gameObject;
                        selectedCoin = null;

                    }

                }

                else if (!hit)
                {
                    if (isStopped(coins[2]) && coins[2].layer == 9)
                    {
                        isPassed = false;
                        GameManager._Instance.HitPointCount(coins[2]);
                        hit = new RaycastHit2D();
                        selectedCoin = null;
                        GameManager._Instance.AdsShow();
                    }
                }
            }
        }

    }
}
