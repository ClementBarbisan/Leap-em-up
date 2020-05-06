using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    public float speed;
    public int damage;
    public int direction;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < -0.5f || transform.position.x > 0.5f || transform.position.y < -0.5f || transform.position.y > 0.5f)
            Destroy(this.gameObject);
        transform.Translate(Mathf.Sin(direction * Mathf.Deg2Rad) * Time.deltaTime * speed, Mathf.Cos(direction * Mathf.Deg2Rad) * Time.deltaTime * speed, 0);
    }
}
