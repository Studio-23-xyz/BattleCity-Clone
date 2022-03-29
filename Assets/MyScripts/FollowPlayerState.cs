using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using GameUtils;
using UnityEngine;
using Pathfinding;
using UnityEngine.Events;


namespace Entities
{
    [Serializable]
    public class FollowPlayerState : EntityAIState
    {
        public Transform Target;
        public float Speed = 2f;
        private float tankMoveSPeed = 200f;
        public float NextWayPointDistance = 2f;
        private Path path;
        public int CurrentWayPoint = 0;
        public bool ReachedEndOfPath;
        private Seeker _seerker;
        private Rigidbody2D _rb;
        public bool invoked;
        private bool _isFollowing;



        void Start()
        {
            Target = GameObject.FindWithTag("Player").GetComponent<Transform>();

        }



        public override void RunState(UnityAction callbackevent)
        {
            base.RunState(callbackevent);

            Debug.Log("Follow Player Triggered by " + Self.name);


            StartCalculatingPath();
        }

        /*void Start()
        {
            Game.Instance.Triggers.PlayerGenerated.AddListener(StartCalculatingPath);
        }*/

        public async void StartCalculatingPath()
        {

            await UniTask.Delay(TimeSpan.FromSeconds(8));
            AstarPath.active.Scan();
            invoked = true;

            _seerker = this.Self.transform.GetComponent<Seeker>();
            _rb = this.Self.GetComponent<Rigidbody2D>();
            Target = GameObject.FindWithTag("Player").GetComponent<Transform>();
            //Target = GameObject.FindWithTag("Player").GetComponent<Transform>();

            //InvokeRepeating("UpdatePath", 0f, 1f);

            UpdatePath();

        }

        void UpdatePath()
        {
            _seerker.StartPath(_rb.position, Target.position, onCompletedPath);

        }

        void onCompletedPath(Path p)
        {
            if (!p.error)
            {
                path = p;
                CurrentWayPoint = 0;
                _isFollowing = true;
            }
        }

        // Update is called once per frame
        //void FixedUpdate()
        //{
        //    if (!invoked)
        //        return;

        //    if (path == null)
        //        return;

        //    if (CurrentWayPoint >= path.vectorPath.Count)
        //    {
        //        ReachedEndOfPath = true;
        //        return;
        //    }
        //    else
        //    {
        //        ReachedEndOfPath = false;
        //    }

        //    Vector2 direction = ((Vector2) path.vectorPath[CurrentWayPoint] - _rb.position).normalized;

        //    /*if (direction.x > direction.y)
        //    {
        //        direction.y = 0;
        //    }
        //    else
        //    {
        //        direction.x = 0;
        //    }*/

        //    Vector2 force = direction * Speed * Time.deltaTime;


        //    //_rb.AddForce(force);

        //    _rb.velocity = direction * Speed;

        //    float distance = Vector2.Distance(_rb.position, path.vectorPath[CurrentWayPoint]);
        //    if (distance < NextWayPointDistance)
        //    {
        //        CurrentWayPoint++;
        //    }

        //}

        public override void UpdateState()
        {
            base.UpdateState();

            if (IsActive)
            {
                if (_isFollowing)
                {





                    if (!invoked)
                        return;

                    if (path == null)
                        return;

                    if (CurrentWayPoint >= path.vectorPath.Count)
                    {
                        ReachedEndOfPath = true;
                        return;
                    }
                    else
                    {
                        ReachedEndOfPath = false;
                    }



                    if (ReachedEndOfPath || (_rb.velocity.x <.5f && _rb.velocity.y <.5f) )
                    {
                        _isFollowing = false;
                        StopState();
                        return;
                    }

                    Vector2 direction = ( (Vector2)path.vectorPath[CurrentWayPoint] - _rb.position).normalized;



                    //Vector2 force = direction * Speed * Time.deltaTime;
                    Vector2 force = direction * tankMoveSPeed * Time.deltaTime;

                    _rb.AddForce(force);

                    //_rb.velocity = direction * Speed;

                    float distance = Vector2.Distance(_rb.position, path.vectorPath[CurrentWayPoint]);
                    if (distance < NextWayPointDistance)
                    {
                        CurrentWayPoint++;
                    }

                    Self.GetComponentInChildren<SpriteRenderer>().GetComponent<Transform>().up= direction;


                }

                
            }
        }

    }
}
