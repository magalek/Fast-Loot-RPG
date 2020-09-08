using UnityEngine;

public class PlayerSounds : MonoBehaviour {
    [SerializeField] private AudioClip hitClip;
    [SerializeField] private AudioClip moveClip;

    private AudioSource hitSource;
    private AudioSource movementSource;

    private AudioReverbFilter reverbFilter;
    private void Start() {
        hitSource = gameObject.AddComponent<AudioSource>();
        hitSource.volume = 0.05f;
        movementSource = gameObject.AddComponent<AudioSource>();
        movementSource.volume = 0.2f;
        reverbFilter = gameObject.AddComponent<AudioReverbFilter>();
        reverbFilter.reverbPreset = AudioReverbPreset.Cave;
    }

    public void PlayHitClip() {
        hitSource.PlayOneShot(hitClip);
    }

    public void PlayMoveSound() {
        movementSource.PlayOneShot(moveClip);
    }

    public void StopMovement() {
        movementSource.Stop();
    }
}
