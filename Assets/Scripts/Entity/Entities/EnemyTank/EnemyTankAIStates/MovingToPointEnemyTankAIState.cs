using System;
using Blocks;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace Entities
{
    [Serializable]
    public class MovingToPointEnemyTankAIState : EntityAIState
    {
        [SerializeField] private float _raycastForwardOffset;

        private int _targetRotationState;
        private Vector2 _targetPoint;
        private bool _isFollowing;
        [SerializeField] private float _moveSpeed;
        private Vector3 _lastPosition;

        public int SetTargetPos;

        public override void RunState(UnityAction callbackevent)
        {
            base.RunState(callbackevent);


            _targetRotationState = UnityEngine.Random.Range(0, 4);
            Self.transform.rotation = Quaternion.Euler(0, 0, _targetRotationState * 90f);

            setupPoint();
        }

        private void setupPoint()
        {

            //SetTargetPos = Random.Range(0, 3);
            SetTargetPos = 0;
            Debug.Log("Setup point  " + SetTargetPos);


            if (SetTargetPos == 0)
            {
                Vector3 castPosition = Self.transform.position + (Self.transform.up * _raycastForwardOffset);
                RaycastHit2D hit = Physics2D.Raycast(castPosition, Self.transform.up, 300);
                if (hit.collider != null)
                {
                    Vector2 actualPoint = Self.transform.position + (Self.transform.up * (hit.distance - -0.18f));
                    _targetPoint = new Vector2(Mathf.Round(actualPoint.x), Mathf.Round(actualPoint.y));

                    _isFollowing = true;
                }
            }

            else if(SetTargetPos == 1)
            {
                _targetPoint = GameObject.FindGameObjectWithTag("Player").transform.position;
                _isFollowing = true;
            }

            else if (SetTargetPos == 2)
            {
                _targetPoint = GameObject.FindObjectOfType<CityBlock>().gameObject.transform.position;
                _isFollowing = true;
            }


        }

        public override void UpdateState()
        {
            base.UpdateState();

            if (IsActive)
            {
                if (_isFollowing)
                {
                    Tank selfTank = Self as Tank;
                    float distanceToTarget = Vector3.Distance(selfTank.transform.position, _targetPoint);


                    //_moveSpeed = (selfTank.transform.position - _lastPosition).magnitude / Time.deltaTime;

                    _lastPosition = selfTank.transform.position;

                    //selfTank.LookAt(-Vector3.Normalize(selfTank.transform.position - (Vector3)_targetPoint));


                    Self.transform.rotation = Quaternion.Euler(0, 0, _targetRotationState * 90f);
                    selfTank.Move(Quaternion.Euler(0, 0, 0) * selfTank.transform.up);

                    if (distanceToTarget <= 0.5f || _moveSpeed <= 0.2f)
                    {
                        _isFollowing = false;
                        StopState();
                    }
                }

                Debug.DrawLine(_targetPoint * 0.99f, _targetPoint * 1.01f);
            }
        }

    }
}
