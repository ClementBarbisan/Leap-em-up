using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public enum TypeSpawnable
{
    Ship,
    Bonus
}
[CreateAssetMenu(fileName = "Spawnable", menuName = "ScriptableObjects/TypeObject", order = 1)]

public class TypeObject : ScriptableObject
{
    public TypeSpawnable type;
    public BonusType bonusType;
    public string tag;
    public int value;
    public float speed;
    public float shiftUpdate = 0.01f;
    public float distance = 0.001f;
    public Sprite sprite;
    public float time;
    public float speedFire;
    public int life;
    public List<TypeAmmo> weapons;
    [FormerlySerializedAs("ammoPrefab")] public GameObject prefab;
}
