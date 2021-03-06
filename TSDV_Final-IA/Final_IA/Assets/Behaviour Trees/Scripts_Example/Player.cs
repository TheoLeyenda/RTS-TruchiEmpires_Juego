﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace BehaviourTree
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private float recoverHealthRate = 1;
        [SerializeField] private float startingHealth = 100;
        public float health;
        public float speed;
        public static Player InstancePlayer;
        private Rigidbody rig;

        [Header("Shooting Settings")]
        [SerializeField] private Bullet bulletObject;
        [SerializeField] private GameObject spawnBullet;
        [SerializeField] private float damageBullet;

        [SerializeField] Transform target;

        void Awake()
        {
            health = startingHealth;
            if (InstancePlayer == null)
                InstancePlayer = this;
            else
                Destroy(gameObject);

            rig = GetComponent<Rigidbody>();

        }
        void Update()
        {
            Movement();
            Rotation();
            rig.velocity = Vector3.zero;
            rig.angularVelocity = Vector3.zero;
            if (health < startingHealth)
            {
                health += Time.deltaTime * recoverHealthRate;
            }
            if (Input.GetMouseButtonDown(0))
            {
                ShootBullet();
            }
        }
        private void Rotation()
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 point = Vector3.zero;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                point = hit.point;
            }

            Vector3 relative = transform.InverseTransformPoint(point);
            float angle = Mathf.Atan2(relative.x, relative.z) * Mathf.Rad2Deg;
            transform.Rotate(0, angle, 0);
        }
        private void Movement()
        {
            transform.position += new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * speed * Time.deltaTime;
        }
        public void TakeDamage(float _damage)
        {
            health -= _damage;
        }
        public void CheckDie()
        {
            if (health <= 0)
            {
                SceneManager.LoadScene("GameOver");
            }
        }

        public void ShootBullet()
        {
            Bullet _bullet = Instantiate(bulletObject, spawnBullet.transform.position, spawnBullet.transform.rotation);
            _bullet.SetShooter(Bullet.Shooter.Player);
            _bullet.SetDamage(damageBullet);
        }
    }
}
