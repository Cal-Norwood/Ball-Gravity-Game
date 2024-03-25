using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelProgressDisplay : MonoBehaviour
{
    public Image progressBar;
    public GameObject Player;
    public GameObject End;

    public Vector3 startPos;
    public Vector3 endPos;

    public float distance;

    // Start is called before the first frame update
    void Start()
    {
        startPos = Player.transform.position;
        endPos = End.transform.position;

        distance = endPos.z - startPos.z;
    }

    // Update is called once per frame
    void Update()
    {
        distance = End.transform.position.z - Player.transform.position.z;
        progressBar.rectTransform.sizeDelta = new Vector2(1000 - distance, 27);
    }
}
