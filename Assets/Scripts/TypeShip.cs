using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TypeSpawnable
{
    Ship,
    Bonus
}
[CreateAssetMenu(fileName = "Ship", menuName = "ScriptableObjects/TypeShip", order = 1)]

public class TypeShip : ScriptableObject
{
    public TypeSpawnable type;
    public int value;
    public float speed;
    public float shiftUpdate = 0.01f;
    public float distance = 0.001f;
    public Sprite sprite;
    public float speedFire;
    public int life;
    public List<TypeAmmo> weapons;
}
