using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoEnemy : Ammo
{
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        this.gameObject.layer = 8;
        this.gameObject.tag = "Enemy";
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ammo"))
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
        else if (other.gameObject.CompareTag("Ship"))
        {
            other.gameObject.GetComponent<Ship>().life -= damage;
            Destroy(this.gameObject);
        }
    }
}
