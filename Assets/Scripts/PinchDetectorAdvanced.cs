using Leap.Unity;

using UnityEngine;
using Leap.Unity.Attributes;
using UnityEngine.Serialization;

namespace Leap.Unity {
    [RequireComponent(typeof(RiggedFinger))]
    public class PinchDetectorAdvanced:PinchDetector
    {
        [FormerlySerializedAs("finger")] [SerializeField]
        private RiggedFinger _finger;
        [FormerlySerializedAs("typeFinger")] [SerializeField]
        private Finger.FingerType _typeFinger;
        protected override void Awake()
        {
            base.Awake();
            _finger = this.GetComponent<RiggedFinger>();
            _typeFinger = _finger.fingerType;
            if (_typeFinger == Finger.FingerType.TYPE_THUMB)
            {
                Debug.LogWarning("Finger type is Thumb : Can't pinch. Script disabled.");
                this.enabled = false;
            }
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
                _rotation = hand.Basis.CalculateRotation();
                _position = ((hand.Fingers[0].TipPosition + hand.Fingers[(int)_typeFinger].TipPosition) * .5f).ToVector3();
          
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

        protected override float GetPinchDistance(Hand hand)
        {
            var indexTipPosition = hand.Fingers[(int)_typeFinger].TipPosition.ToVector3();
            var thumbTipPosition = hand.GetThumb().TipPosition.ToVector3();
            return Vector3.Distance(indexTipPosition, thumbTipPosition);
        }
            
    }
}