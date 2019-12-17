using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalKeeper : MonoBehaviour
{
    Rigidbody2D rigid;

    public int rotateDirection;

    public int rotateSpeed = 50;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        rotateDirection = 1;
    }


    void FixedUpdate()
    {
        this.transform.Rotate(new Vector3(0, 0, rotateSpeed) * rotateDirection * Time.deltaTime);

        if (this.transform.rotation.z > 0.35f)
        {

            rotateDirection = -1;
        }
        else if (this.transform.rotation.z < -0.35)
        {
            rotateDirection = 1;
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Catch")
        {

            GameManager._Instance.AdsShow();
            collision.gameObject.GetComponentInParent<Rigidbody2D>().velocity = new Vector2(0, 0);
            rotateSpeed = 0;
            collision.gameObject.GetComponentInParent<Rigidbody2D>().angularDrag = 100;
        }
    }
}
