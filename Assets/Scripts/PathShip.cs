using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using RiggedFinger = Leap.Unity.RiggedFinger;

public class PathShip : MonoBehaviour
{
    public List<Vector3> path;
    private Coroutine _savePath = null;
    [FormerlySerializedAs("distanceBetweenPoint")] [SerializeField]
    private float _distanceBetweenPoint = 0.01f;
    [FormerlySerializedAs("timeBetweenPoint")] [SerializeField]
    private float _timeBetweenPoint = 0.005f;
    private RiggedFinger finger;
    [SerializeField]
    private int limit = 10;
    private void Awake()
    {
        path = new List<Vector3>();
        finger = GetComponent<RiggedFinger>();
    }

    IEnumerator currentPath()
    {
        while (true)
        {
            if (path.Count < 1 || Vector3.Distance(path[path.Count - 1], new Vector3(finger.GetTipPosition().x,finger.GetTipPosition().y, 0)) > _distanceBetweenPoint)
                path.Add(new Vector3(finger.GetTipPosition().x, finger.GetTipPosition().y, 0));
            yield return new WaitForSeconds(_timeBetweenPoint);
        }
    }
    
    public void NewPath()
    {
        if (path.Count < limit && path.Count > 0)
            return;
        path.Clear();
        _savePath = StartCoroutine(currentPath());
    }

    public void StopPath()
    {
        if (path.Count < limit)
            return;
        if (_savePath != null)
        {
            StopCoroutine(_savePath);
            while (Vector3.Distance(path[path.Count - 1], path[0]) > _distanceBetweenPoint)
            {
                path.Add(Vector3.Lerp(path[path.Count - 1], path[0], 0.5f));
            }
        }
    }
}
