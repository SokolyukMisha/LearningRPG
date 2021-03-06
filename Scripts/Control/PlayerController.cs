using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Combat;
using RPG.Core;
using RPG.Movement;
using UnityEngine;
// ReSharper disable Unity.PerformanceCriticalCodeInvocation
// ReSharper disable Unity.PreferNonAllocApi

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        private Fighter _fighter;
        private Health _health;

        private void Start()
        {
            _health = GetComponent<Health>();
            _fighter = GetComponent<Fighter>();
        }

        private void Update()
        {
            if (_health.IsDead()) return;
            
            if(InteractWithCombat()) return;
            if(InteractWithMovement()) return;
        }
        
        private bool InteractWithCombat()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            foreach (RaycastHit hit in hits)
            {
                CombatTarget target =  hit.transform.GetComponent<CombatTarget>();
                
                if(target == null) continue;
                
                if(!_fighter.CanAttack(target.gameObject)) continue;

                if (Input.GetMouseButton(0))
                {
                    GetComponent<Fighter>().Attack(target.gameObject);
                }
                return true;
            }

            return false;
        }
        
        private bool InteractWithMovement()
        {
            RaycastHit hit;
            bool hasHit = Physics.Raycast(GetMouseRay(), out hit);
            if (hasHit)
            {
                if (Input.GetMouseButton(0))
                {
                    GetComponent<Mover>().StartMoveAction(hit.point, 1f);
                }

                return true;
            }
            return false;
        }

        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}
