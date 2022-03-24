using Entities;
using GameUtils;
using UnityEngine;

namespace Guns
{
    /// <summary>
    /// Default Player Gun
    /// </summary>
    public class EnemyGun : Gun
    {
        [SerializeField] private float _shootOffsetDistance;
        [SerializeField] private bool _destroySteel;
        public float FiringRate;
        private float _nextFiring;



        void Start()
        {
            _nextFiring = FiringRate;
        }

        public override void Update()
        {
            base.Update();
            

            _nextFiring -= Time.deltaTime;
        }
        public override void Shoot()
        {

            if (!GameObject.FindGameObjectWithTag("Game").GetComponent<PlayerPowerUps>().Timer && !Game.Instance.IsGamePaused)
            {
                if (_nextFiring <= 0)
                {

                   


                    _nextFiring = FiringRate;
                    Bullet bullet = SpawnBullet();
                    bullet.transform.position = Tank.transform.position + (Tank.transform.up * _shootOffsetDistance);
                    bullet.Follow(Tank.transform.up, Tank);
                    bullet.CanDestroySteel = _destroySteel;
                    bullet.Type = BulletType.EnemyBullet;
                }
            }


            

        }

    }
}