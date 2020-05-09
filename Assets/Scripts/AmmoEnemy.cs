using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoEnemy : Ammo
{
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }
    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ship"))
        {
            other.gameObject.GetComponent<Ship>().life -= damage;
            Destroy(this.gameObject);
        }
    }
}
