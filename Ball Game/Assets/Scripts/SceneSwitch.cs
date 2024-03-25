using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitch : MonoBehaviour
{
    public Animator VCAnim;
    public SaveVariables SV;

    // Start is called before the first frame update
    void Start()
    {
        VCAnim.Play("FadeIn");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Player")
        {
            SV.ShiftScene();
            VCAnim.Play("FadeOut");
            StartCoroutine(LoadWait());
        }
    }

    private IEnumerator LoadWait()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(SV.sceneIndex);
    }

    public void FailLevel()
    {
        SV.coinAmount = 0;
        SceneManager.LoadScene(SV.sceneIndex);
    }
}
