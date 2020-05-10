using System;
using System.Collections.Generic;
using Leap.Unity;
using UnityEditor.PackageManager.UI;
using UnityEngine;

[RequireComponent(typeof(PathShip))]
public class SpawnGenerator:MonoBehaviour
{
    public PathShip _currentPath;
    [SerializeField] 
    private List<TypeObject> _spawnables;
    [SerializeField]
    private TypeObject _currentSpawnable;
    private void Awake()
    {
        _currentSpawnable = _spawnables[0];
    }

    private void Start()
    {
        float damages = 0;
        foreach (TypeAmmo ammo in _currentSpawnable.weapons)
            damages += ammo.damage;
        UIScript.Instance.SetSprite(_currentSpawnable.sprite, damages, _currentSpawnable.speedFire, _currentSpawnable.value);
    }

    public void Generate()
    {
        GameObject obj = new GameObject("Spawnable");
        obj.transform.position = new Vector3(0.25f, Hands.Right.WristPosition.y, 0);
        SpriteRenderer render = obj.AddComponent<SpriteRenderer>();
        render.sprite = _currentSpawnable.sprite;
        BoxCollider2D collider = obj.AddComponent<BoxCollider2D>();
        collider.size = Vector2.one;
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
            currentShip.transform.localScale = new Vector3(0.015f, 0.015f, 0);
            currentShip.ammoPrefab = _currentSpawnable.prefab;
        }
        else if (_currentSpawnable.type == TypeSpawnable.Bonus && UIScript.Instance.bonus >= 1)
        {
            Bonus currentBonus = obj.AddComponent<Bonus>();
            currentBonus.path = new List<Vector3>(_currentPath.path);
            currentBonus.shiftUpdate = _currentSpawnable.shiftUpdate;
            currentBonus.distance = _currentSpawnable.distance;
            currentBonus.speed = _currentSpawnable.speed;
            currentBonus.value = _currentSpawnable.value;
            currentBonus.time =  _currentSpawnable.time;
            currentBonus.bonusChild = _currentSpawnable.prefab;
            currentBonus.currentType = _currentSpawnable.bonusType;
            currentBonus.bonusChild.GetComponent<PreBonus>().time = _currentSpawnable.time;
            currentBonus.transform.localScale = new Vector3(0.015f, 0.015f, 0);
        }

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            _currentSpawnable = _spawnables[0];
            Generate();
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            _currentSpawnable = _spawnables[1];
            Generate();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            _currentSpawnable = _spawnables[2];
            Generate();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            _currentSpawnable = _spawnables[3];
            Generate();
        }
    }

    public void SetIndex(int index)
    {
        if (index >= _spawnables.Count)
            return;
        _currentSpawnable = _spawnables[index];
        float damages = 0;
        foreach (TypeAmmo ammo in _currentSpawnable.weapons)
            damages += ammo.damage;
        UIScript.Instance.SetSprite(_currentSpawnable.sprite, damages, _currentSpawnable.speedFire, _currentSpawnable.value);
    }
}