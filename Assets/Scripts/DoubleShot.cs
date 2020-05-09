using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleShot : PreBonus
{
    private float _timeElapsed = 0;
    public float speedFire = 0.2f;
    public GameObject ammoPrefab;
    public List<TypeAmmo> weapons;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_timeElapsed >= speedFire)
        {
            _timeElapsed = 0;
            for (int i = 0; i < weapons.Count; i++)
            {
                GameObject obj = Instantiate(ammoPrefab);
                obj.transform.position = this.transform.position;
                AmmoEnemy ammo = obj.GetComponent<AmmoEnemy>();
                ammo.damage = weapons[i].damage;
                ammo.direction = weapons[i].angle;
                ammo.speed = weapons[i].speed;
                ammo.timeGain = weapons[i].timeGain;
                ammo.transform.position += weapons[i].relativePos;
            }
        }

        _timeElapsed += Time.deltaTime;
        time -= Time.deltaTime;
        if (time < 0)
            Destroy(this.gameObject);
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Bonus") && other.gameObject.GetComponent<Bonus>().currentType == BonusType.DoubleShot)
        {
            time += other.gameObject.GetComponent<Bonus>().time;
            Destroy(other.gameObject);
        }
    }
}