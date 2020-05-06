using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    public float speed;
    public int damage;
    public int direction;
    // Start is called before the first frame update
    protected void Start()
    {
        this.gameObject.AddComponent<PolygonCollider2D>();
        Rigidbody2D body = gameObject.AddComponent<Rigidbody2D>();
        body.gravityScale = 0;
        body.mass = 0;
        body.constraints = RigidbodyConstraints2D.FreezeRotation;
        this.gameObject.layer = 9;
        this.gameObject.tag = "Ammo";
    }

    // Update is called once per frame
    protected void Update()
    {
        if (transform.position.x < -0.5f || transform.position.x > 0.5f || transform.position.y < -0.5f || transform.position.y > 0.5f)
            Destroy(this.gameObject);
        transform.Translate(Mathf.Sin(direction * Mathf.Deg2Rad) * Time.deltaTime * speed, Mathf.Cos(direction * Mathf.Deg2Rad) * Time.deltaTime * speed, 0);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
        else if (other.gameObject.CompareTag("EnemyShip"))
        {
            other.gameObject.GetComponent<EnemyShip>().life -= damage;
            Destroy(this.gameObject);
        }
    }
}
