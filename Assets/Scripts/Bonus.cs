using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus : Spawnable
{
    public float time;
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        this.gameObject.tag = "Bonus";
        this.transform.localScale = new Vector3(0.015f, 0.015f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (time <= 0)
            Destroy(this.gameObject);
        
    }
}
