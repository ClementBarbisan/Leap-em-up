using System;
using System.Collections.Generic;
using Leap.Unity;
using UnityEditor.PackageManager.UI;
using UnityEngine;

[RequireComponent(typeof(PathShip))]
public class SpawnGenerator:MonoBehaviour
{
    private PathShip _currentPath;
    [SerializeField] 
    private List<TypeObject> _spawnables;
    [SerializeField]
    private TypeObject _currentSpawnable;
    private void Awake()
    {
        _currentPath = GetComponent<PathShip>();
        _currentSpawnable = _spawnables[0];
    }

    public void Generate()
    {
        if (_currentPath.path.Count <= 0)
            return;
        GameObject obj = new GameObject("Spawnable");
        obj.transform.position = new Vector3(0.25f, Hands.Right.WristPosition.y, 0);
        SpriteRenderer render = obj.AddComponent<SpriteRenderer>();
        render.sprite = _currentSpawnable.sprite;
        obj.AddComponent<PolygonCollider2D>();
        if (_currentSpawnable.type == TypeSpawnable.Ship)
        {
            Ship currentShip = obj.AddComponent<Ship>();
            currentShip.path = new List<Vector3>(_currentPath.path);
            currentShip.weapons = _currentSpawnable.weapons;
            currentShip.life = _currentSpawnable.life;
            currentShip.speedFire = _currentSpawnable.speedFire;
            currentShip.speed = _currentSpawnable.speed;
            currentShip.distance = _currentSpawnable.distance;
            currentShip.shiftUpdate = _currentSpawnable.shiftUpdate;
            obj.tag = "Ship";
        }
        else if (_currentSpawnable.type == TypeSpawnable.Bonus)
        {
            Bonus currentBonus = obj.AddComponent<Bonus>();
            currentBonus.path = new List<Vector3>(_currentPath.path);
            currentBonus.shiftUpdate = _currentSpawnable.shiftUpdate;
            currentBonus.distance = _currentSpawnable.distance;
            currentBonus.speed = _currentSpawnable.speed;
            currentBonus.value = _currentSpawnable.value;
            obj.tag = "Bonus";
        }

        obj.transform.localScale = new Vector3(0.015f, 0.015f, 0);
    }

    public void SetIndex(int index)
    {
        if (index >= _spawnables.Count)
            return;
        _currentSpawnable = _spawnables[index];
    }
}