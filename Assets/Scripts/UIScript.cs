using System;
using System.Collections;
using System.Collections.Generic;
using LeapInternal;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    public static UIScript Instance;
    public float remainingTime = 10;
    public int chest = 0;
    public float bonus = 0;
    public TextMeshProUGUI timeUI;
    public TextMeshProUGUI chestUI;
    [SerializeField] 
    private float _bonusReg;
    [SerializeField]
    private int _bonusLimit;
    public TextMeshProUGUI bonusAvailable;
    public TextMeshProUGUI speedFireText;
    public TextMeshProUGUI damagesText;
    public TextMeshProUGUI valueText;
    public Image image;

    public void SetSprite(Sprite sprite, float damages, float speedFire, int value)
    {
        image.sprite = sprite;
        image.preserveAspect = true;
        damagesText.text = "Damages :" + Environment.NewLine + damages.ToString();
        speedFireText.text = "Speed fire : " + Environment.NewLine + speedFire.ToString();
        valueText.text = "Value : " + Environment.NewLine + value.ToString();
    }
    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (remainingTime > 0)
        {
            timeUI.text = "Time : " + ((int)(remainingTime / 60)).ToString("00") + ":" + ((int)(remainingTime % 60)).ToString("00");
            remainingTime -= Time.deltaTime;   
        }
        chestUI.text = "Chest : " + Environment.NewLine + chest.ToString();
        bonusAvailable.text = "Bonus : " + Environment.NewLine + ((int)bonus).ToString();
        if (bonus < _bonusLimit)
            bonus += _bonusReg * Time.deltaTime;
    }
}
