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

    int currentWall = 2;

    public int laneSwapSpeed;

    public CinemachineVirtualCamera VC;

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

        if (moveRight == false && moveLeft == false && Mathf.Abs(horizontal) <= 0.4)
        {
            moveCooldown = false;
        }

        if (onBotWall == true)
        {
            if(horizontal >= 0.5 && moveCooldown == false && currentWall != 4)
            {
                moveCooldown = true;
                moveRight = true;
                StartCoroutine(CameraShake("Right"));
            }

            if (horizontal <= -0.5 && moveCooldown == false && currentWall != 0)
            {
                moveCooldown = true;
                moveLeft = true;
                StartCoroutine(CameraShake("Left"));
            }
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
            VC.m_Lens.Dutch = 0;

            while (moveLeft == false)
            {
                VC.m_Lens.Dutch += 1;
                yield return new WaitForSeconds(0.005f);

                if(VC.m_Lens.Dutch == 10)
                {
                    break;
                }
            }

            while(moveLeft == false)
            {
                VC.m_Lens.Dutch -= 1;
                yield return new WaitForSeconds(0.005f);

                if (VC.m_Lens.Dutch == 0)
                {
                    break;
                }
            }

            VC.m_Lens.Dutch = 0;
        }

        if(direction == "Left")
        {
            VC.m_Lens.Dutch = 0;

            while (moveRight == false)
            {
                VC.m_Lens.Dutch -= 1;
                yield return new WaitForSeconds(0.005f);

                if (VC.m_Lens.Dutch == -10)
                {
                    break;
                }
            }

            while (moveRight == false)
            {
                VC.m_Lens.Dutch += 1;
                yield return new WaitForSeconds(0.005f);

                if (VC.m_Lens.Dutch == 0)
                {
                    break;
                }
            }

            VC.m_Lens.Dutch = 0;
        }
    }
}
