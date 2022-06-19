using System;
using RPG.Control;
using UnityEngine;
using UnityEngine.Playables;
using RPG.Core;
using RPG.Control;

namespace RPG.Cinematic
{

    public class CinematicControlRemover : MonoBehaviour
    {
        private GameObject player;
        private void Start()
        { 
            player = GameObject.FindWithTag("Player");
            GetComponent<PlayableDirector>().played += DisableControl;   
            GetComponent<PlayableDirector>().stopped += EnableControl;   
        }

        void DisableControl(PlayableDirector pd)
        {
            player.GetComponent<ActionScheduler>().CancelCurrentAction();
            player.GetComponent<PlayerController>().enabled = false;
        }

        void EnableControl(PlayableDirector pd)
        {
            print("Enable control");
            player.GetComponent<PlayerController>().enabled = true;
        }
    }
}
