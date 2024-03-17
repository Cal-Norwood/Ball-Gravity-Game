using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rb;
    public List<GameObject> botWalls;
    public List<GameObject> topWalls;
    public List<GameObject> leftWalls;
    public List<GameObject> rightWalls;

    public bool onBotWall = true;
    public bool onTopWall = false;
    public bool onLeftWall = false;
    public bool onRightWall = false;

    public bool moveRight = false;
    public bool moveLeft = false;

    bool moveCooldown = false;

    bool isRunningRight = false;
    bool isRunningLeft = false;

    bool inMidAir = false;

    bool trackPlayer = false;

    public bool gravityChangeLeft = false;
    public bool gravityChangeRight = false;

    public int currentWall = 2;

    public int laneSwapSpeed;

    public int gravityCharges = 1;

    public float wallCheckDistance = 100;

    public GameObject wallToSnapTo = null;

    public CinemachineVirtualCamera VC;
    public Animator VCAnim;

    // Start is called before the first frame update
    void Start()
    {
        rb.AddForce(gameObject.transform.forward * 50, ForceMode.VelocityChange);
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        if (moveRight == false && moveLeft == false && Mathf.Abs(horizontal) <= 0.4 && inMidAir == false)
        {
            moveCooldown = false;
        }

        if (onBotWall == true)
        {
            if (Input.GetAxisRaw("Jump") == 1 && moveCooldown == false)
            {
                StartCoroutine(Jump());
                inMidAir = true;
                moveCooldown = true;
            }

            if(horizontal >= 0.5 && moveCooldown == false && currentWall != 4)
            {
                moveCooldown = true;
                moveRight = true;

                if(isRunningRight == false)
                {
                    StartCoroutine(CameraShake("Right"));
                }
            }

            if (horizontal <= -0.5 && moveCooldown == false && currentWall != 0)
            {
                moveCooldown = true;
                moveLeft = true;

                if (isRunningLeft == false)
                {
                    StartCoroutine(CameraShake("Left"));
                }
            }
        }

        if(onTopWall == true)
        {
            if (Input.GetAxisRaw("Jump") == 1 && moveCooldown == false)
            {
                StartCoroutine(Jump());
                inMidAir = true;
                moveCooldown = true;
            }

            if (horizontal >= 0.5 && moveCooldown == false && currentWall != 0)
            {
                moveCooldown = true;
                moveLeft = true;

                if (isRunningRight == false)
                {
                    StartCoroutine(CameraShake("Left"));
                }
            }

            if (horizontal <= -0.5 && moveCooldown == false && currentWall != 4)
            {
                moveCooldown = true;
                moveRight = true;

                if (isRunningLeft == false)
                {
                    StartCoroutine(CameraShake("Right"));
                }
            }
        }

        if(trackPlayer == true)
        {
            VC.transform.position = new Vector3(VC.transform.position.x, VC.transform.position.y, gameObject.transform.position.z - 25.21f);
        }
    }

    private void FixedUpdate()
    {
        if(moveRight == true)
        {
            if(onBotWall == true)
            {
                MoveRight();

                if (gameObject.transform.position.x >= botWalls[currentWall + 1].transform.position.x)
                {
                    gameObject.transform.position = new Vector3(botWalls[currentWall + 1].transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
                    currentWall = currentWall + 1;
                    moveRight = false;
                }
            }

            if(onTopWall == true)
            {
                MoveRight();

                if (gameObject.transform.position.x >= topWalls[currentWall + 1].transform.position.x)
                {
                    gameObject.transform.position = new Vector3(topWalls[currentWall + 1].transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
                    currentWall = currentWall + 1;
                    moveRight = false;
                }
            }
        }

        if (moveLeft == true)
        {
            if (onBotWall == true)
            {
                MoveLeft();

                if (gameObject.transform.position.x <= botWalls[currentWall - 1].transform.position.x)
                {
                    gameObject.transform.position = new Vector3(botWalls[currentWall - 1].transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
                    currentWall = currentWall - 1;
                    moveLeft = false;
                }
            }

            if(onTopWall == true)
            {
                MoveLeft();

                if (gameObject.transform.position.x <= topWalls[currentWall - 1].transform.position.x)
                {
                    gameObject.transform.position = new Vector3(topWalls[currentWall - 1].transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
                    currentWall = currentWall - 1;
                    moveLeft = false;
                }
            }
        }
    }

    void MoveRight()
    {
        gameObject.transform.Translate(gameObject.transform.right * Time.deltaTime * laneSwapSpeed);
    }

    void MoveLeft()
    {
        gameObject.transform.Translate(-gameObject.transform.right * Time.deltaTime * laneSwapSpeed);
    }

    private IEnumerator CameraShake(string direction)
    {
        if(direction == "Right")
        {
            isRunningRight = true;
            VC.m_Lens.Dutch = 0;

            while (moveLeft == false)
            {
                VC.m_Lens.Dutch += 1;
                yield return new WaitForSeconds(0.01f);

                if(VC.m_Lens.Dutch == 7)
                {
                    break;
                }
            }

            while(moveLeft == false)
            {
                VC.m_Lens.Dutch -= 1;
                yield return new WaitForSeconds(0.01f);

                if (VC.m_Lens.Dutch == 0)
                {
                    break;
                }
            }

            VC.m_Lens.Dutch = 0;
        }

        if(direction == "Left")
        {
            isRunningLeft = true;
            VC.m_Lens.Dutch = 0;

            while (moveRight == false)
            {
                VC.m_Lens.Dutch -= 1;
                yield return new WaitForSeconds(0.01f);

                if (VC.m_Lens.Dutch == -7)
                {
                    break;
                }
            }

            while (moveRight == false)
            {
                VC.m_Lens.Dutch += 1;
                yield return new WaitForSeconds(0.01f);

                if (VC.m_Lens.Dutch == 0)
                {
                    break;
                }
            }

            VC.m_Lens.Dutch = 0;
        }

        if(direction == "GravitySlam")
        {
            VC.transform.parent = null;
            trackPlayer = true;

            yield return new WaitForSeconds(0.4f);
            if(gravityCharges != 0)
            {
                gravityCharges = 0;
                if (onBotWall == true)
                {
                    VCAnim.Play("GravityFlipTop");
                    StartCoroutine(CameraShift(1));
                }

                if (onTopWall == true)
                {
                    VCAnim.Play("GravityFlipBot");
                    StartCoroutine(CameraShift(2));
                }

                yield return new WaitForSeconds(0.3f);

                gravityCharges = 1;
                VC.transform.parent = gameObject.transform;
                trackPlayer = false;
                inMidAir = false;
                moveCooldown = false;
            }
        }

        isRunningRight = false;
        isRunningLeft = false;
    }

    private IEnumerator Jump()
    {
        StartCoroutine(CameraShake("GravitySlam"));
        while(true)
        {
            if(gravityCharges > 0)
            {
                if(Input.GetAxisRaw("Horizontal") >= 0.5 && Input.GetAxisRaw("Jump") == 1)
                {
                    gravityCharges--;

                    if(onBotWall == true)
                    {
                        gravityChangeRight = true;

                        foreach (GameObject G in rightWalls)
                        {
                            if (Mathf.Abs(gameObject.transform.position.y - G.transform.position.y) < wallCheckDistance)
                            {
                                wallCheckDistance = Mathf.Abs(gameObject.transform.position.y - G.transform.position.y);
                                wallToSnapTo = G;
                            }
                        }
                    } 
                    else if(onTopWall == true)
                    {
                        gravityChangeLeft = true;

                        foreach (GameObject G in leftWalls)
                        {
                            if (Mathf.Abs(gameObject.transform.position.y - G.transform.position.y) < wallCheckDistance)
                            {
                                wallCheckDistance = Mathf.Abs(gameObject.transform.position.y - G.transform.position.y);
                                wallToSnapTo = G;
                            }
                        }
                    }
                    break;
                }

                if (Input.GetAxisRaw("Horizontal") <= -0.5 && Input.GetAxisRaw("Jump") == 1)
                {
                    gravityCharges--;

                    if (onTopWall == true)
                    {
                        gravityChangeRight = true;

                        foreach (GameObject G in rightWalls)
                        {
                            if (Mathf.Abs(gameObject.transform.position.y - G.transform.position.y) < wallCheckDistance)
                            {
                                wallCheckDistance = Mathf.Abs(gameObject.transform.position.y - G.transform.position.y);
                                wallToSnapTo = G;
                            }
                        }
                    }
                    else if (onBotWall == true)
                    {
                        gravityChangeLeft = true;
                        foreach (GameObject G in leftWalls)
                        {
                            if (Mathf.Abs(gameObject.transform.position.y - G.transform.position.y) < wallCheckDistance)
                            {
                                wallCheckDistance = Mathf.Abs(gameObject.transform.position.y - G.transform.position.y);
                                wallToSnapTo = G;
                            }
                        }
                    }
                    break;
                }
            }

            if(gameObject.transform.position.y >= 13.4 && onBotWall == true)
            {
                gameObject.transform.position = new Vector3(gameObject.transform.position.x, 13.5f, gameObject.transform.position.z);
                break;
            }

            if (gameObject.transform.position.y <= -4 && onTopWall == true)
            {
                gameObject.transform.position = new Vector3(gameObject.transform.position.x, -4.5f, gameObject.transform.position.z);
                break;
            }

            if (onBotWall == true)
            {
                gameObject.transform.Translate(gameObject.transform.up * Time.deltaTime * 30);
            }

            if (onTopWall == true)
            {
                gameObject.transform.Translate(-gameObject.transform.up * Time.deltaTime * 30);
            }

            yield return new WaitForSeconds(0.00001f);
        }

        while(gravityChangeLeft == true)
        {
            if(gameObject.transform.position.y < wallToSnapTo.transform.position.y)
            {
                gameObject.transform.Translate(gameObject.transform.up * Time.deltaTime * 30);
            }
            else if (gameObject.transform.position.y > wallToSnapTo.transform.position.y)
            {
                gameObject.transform.Translate(-gameObject.transform.up * Time.deltaTime * 30);
            }

            if(gameObject.transform.position.y > wallToSnapTo.transform.position.y - 0.3 && gameObject.transform.position.y < wallToSnapTo.transform.position.y + 0.3)
            {
                gameObject.transform.position = new Vector3(gameObject.transform.position.x, wallToSnapTo.transform.position.y, gameObject.transform.position.z);
                break;
            }
        }

        while (gravityChangeRight == true)
        {
            if (gameObject.transform.position.y < wallToSnapTo.transform.position.y)
            {
                gameObject.transform.Translate(gameObject.transform.up * Time.deltaTime * 30);
            }
            else if (gameObject.transform.position.y > wallToSnapTo.transform.position.y)
            {
                gameObject.transform.Translate(-gameObject.transform.up * Time.deltaTime * 30);
            }

            if (gameObject.transform.position.y > wallToSnapTo.transform.position.y - 0.3 && gameObject.transform.position.y < wallToSnapTo.transform.position.y + 0.3)
            {
                gameObject.transform.position = new Vector3(gameObject.transform.position.x, wallToSnapTo.transform.position.y, gameObject.transform.position.z);
                break;
            }
        }

        if(onBotWall == true)
        {
            onBotWall = false;
            onTopWall = true;
        }
        else if (onTopWall == true)
        {
            onBotWall = true;
            onTopWall = false;
        }
    }

    private IEnumerator CameraShift(int i)
    {
        if(i == 1)
        {
            for(int a = 0; a < 35; a++)
            {
                VC.transform.position = new Vector3(VC.transform.position.x, VC.transform.position.y + 0.1f, VC.transform.position.z);
                yield return new WaitForSeconds(0.001f);
            }
        }

        if (i == 2)
        {
            for (int a = 0; a < 35; a++)
            {
                VC.transform.position = new Vector3(VC.transform.position.x, VC.transform.position.y - 0.1f, VC.transform.position.z);
                yield return new WaitForSeconds(0.001f);
            }
        }
    }
}