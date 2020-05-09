using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Collectible : MonoBehaviour
{
    [FormerlySerializedAs("dist")] [SerializeField]
    private float _dist;
    private Vector3 _initPos;
    [FormerlySerializedAs("speed")] [SerializeField] 
    private float _speed = 0;
    IEnumerator SlideDirection()
    {
        float currentDist = 0;
        _dist += Random.Range(-0.2f, 0.2f);
        int direction = Random.Range(20, 160);
        while (currentDist < _dist)
        {
            transform.Translate(_speed * Mathf.Sin(direction * Mathf.Deg2Rad) * Time.deltaTime, _speed * Mathf.Cos(direction * Mathf.Deg2Rad) * Time.deltaTime, 0);
            currentDist = Vector3.Distance(this.transform.position, _initPos);
            yield return new WaitForEndOfFrame();
        }
    }

// Start is called before the first frame update
    void Start()
    {
        _initPos = this.transform.position;
        StartCoroutine(SlideDirection());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
