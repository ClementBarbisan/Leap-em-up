using System;
using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemyShip : Ship
{
    public int nbHits = 0;
    [FormerlySerializedAs("nbHitsToGen")] [SerializeField]
    private int _nbHitsToGen = 1;

    public GameObject collectible;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        this.gameObject.layer = 8;
        this.gameObject.tag = "EnemyShip";
    }

    // Update is called once per frame
    void Update()
    {
        if (life <= 0)
            Destroy(this.gameObject);
        if (nbHits >= _nbHitsToGen)
        {
            nbHits = 0;
            Instantiate(collectible, this.transform.position, Quaternion.identity);
        }
        if (timeElapsed >= speedFire)
        {
            timeElapsed = 0;
            for (int i = 0; i < weapons.Count; i++)
            {
                GameObject obj = Instantiate(ammoPrefab);
                obj.transform.position = this.transform.position;
                Ammo ammo = obj.GetComponent<AmmoEnemy>();
                ammo.damage = weapons[i].damage;
                ammo.direction = weapons[i].angle;
                ammo.speed = weapons[i].speed;
                ammo.transform.position += weapons[i].relativePos;
            }
        }
        else
        {
            timeElapsed += Time.deltaTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
       

    }
}
