using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthHandler : MonoBehaviour
{
    public int playerHealthState = 0;
    public SceneSwitch sSwitch;
    public Animator VCAnim;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void HitObstacle()
    {
        if (playerHealthState == 0)
        {
            playerHealthState = 1;
            StartCoroutine(PlayerLastChance());
        }
        else if (playerHealthState == 1)
        {
            sSwitch.FailLevel();
        }
    }

    private IEnumerator PlayerLastChance()
    {
        if(playerHealthState == 1)
        {
            yield return new WaitForSeconds(4);
            playerHealthState = 0;
            Debug.Log("healed");
        }
    }
}
