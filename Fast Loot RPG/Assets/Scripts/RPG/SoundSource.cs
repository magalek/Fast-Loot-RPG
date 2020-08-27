using System;
using UnityEngine;

namespace RPG {
    public class SoundSource : MonoBehaviour {
       
        private AudioSource audioSource;
        private void Awake() {
            audioSource = GetComponent<AudioSource>();
        }

        private void Update() {
            if (!audioSource.isPlaying) {
                Destroy(gameObject);
            }
        }
    }
}