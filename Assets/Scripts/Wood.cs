using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wood : MonoBehaviour
{
    Rigidbody2D rigid;

    public bool isMoving_x;

    public bool isMoving_y;

    public bool isRotate;

    public bool movearound;

    public bool halfMoveAround;

    public bool rightBottom;

    public bool leftTop;

    public float rotateSpeed;

    public float moveSpeed;

    [SerializeField]
    private float maxDistance_y;

    [SerializeField]
    private float minDistance_y;

    [SerializeField]
    private float maxDistance_x;

    [SerializeField]
    private float minDistance_x;

    public int direction;

    public int movecount;

    AudioSource sound;

    public int firstdirection;

    Vector3 zero = new Vector3(0, 0, 0);

    public bool isRotateBack = false;

    public int rotatecount;


    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();

        sound = GetComponent<AudioSource>();

        firstdirection = direction;
        transform.Rotate(zero);

    }


    void FixedUpdate()
    {

        if (isRotate && !movearound && !halfMoveAround)
        {
            isMoving_x = false;
            isMoving_y = false;
            transform.Rotate(new Vector3(0, 0, 45) * rotateSpeed * Time.fixedDeltaTime * direction);
        }
        else if (isMoving_x && !movearound && !halfMoveAround)
        {
            isRotate = false;
            isMoving_y = false;
            rigid.velocity = new Vector2(direction, 0) * moveSpeed * Time.fixedDeltaTime;

            if (transform.position.x > maxDistance_x)
            {
                direction = -1;
            }
            else if (transform.position.x < minDistance_x)
            {
                direction = 1;
            }
        }
        else if (isMoving_y && !movearound && !halfMoveAround)
        {
            isRotate = false;
            isMoving_x = false;

            rigid.velocity = new Vector2(0, direction) * moveSpeed * Time.fixedDeltaTime;

            if (transform.position.y > maxDistance_y)
            {
                direction = -1;
            }
            else if (transform.position.y < minDistance_y)
            {
                direction = 1;
            }
        }
        else if (movearound && rightBottom)
        {

            if (isMoving_x)
            {

                rigid.velocity = new Vector2(direction, 0) * moveSpeed * Time.deltaTime;

                if (firstdirection == -1)
                {

                    if (isRotate)
                    {
                        if (direction == -1)
                        {


                            if (transform.eulerAngles.z < 180)
                            {
                                transform.Rotate(zero);
                                isRotate = false;
                                transform.eulerAngles = new Vector3(0, 0, 180);
                            }
                            else
                            {


                                if (rotatecount % 2 != 0 || rotatecount == 0)
                                {
                                    rotateSpeed = rotateSpeed;
                                    transform.Rotate(new Vector3(0, 0, 45) * rotateSpeed * Time.fixedDeltaTime * direction);

                                }
                                else
                                {

                                    transform.eulerAngles = new Vector3(0, 0, 180);
                                    movecount = 0;
                                }


                            }


                        }
                        else
                        {

                            if (transform.eulerAngles.z > 180)
                            {
                                transform.Rotate(zero);
                                transform.eulerAngles = new Vector3(0, 0, 0);
                                isRotate = false;
                            }
                            else
                            {


                                if (rotatecount % 2 != 0 || rotatecount == 0)
                                {
                                    rotateSpeed = rotateSpeed;
                                    transform.Rotate(new Vector3(0, 0, 45) * rotateSpeed * Time.fixedDeltaTime * direction);
                                }
                                else
                                {
                                    transform.eulerAngles = new Vector3(0, 0, 180);
                                    movecount = 0;
                                }
                            }


                        }

                    }
                    //Right Up Corner
                    if (transform.position.x > maxDistance_x && transform.position.y > maxDistance_y && !isRotateBack)
                    {
                        if (halfMoveAround)
                        {
                            if (!isRotateBack || rotatecount % 2 == 0)
                            {
                                movecount += 1;
                                rotatecount += 1;
                            }
                            if (movecount == 2)
                            {
                                rightBottom = false;
                                leftTop = true;
                                isRotateBack = true;
                            }
                            else
                            {
                                isRotateBack = false;
                            }
                        }
                        if (rotatecount % 2 != 0 || rotatecount == 0)
                        {
                            isMoving_x = false;
                            isMoving_y = true;
                        }
                        direction = -1;

                        isRotate = true;

                    }
                    //Left Down Corner
                    else if (transform.position.x < minDistance_x && transform.position.y < minDistance_y)
                    {

                        if (halfMoveAround)
                        {
                            if (!isRotateBack || rotatecount % 2 == 0)
                            {
                                movecount += 1;
                                rotatecount += 1;
                            }

                            if (movecount == 2)
                            {
                                rightBottom = false;
                                leftTop = true;
                                isRotateBack = true;

                            }
                            else
                            {
                                isRotateBack = false;
                            }
                        }

                        if (rotatecount % 2 != 0 || rotatecount == 0)
                        {
                            isMoving_x = false;
                            isMoving_y = true;
                        }
                        direction = 1;

                        isRotate = true;
                    }
                }
                else
                {
                    if (isRotate)
                    {
                        if (direction == -1)
                        {


                            if (transform.eulerAngles.z > 180)
                            {
                                transform.Rotate(zero);
                                isRotate = false;

                                transform.eulerAngles = new Vector3(0, 0, 180);


                            }
                            else
                            {

                                if (rotatecount % 2 != 0 || rotatecount == 0)
                                {
                                    rotateSpeed = rotateSpeed;
                                    transform.Rotate(new Vector3(0, 0, 45) * rotateSpeed * Time.fixedDeltaTime * -direction);
                                }
                                else
                                {
                                    transform.eulerAngles = new Vector3(0, 0, 180);

                                    transform.position = new Vector2(transform.position.x, maxDistance_y + 0.04f);


                                    movecount = 0;
                                }
                            }



                        }
                        else
                        {

                            if (transform.eulerAngles.z < 180)
                            {

                                if (transform.eulerAngles.z > 0)
                                {

                                    transform.Rotate(zero);
                                    transform.eulerAngles = new Vector3(0, 0, 0);
                                    isRotate = false;
                                }


                            }
                            else
                            {

                                if (rotatecount % 2 != 0 || rotatecount == 0)
                                {
                                    rotateSpeed = rotateSpeed;
                                    transform.Rotate(new Vector3(0, 0, 45) * rotateSpeed * Time.fixedDeltaTime * direction);
                                }
                                else
                                {
                                    transform.eulerAngles = new Vector3(0, 0, 0);
                                    movecount = 0;
                                }
                            }

                        }
                    }
                    //Right Down Corner
                    if (transform.position.y < minDistance_y && transform.position.x > maxDistance_x)
                    {
                        if (halfMoveAround)
                        {
                            if (!isRotateBack || rotatecount % 2 == 0)
                            {
                                movecount += 1;
                                rotatecount += 1;
                            }
                            if (movecount == 2)
                            {
                                leftTop = true;
                                rightBottom = false;
                                isRotateBack = true;
                                movecount = 0;
                            }
                            else
                            {
                                isRotateBack = false;
                            }
                        }
                        if (rotatecount % 2 != 0 || rotatecount == 0)
                        {
                            isMoving_x = false;
                            isMoving_y = true;
                        }
                        direction = 1;

                        isRotate = true;


                    }
                    //Left Up Corner
                    else if (transform.position.y > maxDistance_y && transform.position.x < minDistance_x)
                    {

                        if (halfMoveAround)
                        {
                            if (!isRotateBack || rotatecount % 2 == 0)
                            {
                                movecount += 1;
                                rotatecount += 1;
                            }
                            if (movecount == 2)
                            {

                                leftTop = true;
                                rightBottom = false;
                                isRotateBack = true;
                                movecount = 0;

                            }
                            else
                            {
                                isRotateBack = false;
                            }

                        }
                        if (rotatecount % 2 != 0 || rotatecount == 0)
                        {
                            isMoving_x = false;
                            isMoving_y = true;
                            direction = -1;
                        }
                        else
                        {
                            direction = 1;
                        }


                        isRotate = true;

                    }

                }
            }
            else if (isMoving_y)
            {
                rigid.velocity = new Vector2(0, direction) * moveSpeed * Time.deltaTime;


                if (firstdirection == -1)
                {
                    if (isRotate)
                    {
                        if (direction == -1)
                        {

                            if (transform.eulerAngles.z > 90)
                            {

                                transform.Rotate(zero);
                                transform.eulerAngles = new Vector3(0, 0, 270);
                                isRotate = false;
                            }
                            else
                            {

                                if (rotatecount % 2 != 0 || rotatecount == 0)
                                {

                                    transform.Rotate(new Vector3(0, 0, 45) * rotateSpeed * Time.fixedDeltaTime * -direction);
                                }
                                else
                                {

                                    transform.eulerAngles = new Vector3(0, 0, 270);
                                    movecount = 0;
                                }

                            }

                        }
                        else
                        {


                            if (transform.eulerAngles.z < 90)
                            {

                                transform.Rotate(zero);
                                transform.eulerAngles = new Vector3(0, 0, 90);
                                isRotate = false;
                            }
                            else
                            {

                                if (rotatecount % 2 != 0 || rotatecount == 0)
                                {
                                    rotateSpeed = rotateSpeed;
                                    transform.Rotate(new Vector3(0, 0, 45) * rotateSpeed * Time.fixedDeltaTime * -direction);
                                }
                                else
                                {
                                    transform.eulerAngles = new Vector3(0, 0, 90);
                                    movecount = 0;
                                }
                            }

                        }

                    }
                    //Right Down Corner
                    if (transform.position.y < minDistance_y && transform.position.x > maxDistance_x)
                    {
                        if (halfMoveAround)
                        {
                            if (!isRotateBack || rotatecount % 2 == 0)
                            {
                                movecount += 1;
                                rotatecount += 1;
                            }
                            if (movecount == 2)
                            {
                                leftTop = true;
                                rightBottom = false;
                                isRotateBack = true;
                                movecount = 0;
                            }
                            else
                            {
                                isRotateBack = false;
                            }
                        }
                        if (rotatecount % 2 != 0 || rotatecount == 0)
                        {

                            isMoving_x = true;
                            isMoving_y = false;

                        }

                        direction = -1;
                        isRotate = true;


                    }
                    //Left Up Corner
                    else if (transform.position.y > maxDistance_y && transform.position.x < minDistance_x)
                    {
                        if (halfMoveAround)
                        {
                            if (!isRotateBack || rotatecount % 2 == 0)
                            {
                                movecount += 1;
                                rotatecount += 1;
                            }
                            if (movecount == 2)
                            {
                                rightBottom = false;
                                leftTop = true;
                                isRotateBack = true;
                                movecount = 0;

                            }
                            else
                            {
                                isRotateBack = false;
                            }
                        }
                        if (rotatecount % 2 != 0 || rotatecount == 0)
                        {

                            isMoving_x = true;
                            isMoving_y = false;
                            direction = 1;

                        }
                        else
                        {
                            direction = -1;
                        }

                        isRotate = true;

                    }


                }

                else
                {
                    if (isRotate)
                    {
                        if (direction == -1)
                        {

                            if (transform.eulerAngles.z > 270)
                            {

                                transform.Rotate(zero);
                                transform.eulerAngles = new Vector3(0, 0, 270);
                                isRotate = false;
                            }
                            else
                            {

                                if (rotatecount % 2 != 0 || rotatecount == 0)
                                {
                                    rotateSpeed = rotateSpeed;
                                    transform.Rotate(new Vector3(0, 0, 45) * rotateSpeed * Time.fixedDeltaTime * -direction);
                                }
                                else
                                {
                                    transform.eulerAngles = new Vector3(0, 0, 90);
                                    movecount = 0;
                                }

                            }

                        }
                        else
                        {



                            if (transform.eulerAngles.z > 90)
                            {

                                transform.Rotate(zero);
                                transform.eulerAngles = new Vector3(0, 0, 90);
                                isRotate = false;
                            }
                            else
                            {

                                if (rotatecount % 2 != 0 || rotatecount == 0)
                                {
                                    rotateSpeed = rotateSpeed;
                                    transform.Rotate(new Vector3(0, 0, 45) * rotateSpeed * Time.fixedDeltaTime * direction);
                                }
                                else
                                {
                                    transform.eulerAngles = new Vector3(0, 0, 90);
                                    movecount = 0;
                                }
                            }

                        }

                    }


                    //Right Up Corner
                    if (transform.position.x > maxDistance_x && transform.position.y > maxDistance_y)
                    {
                        if (halfMoveAround)
                        {
                            if (!isRotateBack || rotatecount % 2 == 0)
                            {
                                movecount += 1;
                                rotatecount += 1;
                            }
                            if (movecount == 2)
                            {
                                leftTop = true;
                                rightBottom = false;
                                isRotateBack = true;
                                movecount = 0;
                            }
                            else
                            {
                                isRotateBack = false;
                            }
                        }

                        if (rotatecount % 2 != 0 || rotatecount == 0)
                        {
                            isMoving_x = true;
                            isMoving_y = false;
                        }
                        direction = -1;

                        isRotate = true;

                    }
                    //Left Down Corner
                    else if (transform.position.x < minDistance_x && transform.position.y < minDistance_y)
                    {
                        if (halfMoveAround)
                        {
                            if (!isRotateBack || rotatecount % 2 == 0)
                            {
                                movecount += 1;
                                rotatecount += 1;
                            }
                            if (movecount == 2)
                            {
                                rightBottom = false;
                                leftTop = true;
                                isRotateBack = true;
                                movecount = 0;

                            }
                            else
                            {
                                isRotateBack = false;
                            }
                        }
                        if (rotatecount % 2 != 0 || rotatecount == 0)
                        {
                            isMoving_x = true;
                            isMoving_y = false;
                        }
                        direction = 1;

                        isRotate = true;
                    }
                }
            }

        }
        else if (movearound && leftTop)
        {

            if (isMoving_x)
            {

                rigid.velocity = new Vector2(direction, 0) * moveSpeed * Time.deltaTime;

                if (firstdirection == -1)
                {
                    if (isRotate)
                    {
                        if (direction == 1)
                        {


                            if (transform.eulerAngles.z < 180)
                            {


                                transform.Rotate(zero);
                                transform.eulerAngles = new Vector3(0, 0, 0);
                                isRotate = false;


                            }
                            else
                            {

                                if (rotatecount % 2 != 0 || rotatecount == 0)
                                {


                                    transform.Rotate(new Vector3(0, 0, 45) * rotateSpeed * Time.fixedDeltaTime * direction);
                                }
                                else
                                {
                                    transform.eulerAngles = new Vector3(0, 0, 0);
                                    movecount = 0;
                                }
                            }

                        }
                        else
                        {

                            if (transform.eulerAngles.z > 179)
                            {
                                transform.Rotate(zero);
                                transform.eulerAngles = new Vector3(0, 0, 180);
                                isRotate = false;

                            }
                            else
                            {

                                if (rotatecount % 2 != 0 || rotatecount == 0)
                                {

                                    transform.Rotate(new Vector3(0, 0, 45) * rotateSpeed * Time.fixedDeltaTime * -direction);
                                }
                                else
                                {
                                    transform.eulerAngles = new Vector3(0, 0, 180);
                                    movecount = 0;
                                }

                            }

                        }

                    }
                    //Left Up Corner
                    if (transform.position.x < minDistance_x && transform.position.y > maxDistance_y)
                    {
                        if (halfMoveAround)
                        {
                            if (!isRotateBack || rotatecount % 2 == 0)
                            {
                                movecount += 1;
                                rotatecount += 1;
                            }

                            if (movecount == 2)
                            {
                                leftTop = false;
                                rightBottom = true;
                                isRotateBack = true;
                                movecount = 0;
                            }
                            else
                            {
                                isRotateBack = false;
                            }
                        }
                        if (rotatecount % 2 != 0 || rotatecount == 0)
                        {
                            isMoving_x = false;
                            isMoving_y = true;
                        }
                        direction = -1;

                        isRotate = true;

                    }
                    //Right Down Corner
                    else if (transform.position.x > maxDistance_x && transform.position.y < minDistance_y)
                    {
                        if (halfMoveAround)
                        {
                            if (!isRotateBack || rotatecount % 2 == 0)
                            {
                                movecount += 1;
                                rotatecount += 1;
                            }
                            if (movecount == 2)
                            {
                                leftTop = false;
                                rightBottom = true;
                                isRotateBack = true;
                                movecount = 0;
                            }
                            else
                            {
                                isRotateBack = false;
                            }
                        }
                        if (rotatecount % 2 != 0 || rotatecount == 0)
                        {
                            isMoving_x = false;
                            isMoving_y = true;
                            direction = 1;
                        }
                        else
                        {
                            direction = -1;
                        }

                        isRotate = true;
                    }
                }
                else
                {
                    if (isRotate)
                    {
                        if (direction == 1)
                        {

                            if (transform.eulerAngles.z > 180)
                            {

                                transform.Rotate(zero);
                                transform.eulerAngles = new Vector3(0, 0, 0);
                                isRotate = false;
                            }
                            else
                            {

                                if (rotatecount % 2 != 0 || rotatecount == 0)
                                {

                                    transform.Rotate(new Vector3(0, 0, 45) * rotateSpeed * Time.fixedDeltaTime * direction);
                                }
                                else
                                {
                                    transform.eulerAngles = new Vector3(0, 0, 0);
                                    movecount = 0;
                                }


                            }

                        }
                        else
                        {



                            if (transform.eulerAngles.z < 180)
                            {

                                isRotate = false;
                                transform.Rotate(zero);
                                transform.eulerAngles = new Vector3(0, 0, 180);

                            }
                            else
                            {

                                if (rotatecount % 2 != 0 || rotatecount == 0)
                                {

                                    transform.Rotate(new Vector3(0, 0, 45) * rotateSpeed * Time.fixedDeltaTime * direction);
                                }
                                else
                                {
                                    transform.eulerAngles = new Vector3(0, 0, 180);
                                    movecount = 0;
                                }
                            }

                        }

                    }
                    //Right Up Corner
                    if (transform.position.y > maxDistance_y && transform.position.x > maxDistance_x)
                    {
                        if (halfMoveAround)
                        {
                            if (!isRotateBack || rotatecount % 2 == 0)
                            {
                                movecount += 1;
                                rotatecount += 1;
                            }
                            if (movecount == 2)
                            {
                                leftTop = false;
                                rightBottom = true;
                                isRotateBack = true;
                                movecount = 0;
                            }
                            else
                            {
                                isRotateBack = false;
                            }
                        }
                        if (rotatecount % 2 != 0 || rotatecount == 0)
                        {
                            isMoving_x = false;
                            isMoving_y = true;
                        }
                        direction = -1;

                        isRotate = true;

                    }
                    //Left Down Corner
                    else if (transform.position.y < minDistance_y && transform.position.x < minDistance_x)
                    {
                        if (halfMoveAround)
                        {
                            if (!isRotateBack || rotatecount % 2 == 0)
                            {
                                movecount += 1;
                                rotatecount += 1;
                            }
                            if (movecount == 2)
                            {
                                leftTop = false;
                                rightBottom = true;
                                isRotateBack = true;
                                movecount = 0;
                            }
                            else
                            {
                                isRotateBack = false;
                            }
                        }
                        if (rotatecount % 2 != 0 || rotatecount == 0)
                        {
                            isMoving_x = false;
                            isMoving_y = true;
                        }
                        direction = 1;

                        isRotate = true;
                    }

                }
            }
            else if (isMoving_y)
            {
                rigid.velocity = new Vector2(0, direction) * moveSpeed * Time.deltaTime;
                //isRotate = true;

                if (firstdirection == -1)
                {
                    if (isRotate)
                    {
                        if (direction == -1)
                        {



                            if (transform.eulerAngles.z > 270)
                            {

                                transform.Rotate(zero);

                                transform.eulerAngles = new Vector3(0, 0, 270);
                                //transform.position = new Vector2(minDistance_x - 0.01f, transform.position.y);
                                isRotate = false;

                            }
                            else
                            {

                                if (rotatecount % 2 != 0 || rotatecount == 0)
                                {


                                    transform.Rotate(new Vector3(0, 0, 45) * rotateSpeed * Time.fixedDeltaTime * -direction);
                                }
                                else
                                {
                                    transform.eulerAngles = new Vector3(0, 0, 270);
                                    movecount = 0;
                                }
                            }

                        }
                        else
                        {



                            if (transform.eulerAngles.z > 90)
                            {

                                transform.Rotate(zero);
                                isRotate = false;
                                transform.eulerAngles = new Vector3(0, 0, 90);
                            }
                            else
                            {


                                if (rotatecount % 2 != 0 || rotatecount == 0)
                                {

                                    transform.Rotate(new Vector3(0, 0, 45) * rotateSpeed * Time.fixedDeltaTime * direction);
                                }
                                else
                                {
                                    transform.eulerAngles = new Vector3(0, 0, 90);
                                    movecount = 0;
                                }

                            }

                        }

                    }

                    //Left Down Corner
                    if (transform.position.y < minDistance_y && transform.position.x < minDistance_x)
                    {

                        if (halfMoveAround)
                        {
                            if (!isRotateBack || rotatecount % 2 == 0)
                            {
                                movecount += 1;
                                rotatecount += 1;
                            }
                            if (movecount == 2)
                            {
                                leftTop = false;
                                rightBottom = true;
                                isRotateBack = true;
                                movecount = 0;
                            }
                            else
                            {
                                isRotateBack = false;
                            }
                        }
                        if (rotatecount % 2 != 0 || rotatecount == 0)
                        {
                            isMoving_x = true;
                            isMoving_y = false;
                        }
                        direction = 1;

                        isRotate = true;
                    }
                    //Right Up Corner
                    else if (transform.position.y > maxDistance_y && transform.position.x > maxDistance_x)
                    {
                        if (halfMoveAround)
                        {
                            if (!isRotateBack || rotatecount % 2 == 0)
                            {
                                movecount += 1;
                                rotatecount += 1;
                            }
                            if (movecount == 2)
                            {
                                leftTop = false;
                                rightBottom = true;
                                isRotateBack = true;
                                movecount = 0;
                            }
                            else
                            {
                                isRotateBack = false;
                            }
                        }
                        if (rotatecount % 2 != 0 || rotatecount == 0)
                        {
                            isMoving_x = true;
                            isMoving_y = false;
                        }
                        direction = -1;

                        isRotate = true;


                    }


                }

                else
                {
                    if (isRotate)
                    {
                        if (direction == -1)
                        {


                            if (transform.eulerAngles.z < 270 && transform.eulerAngles.z > 90)
                            {
                                isRotate = false;
                                transform.Rotate(zero);
                                transform.eulerAngles = new Vector3(0, 0, 270);
                            }
                            else
                            {


                                if (rotatecount % 2 != 0 || rotatecount == 0)
                                {

                                    transform.Rotate(new Vector3(0, 0, 45) * rotateSpeed * Time.fixedDeltaTime * direction);
                                }

                                else
                                {
                                    transform.eulerAngles = new Vector3(0, 0, 270);
                                    movecount = 0;
                                }
                            }
                        }
                        else
                        {



                            if (transform.eulerAngles.z < 90)
                            {

                                transform.Rotate(zero);
                                transform.eulerAngles = new Vector3(0, 0, 90);
                                isRotate = false;
                            }
                            else
                            {


                                if (rotatecount % 2 != 0 || rotatecount == 0)
                                {

                                    transform.Rotate(new Vector3(0, 0, 45) * rotateSpeed * Time.fixedDeltaTime * -direction);
                                }
                                else
                                {
                                    transform.eulerAngles = new Vector3(0, 0, 90);
                                    movecount = 0;
                                }
                            }

                        }
                    }


                    //Right Down Corner
                    if (transform.position.x > maxDistance_x && transform.position.y < minDistance_y)
                    {


                        if (halfMoveAround)
                        {
                            if (!isRotateBack || rotatecount % 2 == 0)
                            {
                                movecount += 1;
                                rotatecount += 1;
                            }
                            if (movecount == 2)
                            {
                                leftTop = false;
                                rightBottom = true;
                                isRotateBack = true;
                                movecount = 0;

                            }
                            else
                            {
                                isRotateBack = false;
                            }

                        }
                        if (rotatecount % 2 != 0 || rotatecount == 0)
                        {
                            isMoving_x = true;
                            isMoving_y = false;
                            direction = -1;
                        }
                        else
                        {
                            direction = 1;
                        }


                        isRotate = true;
                    }
                    //Left Up Corner
                    else if (transform.position.x < minDistance_x && transform.position.y > maxDistance_y)
                    {

                        if (halfMoveAround)
                        {
                            if (!isRotateBack || rotatecount % 2 == 0)
                            {
                                movecount += 1;
                                rotatecount += 1;
                            }

                            if (movecount == 2)
                            {
                                leftTop = false;
                                rightBottom = true;
                                isRotateBack = true;
                                movecount = 0;
                            }
                            else
                            {
                                isRotateBack = false;
                            }
                        }

                        if (rotatecount % 2 != 0 || rotatecount == 0)
                        {

                            isMoving_x = true;
                            isMoving_y = false;
                            direction = 1;
                        }
                        else
                        {
                            direction = 1;
                        }


                        isRotate = true;
                    }
                }
            }

        }

        if (isMoving_x && !isRotate)
        {
            rigid.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;

        }
        else if (isMoving_y && !isRotate)
        {
            rigid.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        }
        else if (isRotate && isMoving_x)
        {
            rigid.constraints = RigidbodyConstraints2D.FreezePositionY;
        }
        else if (isRotate && isMoving_y)
        {
            rigid.constraints = RigidbodyConstraints2D.FreezePositionX;
        }

        if (GameManager._Instance.isSoundOn)
        {
            sound.mute = false;
        }
        else
        {
            sound.mute = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Coin_1" || collision.gameObject.tag == "Coin_2" || collision.gameObject.tag == "Coin_3")
        {
            sound.Play();
        }

    }
}
