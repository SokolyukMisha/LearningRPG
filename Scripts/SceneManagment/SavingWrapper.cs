using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Saving;
using UnityEngine;
// ReSharper disable Unity.PerformanceCriticalCodeInvocation

namespace RPG.SceneManagment
{
    public class SavingWrapper : MonoBehaviour
    {

        [SerializeField] private float fadeInTime = .2f;
        
        private const string defaultSaveFile = "save";

        IEnumerator Start()
        {
            Fader _fader = FindObjectOfType<Fader>();
            _fader.FadeOutImmediate();
            yield return GetComponent<SavingSystem>().LoadLastScene(defaultSaveFile);
            yield return _fader.FadeIn(fadeInTime);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.L))
            {
                Load();
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                Save();
            }
        }

        public void Save()
        {
            GetComponent<SavingSystem>().Save(defaultSaveFile);
        }

        public void Load()
        {
            GetComponent<SavingSystem>().Load(defaultSaveFile);
        }
    }
}   