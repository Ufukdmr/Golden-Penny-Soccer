using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Receivable : MonoBehaviour
{
    [SerializeField]
    private bool isMoving_x;
    [SerializeField]
    private bool isMoving_y;

    [SerializeField]
    private float maxDistance;
    [SerializeField]
    private float minDistance;

    public int direction;

    [SerializeField]
    private int moveSpeed;


    private int rotateSpeed;

    Rigidbody2D rigid;



    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        rotateSpeed = 2;
    }


    void FixedUpdate()
    {
        transform.Rotate(new Vector3(0, 0, 45) * rotateSpeed * Time.fixedDeltaTime * 1);

        if (isMoving_x)
        {
            isMoving_y = false;

            rigid.velocity = new Vector2(direction, 0) * moveSpeed * Time.fixedDeltaTime;

            if (transform.position.x > maxDistance)
            {
                direction = -1;
            }
            else if (transform.position.x < minDistance)
            {
                direction = 1;
            }
        }
        else if (isMoving_y)
        {
            isMoving_x = false;
            rigid.velocity = new Vector2(0, direction) * moveSpeed * Time.fixedDeltaTime;

            if (transform.position.y > maxDistance)
            {
                direction = -1;
            }
            else if (transform.position.y < minDistance)
            {
                direction = 1;
            }
        }

    }
}
