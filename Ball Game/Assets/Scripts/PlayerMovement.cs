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
    public bool gravityChangeUp = false;
    public bool gravityChangeDown = false;

    public bool readyToJumpLeft = false;
    public bool readyToJumpRight = false;
    public bool readyToJumpUp = false;
    public bool readyToJumpDown = false;

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

        if(onLeftWall == true)
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
                moveRight = true;

                if (isRunningRight == false)
                {
                    StartCoroutine(CameraShake("Right"));
                }
            }

            if (horizontal <= -0.5 && moveCooldown == false && currentWall != 4)
            {
                moveCooldown = true;
                moveLeft = true;

                if (isRunningLeft == false)
                {
                    StartCoroutine(CameraShake("Left"));
                }
            }
        }

        if (onRightWall == true)
        {
            if (Input.GetAxisRaw("Jump") == 1 && moveCooldown == false)
            {
                StartCoroutine(Jump());
                inMidAir = true;
                moveCooldown = true;
            }

            if (horizontal >= 0.5 && moveCooldown == false && currentWall != 4)
            {
                moveCooldown = true;
                moveRight = true;

                if (isRunningRight == false)
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

        if (trackPlayer == true)
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

            if(onLeftWall == true)
            {
                MoveDown();

                if (gameObject.transform.position.y <= leftWalls[currentWall - 1].transform.position.y)
                {
                    gameObject.transform.position = new Vector3(gameObject.transform.position.x, leftWalls[currentWall - 1].transform.position.y, gameObject.transform.position.z);
                    currentWall = currentWall - 1;
                    moveRight = false;
                }
            }

            if (onRightWall == true)
            {
                MoveUp();

                if (gameObject.transform.position.y >= leftWalls[currentWall + 1].transform.position.y)
                {
                    gameObject.transform.position = new Vector3(gameObject.transform.position.x, leftWalls[currentWall + 1].transform.position.y, gameObject.transform.position.z);
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

            if (onLeftWall == true)
            {
                MoveUp();

                if (gameObject.transform.position.y >= leftWalls[currentWall + 1].transform.position.y)
                {
                    gameObject.transform.position = new Vector3(gameObject.transform.position.x, leftWalls[currentWall + 1].transform.position.y, gameObject.transform.position.z);
                    currentWall = currentWall + 1;
                    moveLeft = false;
                }
            }

            if (onRightWall == true)
            {
                MoveDown();

                if (gameObject.transform.position.y <= leftWalls[currentWall - 1].transform.position.y)
                {
                    gameObject.transform.position = new Vector3(gameObject.transform.position.x, leftWalls[currentWall - 1].transform.position.y, gameObject.transform.position.z);
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

    void MoveUp()
    {
        gameObject.transform.Translate(gameObject.transform.up * Time.deltaTime * laneSwapSpeed);
    }

    void MoveDown()
    {
        gameObject.transform.Translate(-gameObject.transform.up * Time.deltaTime * laneSwapSpeed);
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
                else if (onTopWall == true)
                {
                    VCAnim.Play("GravityFlipBot");
                    StartCoroutine(CameraShift(2));
                }
                else if (onLeftWall == true)
                {
                    VCAnim.Play("GravityFlipLeftRight");
                    StartCoroutine(CameraShift(5));
                }
                else if (onRightWall == true)
                {
                    VCAnim.Play("GravityFlipRightLeft");
                    StartCoroutine(CameraShift(6));
                }
            }
            else
            {
                if (onBotWall == true && gravityChangeLeft == true)
                {
                    yield return new WaitForSeconds(0.1f);
                    VCAnim.Play("GravityFlipLeft");
                    StartCoroutine(CameraShift(3));
                }
                else if (onBotWall == true && gravityChangeRight == true)
                {
                    yield return new WaitForSeconds(0.1f);
                    VCAnim.Play("GravityFlipRight");
                    StartCoroutine(CameraShift(4));
                }
                else if (onTopWall == true && gravityChangeLeft == true)
                {
                    yield return new WaitForSeconds(0.1f);
                    VCAnim.Play("GravityFlipTLeft");
                    StartCoroutine(CameraShift(3));
                }
                else if (onTopWall == true && gravityChangeRight == true)
                {
                    yield return new WaitForSeconds(0.1f);
                    VCAnim.Play("GravityFlipTRight");
                    StartCoroutine(CameraShift(4));
                }
            }
            yield return new WaitForSeconds(0.3f);

            if (onBotWall == true)
            {
                if (readyToJumpLeft == false && readyToJumpRight == false)
                {
                    onBotWall = false;
                    onTopWall = true;
                }
                else if (readyToJumpLeft == true)
                {
                    onBotWall = false;
                    onLeftWall = true;
                }
                else if (readyToJumpRight == true)
                {
                    onBotWall = false;
                    onRightWall = true;
                }
            }
            else if (onTopWall == true)
            {
                if (readyToJumpLeft == false && readyToJumpRight == false)
                {
                    onBotWall = true;
                    onTopWall = false;
                }
                else if (readyToJumpLeft == true)
                {
                    onTopWall = false;
                    onLeftWall = true;
                }
                else if (readyToJumpRight == true)
                {
                    onTopWall = false;
                    onRightWall = true;
                }
            }
            else if (onLeftWall == true)
            {
                if (readyToJumpDown == false && readyToJumpUp == false)
                {
                    onRightWall = true;
                    onLeftWall = false;
                }
                else if (readyToJumpUp == true)
                {
                    onTopWall = true;
                    onLeftWall = false;
                }
                else if (readyToJumpDown == true)
                {
                    onLeftWall = false;
                    onBotWall = true;
                }
            }
            else if (onRightWall == true)
            {
                if (readyToJumpLeft == false && readyToJumpRight == false)
                {
                    onRightWall = false;
                    onLeftWall = true;
                }
                else if (readyToJumpLeft == true)
                {
                    onBotWall = true;
                    onRightWall = false;
                }
                else if (readyToJumpRight == true)
                {
                    onRightWall = false;
                    onTopWall = true;
                }
            }

            gravityChangeLeft = false;
            gravityChangeRight = false;
            readyToJumpLeft = false;
            readyToJumpRight = false;

            VC.transform.parent = gameObject.transform;
            gravityCharges = 1;
            trackPlayer = false;
            inMidAir = false;
            moveCooldown = false;
        }

        isRunningRight = false;
        isRunningLeft = false;
    }

    private IEnumerator Jump()
    {
        wallCheckDistance = 100;
        StartCoroutine(CameraShake("GravitySlam"));
        while(true)
        {
            if(gravityCharges > 0)
            {
                if(Input.GetAxisRaw("Horizontal") >= 0.5 && Input.GetAxisRaw("Jump") == 1)
                {
                    gravityCharges--;

                    int i = 0;
                    if(onBotWall == true)
                    {
                        gravityChangeRight = true;

                        foreach (GameObject G in rightWalls)
                        {
                            if (Mathf.Abs(gameObject.transform.position.y - G.transform.position.y) < wallCheckDistance)
                            {
                                wallCheckDistance = Mathf.Abs(gameObject.transform.position.y - G.transform.position.y);
                                wallToSnapTo = G;
                                currentWall = i;
                                i++;
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
                                currentWall = i;
                                i++;
                            }
                        }
                    }
                    else if(onLeftWall == true)
                    {
                        gravityChangeDown = true;

                        foreach (GameObject G in botWalls)
                        {
                            if (Mathf.Abs(gameObject.transform.position.x - G.transform.position.x) < wallCheckDistance)
                            {
                                wallCheckDistance = Mathf.Abs(gameObject.transform.position.x - G.transform.position.x);
                                wallToSnapTo = G;
                                currentWall = i;
                                i++;
                            }
                        }
                    }

                    break;
                }

                if (Input.GetAxisRaw("Horizontal") <= -0.5 && Input.GetAxisRaw("Jump") == 1)
                {
                    gravityCharges--;

                    int i = 0;
                    if (onTopWall == true)
                    {
                        gravityChangeRight = true;

                        foreach (GameObject G in rightWalls)
                        {
                            if (Mathf.Abs(gameObject.transform.position.y - G.transform.position.y) < wallCheckDistance)
                            {
                                wallCheckDistance = Mathf.Abs(gameObject.transform.position.y - G.transform.position.y);
                                wallToSnapTo = G;
                                currentWall = i;
                                i++;
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
                                currentWall = i;
                                i++;
                            }
                        }
                    }
                    else if (onLeftWall == true)
                    {
                        gravityChangeUp = true;

                        foreach (GameObject G in topWalls)
                        {
                            if (Mathf.Abs(gameObject.transform.position.x - G.transform.position.x) < wallCheckDistance)
                            {
                                wallCheckDistance = Mathf.Abs(gameObject.transform.position.x - G.transform.position.x);
                                wallToSnapTo = G;
                                currentWall = i;
                                i++;
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

            if (gameObject.transform.position.x >= 8.75 && onLeftWall == true)
            {
                gameObject.transform.position = new Vector3(9f, gameObject.transform.position.y, gameObject.transform.position.z);
                break;
            }

            if (gameObject.transform.position.x <= -8.75 && onRightWall == true)
            {
                gameObject.transform.position = new Vector3(-9f, gameObject.transform.position.y, gameObject.transform.position.z);
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

            if(onLeftWall == true)
            {
                gameObject.transform.Translate(gameObject.transform.right * Time.deltaTime * 30);
            }

            if (onRightWall == true)
            {
                gameObject.transform.Translate(-gameObject.transform.right * Time.deltaTime * 30);
            }

            yield return new WaitForSeconds(0.00001f);
        }

        bool gravityShakeL = false;

        while(gravityChangeLeft == true)
        {
            if(readyToJumpLeft == false)
            {
                if (gameObject.transform.position.y < wallToSnapTo.transform.position.y)
                {
                    gameObject.transform.Translate(gameObject.transform.up * Time.fixedDeltaTime * 15);
                }
                else if (gameObject.transform.position.y > wallToSnapTo.transform.position.y)
                {
                    gameObject.transform.Translate(-gameObject.transform.up * Time.fixedDeltaTime * 15);
                }

                if (gameObject.transform.position.y > wallToSnapTo.transform.position.y - 0.3 && gameObject.transform.position.y < wallToSnapTo.transform.position.y + 0.3)
                {
                    gameObject.transform.position = new Vector3(gameObject.transform.position.x, wallToSnapTo.transform.position.y, gameObject.transform.position.z);
                    readyToJumpLeft = true;
                }
            }

            if(readyToJumpLeft == true)
            {
                if(gravityShakeL == false)
                {
                    if(onBotWall == true)
                    {
                        StartCoroutine(CameraShakeGravity(0));
                    }
                    else if (onTopWall == true)
                    {
                        StartCoroutine(CameraShakeGravity(1));
                    }
                    gravityShakeL = true;
                }

                if(gameObject.transform.position.x > -8.7)
                {
                    gameObject.transform.Translate(-gameObject.transform.right * Time.fixedDeltaTime * 25);
                }
                else if(gameObject.transform.position.x < -8.7)
                {
                    gameObject.transform.position = new Vector3(-9, gameObject.transform.position.y, gameObject.transform.position.z);
                    break;
                }
            }

            yield return new WaitForSeconds(0.01f);
        }

        bool gravityShakeR = false;
        while (gravityChangeRight == true)
        {
            if (gameObject.transform.position.y < wallToSnapTo.transform.position.y)
            {
                gameObject.transform.Translate(gameObject.transform.up * Time.fixedDeltaTime * 15);
            }
            else if (gameObject.transform.position.y > wallToSnapTo.transform.position.y)
            {
                gameObject.transform.Translate(-gameObject.transform.up * Time.fixedDeltaTime * 15);
            }

            if (gameObject.transform.position.y > wallToSnapTo.transform.position.y - 0.3 && gameObject.transform.position.y < wallToSnapTo.transform.position.y + 0.3)
            {
                gameObject.transform.position = new Vector3(gameObject.transform.position.x, wallToSnapTo.transform.position.y, gameObject.transform.position.z);
                readyToJumpRight = true;
            }

            if (readyToJumpRight == true)
            {
                if (gameObject.transform.position.x < 8.7)
                {
                    if(gravityShakeR == false)
                    {
                        if(onBotWall == true)
                        {
                            StartCoroutine(CameraShakeGravity(1));
                        }
                        else if(onTopWall == true)
                        {
                            StartCoroutine(CameraShakeGravity(0));
                        }
                    }

                    gravityShakeR = true;
                    gameObject.transform.Translate(gameObject.transform.right * Time.fixedDeltaTime * 25);
                }
                else if (gameObject.transform.position.x > 8.7)
                {
                    gameObject.transform.position = new Vector3(9, gameObject.transform.position.y, gameObject.transform.position.z);
                    break;
                }
            }
            yield return new WaitForSeconds(0.01f);
        }

        while (gravityChangeDown == true)
        {
            if (gameObject.transform.position.x < wallToSnapTo.transform.position.x)
            {
                gameObject.transform.Translate(gameObject.transform.right * Time.fixedDeltaTime * 15);
            }
            else if (gameObject.transform.position.x > wallToSnapTo.transform.position.x)
            {
                gameObject.transform.Translate(-gameObject.transform.right * Time.fixedDeltaTime * 15);
            }

            if (gameObject.transform.position.x > wallToSnapTo.transform.position.x - 0.3 && gameObject.transform.position.x < wallToSnapTo.transform.position.x + 0.3)
            {
                gameObject.transform.position = new Vector3(wallToSnapTo.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
                readyToJumpDown = true;
                break;
            }
        }

        while (gravityChangeUp == true)
        {
            if (gameObject.transform.position.x < wallToSnapTo.transform.position.x)
            {
                gameObject.transform.Translate(gameObject.transform.right * Time.fixedDeltaTime * 15);
            }
            else if (gameObject.transform.position.x > wallToSnapTo.transform.position.x)
            {
                gameObject.transform.Translate(-gameObject.transform.right * Time.fixedDeltaTime * 15);
            }

            if (gameObject.transform.position.x > wallToSnapTo.transform.position.x - 0.3 && gameObject.transform.position.x < wallToSnapTo.transform.position.x + 0.3)
            {
                gameObject.transform.position = new Vector3(wallToSnapTo.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
                readyToJumpUp = true;
                break;
            }
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

        if(i == 3)
        {
            for(int a = 0; a < 15; a++)
            {
                if(VC.transform.position.y < gameObject.transform.position.y)
                {
                    VC.transform.position = new Vector3(VC.transform.position.x, VC.transform.position.y + (gameObject.transform.position.y / 5), VC.transform.position.z);
                }
                else
                {
                    VC.transform.position = new Vector3(VC.transform.position.x, VC.transform.position.y - (gameObject.transform.position.y / 5), VC.transform.position.z);
                }

                if(VC.transform.position.x > 3.5)
                {
                    VC.transform.position = new Vector3(VC.transform.position.x - (1.5f / 15), VC.transform.position.y, VC.transform.position.z);
                }
                else
                {
                    VC.transform.position = new Vector3(VC.transform.position.x + (1.5f / 15), VC.transform.position.y, VC.transform.position.z);
                }

                yield return new WaitForSeconds(0.001f);
            }

            VC.transform.position = new Vector3(-1.5f, gameObject.transform.position.y, VC.transform.position.z);
        }

        if (i == 4)
        {
            for (int a = 0; a < 15; a++)
            {
                if (VC.transform.position.y < gameObject.transform.position.y)
                {
                    VC.transform.position = new Vector3(VC.transform.position.x, VC.transform.position.y + (gameObject.transform.position.y / 5), VC.transform.position.z);
                }
                else
                {
                    VC.transform.position = new Vector3(VC.transform.position.x, VC.transform.position.y - (gameObject.transform.position.y / 5), VC.transform.position.z);
                }

                if (VC.transform.position.x > 3.5)
                {
                    VC.transform.position = new Vector3(VC.transform.position.x - (1.5f / 15), VC.transform.position.y, VC.transform.position.z);
                }
                else
                {
                    VC.transform.position = new Vector3(VC.transform.position.x + (1.5f / 15), VC.transform.position.y, VC.transform.position.z);
                }

                yield return new WaitForSeconds(0.001f);
            }

            VC.transform.position = new Vector3(1.5f, gameObject.transform.position.y, VC.transform.position.z);
        }

        if(i == 5)
        {
            for (int a = 0; a < 35; a++)
            {
                VC.transform.position = new Vector3(VC.transform.position.x + 0.1f, VC.transform.position.y, VC.transform.position.z);
                yield return new WaitForSeconds(0.001f);
            }
        }

        if (i == 6)
        {
            for (int a = 0; a < 35; a++)
            {
                VC.transform.position = new Vector3(VC.transform.position.x - 0.1f, VC.transform.position.y, VC.transform.position.z);
                yield return new WaitForSeconds(0.001f);
            }
        }
    }

    IEnumerator CameraShakeGravity(int dir)
    {
        if (dir == 0)
        {
            isRunningLeft = true;
            VC.m_Lens.Dutch = 0;

            while (moveRight == false)
            {
                VC.m_Lens.Dutch -= 1;
                yield return new WaitForSeconds(0.01f);

                if (VC.m_Lens.Dutch == -20)
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

        if (dir == 1)
        {
            isRunningRight = true;
            VC.m_Lens.Dutch = 0;

            while (moveLeft == false)
            {
                VC.m_Lens.Dutch += 1;
                yield return new WaitForSeconds(0.01f);

                if (VC.m_Lens.Dutch == 20)
                {
                    break;
                }
            }

            while (moveLeft == false)
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
    }
}
