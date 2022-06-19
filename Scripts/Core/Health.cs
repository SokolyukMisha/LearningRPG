using UnityEngine;
using RPG.Saving;
// ReSharper disable Unity.PerformanceCriticalCodeInvocation

namespace RPG.Core
{
    public class Health : MonoBehaviour, ISaveable
    {
        [SerializeField] float hitPoints = 100f;
        private bool _isDead = false;

        public bool IsDead()
        {
            return _isDead;
        }

        public void TakeDamage(float damage)
        {
            hitPoints = Mathf.Max(hitPoints - damage, 0);
            print(hitPoints);
            if (hitPoints == 0 && !_isDead)
            {
                Die();
            }
        }

        private void Die()
        {
            _isDead = true;
            GetComponent<Animator>().SetTrigger("die");
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        public object CaptureState()
        {
            return hitPoints;
        }

        public void RestoreState(object state)
        {
            hitPoints = (float)state;
            if (hitPoints == 0)
            {
                Die();
            }
        }
    }
}
