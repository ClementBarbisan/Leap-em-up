using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : PreBonus
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        if (time < 0)
            Destroy(this.gameObject);
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Bonus") && other.gameObject.GetComponent<Bonus>().currentType == BonusType.Shield)
        {
            time += other.gameObject.GetComponent<Bonus>().time;
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("Ammo"))
            Destroy(other.gameObject);
    }
}
