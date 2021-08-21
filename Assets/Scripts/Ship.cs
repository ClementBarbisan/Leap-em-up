using System;
using System.Collections;
using System.Collections.Generic;
using Leap;
using UnityEngine;
using UnityEngine.Serialization;

public class Ship : Spawnable
{
    public float speedFire;
    public int life;
    public List<TypeAmmo> weapons;
    protected float timeElapsed;

    public GameObject ammoPrefab;
    // Start is called before the first frame update
    protected void Start()
    {
        base.Start();
        this.gameObject.tag = "Ship";
        Rigidbody2D body = gameObject.AddComponent<Rigidbody2D>();
        body.gravityScale = 0;
        body.mass = 0;
        body.constraints = RigidbodyConstraints2D.FreezeRotation;
        for (int i = 0; i < weapons.Count; i++)
            weapons[i].Awake();
    }

    private void OnBecameInvisible()
    {
        
    }

    // Update is called once per frame
    protected void Update()
    {
        base.Update();
        
        if (life <= 0)
            Destroy(this.gameObject);
        if (timeElapsed >= speedFire)
        {
            timeElapsed = 0;
            for (int i = 0; i < weapons.Count; i++)
            {
                weapons[i].Shot(ammoPrefab, transform.position, weapons[i]);
            }
        }
        else
        {
            timeElapsed += Time.deltaTime;
        }
    }
    
    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("EnemyShip"))
        {
            other.gameObject.GetComponent<EnemyShip>().life -= this.value;
            Destroy(this.gameObject);
        }
        else if (other.gameObject.CompareTag("Collectible"))
        {
            UIScript.Instance.chest++;
            Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag("PreBonus"))
            Destroy(other.gameObject);
    }
}
