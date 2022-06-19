using RPG.Saving;
using UnityEngine;
using UnityEngine.Playables;
using RPG.Saving;

namespace RPG.Cinematic
{


    public class CinematicTrigger : MonoBehaviour, ISaveable
    {
        bool alreadyTriggered = false;
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player") && !alreadyTriggered)
            {
                alreadyTriggered = true;
                GetComponent<PlayableDirector>().Play();
            }
        }

        public object CaptureState()
        {
            return alreadyTriggered;
        }

        public void RestoreState(object state)
        {
            alreadyTriggered = (bool)state;
        }
    }
}