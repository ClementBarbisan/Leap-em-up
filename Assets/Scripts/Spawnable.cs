using System;
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
    private float _shift = -0.3f;
    private int _index = 0;
    public float shiftUpdate= 0.01f;
    public float distance = 0.001f;
    protected void Update()
    {
        if (this.transform.position.x < -0.5f)
        {
            // Destroy(this.GetComponent<SpriteRenderer>().sprite);
            Destroy(this.gameObject);
            return;
        }

        if (Vector3.Distance(this.transform.position, new Vector3(path[_index].x - _shift, path[_index].y)) < distance)
        {
            _index++;
            _shift += shiftUpdate;
            _index = _index % path.Count;
        }
        transform.position = Vector3.Lerp(transform.position, new Vector3(path[_index].x - _shift, path[_index].y), Time.deltaTime * (1 / speed));
    }

    private void OnCollisionEnter(Collision other)
    {
        
    }
}
