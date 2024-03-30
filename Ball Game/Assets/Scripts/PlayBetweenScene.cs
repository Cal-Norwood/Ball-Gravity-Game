using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayBetweenScene : MonoBehaviour
{
    public SaveVariables SV;
    public AudioSource AS;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        if(SV.isMusicPlaying == false)
        {
            AS.Play();
            SV.isMusicPlaying = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
