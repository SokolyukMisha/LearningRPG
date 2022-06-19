using System;
using System.Collections;
using UnityEngine;

namespace RPG.SceneManagment
{
    public class Fader : MonoBehaviour
    {
        private CanvasGroup _canvasGroup;

        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        public void FadeOutImmediate()
        {
            _canvasGroup.alpha = 1;
        }
        
        public IEnumerator FadeOut(float time)
        {
            while (_canvasGroup.alpha < 1) 
            {
                //moving alpha toward 1
                _canvasGroup.alpha += Time.deltaTime / time;
                yield return null;

            }  
        }
        public IEnumerator FadeIn(float time)
        {
            if (_canvasGroup == null) yield break;
            while (_canvasGroup.alpha > 0) 
            {
                //moving alpha toward 0
                _canvasGroup.alpha -= Time.deltaTime / time;
                yield return null;

            }  
        }
    }
}