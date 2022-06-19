using System;
using RPG.Combat;
using RPG.Core;
using RPG.Movement;
using UnityEngine;
// ReSharper disable Unity.PerformanceCriticalCodeInvocation

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] private float suspicionTime = 3f;
        [SerializeField] private float chaseDistance = 5f;
        [SerializeField] private float waypointTolerance = 1f;
        [SerializeField] private float waypointDwellTime = .5f;
        [Range(0,1)]
        [SerializeField] private float patrolSpeedFraction = .3f;
        [SerializeField] private PatrolPath patrolPath;
        
        private GameObject player;

        private Fighter _fighter;
        private Health _health;
        private Mover _mover;
        private ActionScheduler _scheduler;

        private Vector3 guardPosition;
        private float timeSinceLastSawPlayer = Mathf.Infinity;
        private float timeSinceArrivedAtWaypoint = Mathf.Infinity;
        private int currentWaypointIndex;
        private void Start()
        {
            _mover = GetComponent<Mover>();
            _health = GetComponent<Health>();
            _fighter = GetComponent<Fighter>();
            _scheduler = GetComponent<ActionScheduler>();
            
            player =  GameObject.FindWithTag("Player");
            
            guardPosition = transform.position;
        }

        private void Update()
        {
            if (_health.IsDead())
            {
                return;
            }
            if (InAttackRangeOfPlayer() && _fighter.CanAttack(player)) //If in attack range and can attack AI attack
            {
                timeSinceLastSawPlayer = 0f;
                _fighter.Attack(player);
            }
            else if (timeSinceLastSawPlayer < suspicionTime) //If in attack range and can attack AI attack
            {
                _scheduler.CancelCurrentAction();
            }
            else  //If in attack range and can attack AI attack
            {
                PatrolBehaviour();
            }
            UpdateTimers();
        }

        private void PatrolBehaviour()
        {
            Vector3 nextPosition = guardPosition;

            if (patrolPath != null)
            {
                if (AtWaypoint())
                {
                    timeSinceArrivedAtWaypoint = 0;
                    CycleWaypoint();
                }

                nextPosition = GetCurrentWaypoint();
            }

            if (timeSinceArrivedAtWaypoint >  waypointDwellTime)
            {
                _mover.StartMoveAction(nextPosition, patrolSpeedFraction);
            }
        }

        private Vector3 GetCurrentWaypoint()
        {
            return patrolPath.GetWaypoint(currentWaypointIndex);
        }

        private void CycleWaypoint()
        {
            currentWaypointIndex = patrolPath.GetNextIndex(currentWaypointIndex);
        }

        private bool AtWaypoint()
        {
            float distanceToWaypoint = Vector3.Distance(transform.position, GetCurrentWaypoint());
            return distanceToWaypoint <= waypointTolerance;
        }

        private bool InAttackRangeOfPlayer()
        {
            float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
            return distanceToPlayer < chaseDistance;
        }

        //Called by Unity
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
        }

        private void UpdateTimers()
        {
            timeSinceLastSawPlayer += Time.deltaTime;
            timeSinceArrivedAtWaypoint += Time.deltaTime;
        }
    }
}
