using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    Rigidbody2D rigid;
    [SerializeField]
    private Transform direction;

    int layerChangeControl = 0;



    AudioSource Sound;





    float startTime = 0;
    float elapsedTime;
    float hitspeed;

    public bool isBounce;

    public int hitTakedStarCaunt;





    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        Sound = GetComponent<AudioSource>();

    }


    void FixedUpdate()
    {


        startTime = Time.time;
        if (layerChangeControl >= 1)
        {
            if (rigid.velocity.x < 0.005157948f && rigid.velocity.y < 0.004095177f)
            {


                if ((startTime == (int)startTime))
                {
                    elapsedTime += 1;

                }

                if (elapsedTime >= 2)
                {

                    rigid.angularVelocity = 0;

                    rigid.velocity = Vector2.zero;
                }

            }
            StartCoroutine("ChangeLayer");
        }


        if (RayCastManager._Instance.isStopped(this.gameObject))
        {

            isBounce = false;
            hitTakedStarCaunt = 0;

        }

        if (GameManager._Instance.isSoundOn)
        {
            Sound.mute = false;
        }
        else
        {
            Sound.mute = true;
        }

    }



    public void ThrowCoin(float throwSpeed)
    {

        Sound.Play();
        hitspeed = throwSpeed;

        throwSpeed = 0;
        Vector2 directionVector = direction.position - transform.position;

        hitspeed *= 15f;


        rigid.AddForce(directionVector * hitspeed * 20);
        rigid.AddTorque(1 * hitspeed * 5 * Time.deltaTime);
        this.gameObject.layer = 10;
        elapsedTime = 0;

        layerChangeControl++;

        hitspeed = 0;
        UIManager._Instance.DeselectedCoin(this.transform);
    }


    IEnumerator ChangeLayer()
    {

        yield return new WaitForSeconds(0.1f);

        if (RayCastManager._Instance.isStopped(this.gameObject))
        {

            GameManager._Instance.CoinsLayer(this.gameObject);
            layerChangeControl = 0;

        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Border" && !GameManager._Instance.isFinish)
        {

            GameManager._Instance.AdsShow();

        }

        if (collision.tag == "Receivable" && !RayCastManager._Instance.isStopped(this.gameObject))
        {

            hitTakedStarCaunt++;
            collision.gameObject.SetActive(false);
            GameManager._Instance.TakeStar(this.gameObject);


        }
        if (collision.tag == "Goal" && (this.gameObject.layer == 0 || this.gameObject.layer == 10))
        {
            Destroy(collision.gameObject);
            if (RayCastManager._Instance.isPassed)
            {
                GameManager._Instance.Goal(this.gameObject);

            }
            else
            {
                GameManager._Instance.AdsShow();

            }

        }



    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wood")
        {

            isBounce = true;
        }

        if (collision.gameObject.GetComponent<Coin>() != null)
        {

            Sound.Play();

            isBounce = true;
        }
    }
}
