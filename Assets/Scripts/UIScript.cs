using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class UIScript : MonoBehaviour
{
    public static float RemainingTime = 10;
    public static int chest = 0;
    public TextMeshProUGUI timeUI;
    public TextMeshProUGUI chestUI;
    // Start is called before the first frame update
    void Start()
    {
        timeUI = GetComponent<TextMeshProUGUI>();
        
    }

    // Update is called once per frame
    void Update()
    {
        timeUI.text = ((int)(RemainingTime / 60)).ToString("00") + ":" + ((int)(RemainingTime % 60)).ToString("00");
        RemainingTime -= Time.deltaTime;
        chestUI.text = chest.ToString();
    }
}
