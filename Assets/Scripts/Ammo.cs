using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    public float speed;
    public int damage;
    public float direction;
    public Action<Ammo> action;
    public float timeGain;

    public Ship generator;
    // Start is called before the first frame update
    protected void Start()
    {
       
    }

    // Update is called once per frame
    protected void Update()
    {
        action(this);
        // transform.Translate(Mathf.Sin(direction * Mathf.Deg2Rad) * Time.deltaTime * speed, Mathf.Cos(direction * Mathf.Deg2Rad) * Time.deltaTime * speed, 0);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("EnemyShip"))
        {
            EnemyShip enemy = other.gameObject.GetComponent<EnemyShip>();
            enemy.life -= damage;
            UIScript.Instance.remainingTime += timeGain;
            enemy.nbHits++;
            Destroy(this.gameObject);
        }
        else if (other.gameObject.CompareTag("PreBonus"))
            Destroy(this.gameObject);
    }
}
