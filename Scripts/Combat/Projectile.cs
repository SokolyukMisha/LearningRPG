using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Core;
using UnityEngine;
// ReSharper disable Unity.PerformanceCriticalCodeInvocation
// ReSharper disable Unity.PerformanceCriticalCodeNullComparison

namespace RPG.Combat
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private float speed = 1;
        [SerializeField] private bool isHoming = true;
        [SerializeField] private GameObject hitEffect = null;
        [SerializeField] float maxLifeTime = 10f;
        [SerializeField] GameObject[] destroyOnHit = null;
        private Health target = null;
        private float damage = 0;
        private float lifeAfterImpact = .5f;

        private void Start()
        {
            transform.LookAt(GetAimLocation()); //can dodge the projectile 
        }

        void Update()
        {
            if (target == null) return;

            if (isHoming && !target.IsDead())
            {
                transform.LookAt(GetAimLocation()); // can`t dodge the projectile
            }
            transform.Translate(Vector3.forward * (speed * Time.deltaTime));
        }

        public void SetTarget(Health target, float damage)
        {
            this.target = target;
            this.damage = damage;
            
            Destroy(gameObject, maxLifeTime);
        }

        private Vector3 GetAimLocation()
        {
            CapsuleCollider targetCapsule = target.GetComponent<CapsuleCollider>();
            if (targetCapsule == null)
            {
                return target.transform.position;
            }
            return target.transform.position + Vector3.up * targetCapsule.height/ 2;
        }

        private void OnTriggerEnter(Collider other)
        { 
            if (other.GetComponent<Health>() != target) return;
            if (target.IsDead()) return;
            target.TakeDamage(damage);
            speed = 0;
            if(hitEffect != null)
            {
               Instantiate(hitEffect, GetAimLocation(), transform.rotation);
            }

            foreach (GameObject toDestroy in destroyOnHit)
            {
                Destroy(toDestroy);
            }
            {
                
            }
            
            Destroy(gameObject, lifeAfterImpact);
        }
    }
}
