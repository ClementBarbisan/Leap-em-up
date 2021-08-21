using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

[CreateAssetMenu(fileName = "Ammo", menuName = "ScriptableObjects/TypeAmmo", order = 1)]
public class TypeAmmo : ScriptableObject
{
     public enum typeInstance
     {
          RotateAmmo,
          StandardAmmo
     };

     public enum typeAmmo
     {
          UpdateAmmoCos,
          UpdateAmmoSin,
          UpdateAmmoRotate,
          UpdateAmmo
     };

     public Sprite sprite;
     public float timeGain;
     public Vector3 relativePos;
     public float speed = 0.1f;
     public int damage;
     private float angle;
     public float initialAngle;
     public float deviation = 0;
     public Dictionary<typeAmmo, Action<Ammo>> displaceAmmo = new Dictionary<typeAmmo, Action<Ammo>>();
     public Dictionary<typeInstance, Action<GameObject, Vector3>> ammoInstance = new Dictionary<typeInstance, Action<GameObject, Vector3>>();
     public typeAmmo actionIndex;
     public typeInstance instance;

     public void Awake()
     {
          if (Application.isPlaying)
          {
               angle = initialAngle;
               displaceAmmo.Add(typeAmmo.UpdateAmmoCos, UpdateAmmoCos);
               displaceAmmo.Add(typeAmmo.UpdateAmmoSin, UpdateAmmoSin);
               displaceAmmo.Add(typeAmmo.UpdateAmmoRotate, UpdateAmmoRotate);
               displaceAmmo.Add(typeAmmo.UpdateAmmo, UpdateAmmo);
               ammoInstance.Add(typeInstance.RotateAmmo, ShotRotate);
               ammoInstance.Add(typeInstance.StandardAmmo, ShotStandard);
          }
     }

     public void UpdateBase(Ammo ammo)
     {
          ammo.transform.position += new Vector3(ammo.speed * Time.deltaTime, 0 ,0);
     }

     public void UpdateAmmoCos(Ammo ammo)
     {
          ammo.angle += ammo.speed * 2f;
          ammo.transform.position = new Vector3(Mathf.Sin(ammo.angle * Mathf.Deg2Rad) * Time.deltaTime * ammo.speed + ammo.transform.position.x, Mathf.Cos(ammo.angle * Mathf.Deg2Rad) * Time.deltaTime * ammo.speed + ammo.transform.position.y, ammo.transform.position.z);
          UpdateBase(ammo);
     }
     
     public void UpdateAmmoSin(Ammo ammo)
     {
          // speed = Mathf.Abs(Mathf.Sin(Time.time)) + initialSpeed;
          ammo.transform.position = new Vector3(Mathf.Sin(ammo.angle * Mathf.Deg2Rad) * ammo.speed * Time.deltaTime + ammo.transform.position.x, Mathf.Cos(ammo.angle * Mathf.Deg2Rad) * ammo.speed * Time.deltaTime + ammo.transform.position.y, ammo.transform.position.z);
     }
     
     public void UpdateAmmoRotate(Ammo ammo)
     {
          ammo.angle += Time.deltaTime;
          ammo.transform.position = new Vector3(Mathf.Abs(Mathf.Sin(ammo.angle * Mathf.Deg2Rad) * Time.deltaTime) * ammo.speed + ammo.transform.position.x, Mathf.Abs(Mathf.Cos(ammo.angle * Mathf.Deg2Rad) * Time.deltaTime) * ammo.speed + ammo.transform.position.y, ammo.transform.position.z);
     }
     public void UpdateAmmo(Ammo ammo)
     {
          ammo.transform.position = new Vector3(Mathf.Sin(ammo.angle * Mathf.Deg2Rad) * Time.deltaTime * ammo.speed + ammo.transform.position.x, Mathf.Cos(ammo.angle * Mathf.Deg2Rad) * Time.deltaTime * ammo.speed + ammo.transform.position.y, ammo.transform.position.z);
     }
     public void ShotRotate(GameObject ammoPref, Vector3 pos)
     {
          GameObject ammoGo = Instantiate(ammoPref, pos, Quaternion.identity);
          Ammo ammo = ammoGo.GetComponent<Ammo>();
          ammo.damage = damage;
          angle += deviation;
          ammo.angle = angle;
          ammo.speed = speed;
          ammo.timeGain = timeGain;
          ammo.transform.position += relativePos;
          ammo.action = displaceAmmo[actionIndex];
     }
     public void ShotStandard(GameObject ammoPref, Vector3 pos)
     {
          GameObject ammoGo = Instantiate(ammoPref, pos, Quaternion.identity);
          Ammo ammo = ammoGo.GetComponent<Ammo>();
          ammo.damage = damage;
          ammo.angle = angle;
          ammo.speed = speed;
          ammo.timeGain = timeGain;
          ammo.transform.position += relativePos;
          ammo.action = displaceAmmo[actionIndex];
     }
     
     public void Shot(GameObject ammoPref, Vector3 pos, TypeAmmo type)
     {
          type.ammoInstance[type.instance](ammoPref, pos);
     }
}
