using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SaveVariables", menuName = "Persistence")]
public class SaveVariables : ScriptableObject
{
    public int sceneIndex = 0;
    public string currentScene;
    public string[] Scenes;
    public int coinAmount = 0;
    public bool isMusicPlaying = false;

    // Start is called before the first frame update
    void OnEnable()
    {
        coinAmount = 0;
        sceneIndex = 0;
        currentScene = Scenes[0];
        isMusicPlaying = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShiftScene()
    {
        sceneIndex += 1;
        currentScene = Scenes[sceneIndex];
    }
}
