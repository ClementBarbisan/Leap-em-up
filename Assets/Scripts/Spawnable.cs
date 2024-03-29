﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Collider2D))]
public class Spawnable : MonoBehaviour
{
    public List<Vector3> path;
    public int value = 0;
    public float speed;
    protected float _shift = -0.1f;
    protected int _index = 0;
    public float shiftUpdate = 0.01f;
    public float distance = 0.001f;
    protected float _startTime = 0;
    protected float _distTotal = 0;
    protected Vector3 _initPosition;
    protected void Start()
    {
        this.gameObject.layer = 9;
        _startTime = Time.time;
        if (path.Count > 0)
            _distTotal = Vector3.Distance(new Vector3(path[_index].x + _shift, path[_index].y), transform.position);
        _initPosition = transform.position;
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    protected void Update()
    {

        if (_index < path.Count)
        {

            if (Vector3.Distance(this.transform.position, new Vector3(path[_index].x + _shift, path[_index].y)) < distance)
            {
                _index++;
                _shift += shiftUpdate * speed;
                _index = _index % path.Count;
                _startTime = Time.time;
                _distTotal = Vector3.Distance(new Vector3(path[_index].x + _shift, path[_index].y), transform.position);
                _initPosition = transform.position;
            }
            float distCovered = (Time.time - _startTime) * speed;
            float part = distCovered / _distTotal;
            transform.position = Vector3.Lerp(_initPosition, new Vector3(path[_index].x + _shift, path[_index].y), part);
        }
        else
        {
          this.transform.Translate(_shift * speed * Time.deltaTime, 0, 0);
        }
    }
}
