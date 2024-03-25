using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthHandler : MonoBehaviour
{
    public int playerHealthState = 0;
    public SceneSwitch sSwitch;
    public Animator VCAnim;
    public Image progressBar;
    public bool onLastChance = false;
    public Color originalProgressColor;

    // Start is called before the first frame update
    void Start()
    {
        originalProgressColor = progressBar.color;
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
        onLastChance = true;
        StartCoroutine(ProgressFlash());
        if(playerHealthState == 1)
        {
            yield return new WaitForSeconds(4);
            onLastChance = false;
            progressBar.color = originalProgressColor;
            playerHealthState = 0;
            Debug.Log("healed");
        }
    }

    private IEnumerator ProgressFlash()
    {
        while(onLastChance)
        {
            progressBar.color = Color.red;
            yield return new WaitForSeconds(0.2f);
            progressBar.color = Color.grey;
            yield return new WaitForSeconds(0.2f);
        }
    }
}
