using Entities;
using UnityEngine;

namespace Guns
{
    /// <summary>
    /// Base class of Gun
    /// </summary>
    public class Gun : MonoBehaviour
    {
        [SerializeField] protected Bullet _bulletPrefab;
        public AudioClip TankShootingSfx;

        /// <summary>
        /// Self tank
        /// </summary>
        protected Tank Tank;

        /// <summary>
        /// Initializinng gun
        /// </summary>
        /// <param name="tank">Self tank</param>
        public virtual void Init(Tank tank)
        {
            Tank = tank;
        }

        void Start()
        {
            _bulletPrefab.CanDestroySteel = false;
           


        }


        public virtual void Update()
        {
            
        }

        /// <summary>
        /// Shoots bullet
        /// </summary>
        public virtual void Shoot()
        {
            /*Bullet bullet = SpawnBullet();
            bullet.transform.position = Tank.transform.position;
            bullet.Follow(Tank.transform.up, Tank);*/
        }

        /// <summary>
        /// Spawns bullet
        /// </summary>
        /// <returns>Bullet reference</returns>
        public virtual Bullet SpawnBullet()
        {
            
            Bullet bullet = Instantiate((Bullet)_bulletPrefab.Clone());
            bullet.transform.position = Tank.transform.position;

            return bullet;
        }

    }
}
