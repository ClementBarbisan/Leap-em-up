﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Ammo", menuName = "ScriptableObjects/TypeAmmo", order = 1)]
public class TypeAmmo : ScriptableObject
{
     public Sprite sprite;
     public float timeGain;
     public Vector3 relativePos;
     public float speed;
     public int damage;
     public int angle;
}
