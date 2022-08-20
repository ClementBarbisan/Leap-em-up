using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Leap;
using Leap.Unity;
using Leap.Unity.HandsModule;
using UnityEngine;
using UnityEngine.Serialization;

public class PathShip : MonoBehaviour
{
    public List<Vector3> path = new List<Vector3>();
    public Finger.FingerType typeFinger;
    private  HandBinder finger;
    private Coroutine _savePath = null;
    [FormerlySerializedAs("distanceBetweenPoint")] [SerializeField]
    private float _distanceBetweenPoint = 0.01f;
    [FormerlySerializedAs("timeBetweenPoint")] [SerializeField]
    private float _timeBetweenPoint = 0.005f;
    [SerializeField]
    private int limit = 10;

    private LeapServiceProvider leapService;

    private float _startTime = 0;
    private void Awake()
    {
        leapService = GetComponentInChildren<LeapServiceProvider>();
        finger = FindObjectsOfType<HandBinder>().Where(x => x.Chirality == Chirality.Right).ToArray()[0];
    }

    IEnumerator currentPath()
    {
        Finger currentFinger = finger.LeapHand.GetIndex();
        while (true)
        {
            
            if ( currentFinger != null
                 && (path.Count < 1 || 
                 Vector3.Distance(path[path.Count - 1],
                     new Vector3(finger.LeapHand.GetIndex().TipPosition.x,
                         finger.LeapHand.GetIndex().TipPosition.y, 0)) > _distanceBetweenPoint))
            {
                path.Add(new Vector3(currentFinger.TipPosition.x, currentFinger.TipPosition.y,
                    0));
               // Debug.Log(finger.LeapHand.GetIndex().TipPosition);
            }
            else
            {
                currentFinger = finger.LeapHand.GetIndex();
            }
            yield return new WaitForSeconds(_timeBetweenPoint);
        }
    }

    public void resetPath()
    {
       // path.Clear();
    }
    
    public void NewPath()
    {
        if (_savePath != null)
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
            _savePath = null;
            float distTotal = Vector3.Distance(path[path.Count - 1], path[0]);
            while (Vector3.Distance(path[path.Count - 1], path[0]) > _distanceBetweenPoint)
            {
                float distCovered = (Time.time - _startTime) * _distanceBetweenPoint;
                float part = distCovered / distTotal;
                path.Add(Vector3.Lerp(path[path.Count - 1], path[0], part));
            }
        }
    }

    private void OnDestroy()
    {
        leapService.destroyController();
    }
}
