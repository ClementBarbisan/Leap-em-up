using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    public float speed;
    public int damage;
    public int direction;

    public float timeGain;
    // Start is called before the first frame update
    protected void Start()
    {
       
    }

    // Update is called once per frame
    protected void Update()
    {
        if (transform.position.x < -0.5f || transform.position.x > 0.5f || transform.position.y < -0.5f || transform.position.y > 0.5f)
            Destroy(this.gameObject);
        transform.Translate(Mathf.Sin(direction * Mathf.Deg2Rad) * Time.deltaTime * speed, Mathf.Cos(direction * Mathf.Deg2Rad) * Time.deltaTime * speed, 0);
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("EnemyShip"))
        {
            EnemyShip enemy = other.gameObject.GetComponent<EnemyShip>();
            enemy.life -= damage;
            UIScript.RemainingTime += timeGain;
            enemy.nbHits++;
            Destroy(this.gameObject);
        }
    }
}
