using System.Collections.Generic;
using RPG;
using RPG.Utility;
using UnityEngine;

public class SoundSpawner : MonoBehaviour {
    [SerializeField] private List<AudioClip> sounds;
    [SerializeField] private List<AudioClip> deathSounds;

    private AudioSource audioSource;

    public void PlayDeathSound() {
        GameObject soundObject = new GameObject("Sound");
        audioSource = soundObject.AddComponent<AudioSource>();

        if (deathSounds.Count == 0) return;
        
        audioSource.PlayOneShot(deathSounds.Random());
        soundObject.AddComponent<SoundSource>();
    }
}
