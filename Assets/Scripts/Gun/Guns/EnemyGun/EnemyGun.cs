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
        private Bullet _previousBullet;
        public float bulletSpeed;

        void Start()
        {
            _nextFiring = FiringRate;
        }

        public override void Update()
        {
            base.Update();
            Debug.Log($"Can destroy for player :  {_bulletPrefab.CanDestroySteel}");
            _nextFiring -= Time.deltaTime;
        }
        public override void Shoot()
        {

            if (!GameObject.FindGameObjectWithTag("Game").GetComponent<PlayerPowerUps>().Timer && !Game.Instance.IsGamePaused)
            {
                if (/*_nextFiring <= 0 &&*/ _previousBullet == null)
                {

                    Debug.Log("Shooted From " + gameObject.name);

                    _nextFiring = FiringRate;
                    Bullet bullet = SpawnBullet();
                    bullet.transform.position = Tank.transform.position + (Tank.transform.up * _shootOffsetDistance);
                    bullet.Follow(Tank.transform.up, Tank);
                    bullet.CanDestroySteel = _destroySteel;
                    _previousBullet = bullet;
                    bullet.Type = BulletType.EnemyBullet;
                    bullet._bulletSpeed = 6f;
                    _previousBullet = bullet;
                }
            }


            

        }

    }
}