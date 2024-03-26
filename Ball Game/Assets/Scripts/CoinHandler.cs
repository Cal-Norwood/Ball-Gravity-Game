using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinHandler : MonoBehaviour
{
    public SaveVariables SV;
    public TextMeshProUGUI coinText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        coinText.text = SV.coinAmount.ToString();
    }

    public void CoinPickup()
    {
        SV.coinAmount += 1;
    }
}
