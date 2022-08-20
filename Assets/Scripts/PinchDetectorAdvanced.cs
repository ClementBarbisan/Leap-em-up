using System.Collections.Generic;
using Leap.Unity;

using UnityEngine;
using Leap.Unity.Attributes;
using Leap.Unity.HandsModule;
using UnityEngine.Serialization;

namespace Leap.Unity {
    public class PinchDetectorAdvanced:PinchDetector
    {
        [FormerlySerializedAs("typeFinger")] [SerializeField]
        public Finger.FingerType _typeFinger;
        protected override void Awake()
        {
            base.Awake();
        }

        protected override void ensureUpToDate()
        {
                if (Time.frameCount == _lastUpdateFrame) {
                  return;
                }
                _lastUpdateFrame = Time.frameCount;
          
                _didChange = false;
          
                Hand hand = _handModel.GetLeapHand();
          
                if (hand == null || !_handModel.IsTracked) {
                  changeState(false);
                  return;
                }
          
                _distance = GetPinchDistance(hand);
                _rotation = hand.Basis.rotation;
                _position = ((hand.Fingers[0].TipPosition + hand.Fingers[(int)_typeFinger].TipPosition) * .5f);
          
                if (IsActive) {
                  if (_distance > DeactivateDistance) {
                    changeState(false);
                    //return;
                  }
                } else {
                  if (_distance < ActivateDistance) {
                    changeState(true);
                  }
                }
          
                if (IsActive) {
                  _lastPosition = _position;
                  _lastRotation = _rotation;
                  _lastDistance = _distance;
                  _lastDirection = _direction;
                  _lastNormal = _normal;
                }
                if (ControlsTransform) {
                  transform.position = _position;
                  transform.rotation = _rotation;
                }
        }

        protected float GetPinchDistance(Hand hand)
        {
            var indexTipPosition = hand.Fingers[(int)_typeFinger].TipPosition;
            var thumbTipPosition = hand.GetThumb().TipPosition;
            return Vector3.Distance(indexTipPosition, thumbTipPosition);
        }
            
    }
}