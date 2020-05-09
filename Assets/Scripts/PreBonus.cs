using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreBonus : MonoBehaviour
{
    public float time;

    public BonusType currentType;
    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.tag = "PreBonus";
        this.gameObject.layer = 14;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   
}
