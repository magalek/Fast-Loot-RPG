using System.Collections.Generic;
using RPG;
using RPG.Utility;
using UnityEngine;

public class SoundSpawner : MonoBehaviour {
    [SerializeField] private List<AudioClip> sounds;
    [SerializeField] private List<AudioClip> deathSounds;
    [SerializeField] private List<AudioClip> hitSounds;

    
    private AudioSource audioSource;
    private GameObject soundObject;
    public void PlayDeathSound() {
        if (deathSounds.Count == 0) return;
        if (audioSource == null) {
            if (soundObject == null) soundObject = new GameObject("Sound");
            audioSource = soundObject.AddComponent<AudioSource>();
            soundObject.AddComponent<AudioReverbFilter>().reverbPreset = AudioReverbPreset.Cave;
        }

        audioSource.volume = 0.3f;

        audioSource.PlayOneShot(deathSounds.Random());
        soundObject.AddComponent<SoundSource>();
    }

    public void PlayHitSound() {
        if (hitSounds.Count == 0) return;
        if (audioSource == null) {
            if (soundObject == null) soundObject = new GameObject("Sound");
            audioSource = soundObject.AddComponent<AudioSource>();
            soundObject.AddComponent<AudioReverbFilter>().reverbPreset = AudioReverbPreset.Cave;
        }
        
        audioSource.volume = 0.3f;
        
        audioSource.PlayOneShot(hitSounds.Random());
        soundObject.AddComponent<SoundSource>();
    }
}
