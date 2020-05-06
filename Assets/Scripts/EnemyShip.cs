using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShip : Ship
{
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
        if (timeElapsed >= speedFire)
        {
            timeElapsed = 0;
            for (int i = 0; i < weapons.Count; i++)
            {
                GameObject obj = new GameObject("Ammo");
                obj.transform.position = this.transform.position;
                SpriteRenderer render = obj.AddComponent<SpriteRenderer>();
                Ammo ammo = obj.AddComponent<AmmoEnemy>();
                ammo.damage = weapons[i].damage;
                ammo.direction = weapons[i].angle;
                ammo.speed = weapons[i].speed;
                render.sprite = weapons[i].sprite;
                obj.transform.localScale = new Vector3(0.015f, 0.015f, 0);
                obj.layer = 8;
            }
        }
        else
        {
            timeElapsed += Time.deltaTime;
        }
    }
}
