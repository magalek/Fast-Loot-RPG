using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicSource : MonoBehaviour {
    [SerializeField] private AudioClip menuMusic;
    [SerializeField] private AudioClip menuHitSound;

    
    
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);   
        
        var musicSource = GetComponent<AudioSource>();

        SceneManager.activeSceneChanged += (s, c) => StartCoroutine(FadeOutMusic(musicSource));
    }

    private IEnumerator FadeOutMusic(AudioSource source) {
        for (int i = 0; i < 10; i++) {
            source.volume = -0.1f;
            yield return new WaitForSeconds(1/2f);
        }
    }
    
    public void PlayHitSound() {
        var source = gameObject.AddComponent<AudioSource>();
        source.playOnAwake = false;
        source.volume = 0.4f;
        
        source.PlayOneShot(menuHitSound);
    }
}
