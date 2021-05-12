using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

[CreateAssetMenu(fileName = "Ammo", menuName = "ScriptableObjects/TypeAmmo", order = 1)]
public class TypeAmmo : ScriptableObject
{
     public Sprite sprite;
     public float timeGain;
     public Vector3 relativePos;
     private float speed;
     public float initialSpeed = 0.1f;
     public int damage;
     public float angle;
     public List<Action<Ammo>> displaceAmmo = new List<Action<Ammo>>();
     public int actionIndex = 0;

     private void Awake()
     {
          displaceAmmo.Add(UpdateAmmoCos);
          displaceAmmo.Add(UpdateAmmoRotate);
          displaceAmmo.Add(UpdateAmmo);
     }

     public void UpdateAmmoCos(Ammo ammo)
     {
          speed = Mathf.Abs(Mathf.Cos(Time.time)) * initialSpeed;
          ammo.transform.Translate(Mathf.Sin(angle * Mathf.Deg2Rad) * Time.deltaTime * speed, Mathf.Cos(angle * Mathf.Deg2Rad) * Time.deltaTime * speed, 0);
     }
     
     public void UpdateAmmoSin(Ammo ammo)
     {
          speed = Mathf.Abs(Mathf.Sin(Time.time)) * initialSpeed;
          ammo.transform.Translate(Mathf.Sin(angle * Mathf.Deg2Rad) * Time.deltaTime * speed, Mathf.Cos(angle * Mathf.Deg2Rad) * Time.deltaTime * speed, 0);
     }
     
     public void UpdateAmmoRotate(Ammo ammo)
     {
          angle += Time.deltaTime;
          ammo.transform.Translate(Mathf.Sin(angle * Mathf.Deg2Rad) * Time.deltaTime * speed, Mathf.Cos(angle * Mathf.Deg2Rad) * Time.deltaTime * speed, 0);
     }
     public void UpdateAmmo(Ammo ammo)
     {
          ammo.transform.Translate(Mathf.Sin(angle * Mathf.Deg2Rad) * Time.deltaTime * speed, Mathf.Cos(angle * Mathf.Deg2Rad) * Time.deltaTime * speed, 0);
     }
     public Ammo Shot(GameObject ammoPref, Vector3 pos)
     {
          if (displaceAmmo.Count == 0)
          {
               displaceAmmo.Add(UpdateAmmo);
               displaceAmmo.Add(UpdateAmmoCos);
               displaceAmmo.Add(UpdateAmmoSin);
               displaceAmmo.Add(UpdateAmmoRotate);
          }

          GameObject ammoGo = Instantiate(ammoPref, pos, Quaternion.identity);
          Ammo ammo = ammoGo.GetComponent<Ammo>();
          ammo.damage = damage;
          ammo.direction = angle;
          ammo.speed = speed;
          ammo.timeGain = timeGain;
          ammo.transform.position += relativePos;
          ammo.action = displaceAmmo[actionIndex];
          return (ammo);
     }
}
