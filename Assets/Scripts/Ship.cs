using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Ship : Spawnable
{
    public float speedFire;
    public int life;
    public List<TypeAmmo> weapons;

    private float timeElapsed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
        if (timeElapsed >= speedFire)
        {
            timeElapsed = 0;
            for (int i = 0; i < weapons.Count; i++)
            {
                GameObject obj = new GameObject("Ammo");
                obj.transform.position = this.transform.position;
                SpriteRenderer render = obj.AddComponent<SpriteRenderer>();
                Ammo ammo = obj.AddComponent<Ammo>();
                ammo.damage = weapons[i].damage;
                ammo.direction = weapons[i].angle;
                ammo.speed = weapons[i].speed;
                render.sprite = weapons[i].sprite;
                obj.transform.localScale = new Vector3(0.015f, 0.015f, 0);
                obj.AddComponent<PolygonCollider2D>();
            }
        }
        else
        {
            timeElapsed += Time.deltaTime;
        }
    }
}
