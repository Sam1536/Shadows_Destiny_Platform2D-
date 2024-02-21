using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    private AudioSource audioSoucePlayer;

    public AudioClip coinSound;
    public AudioClip jumpSound;
    public AudioClip attackSound;
    public AudioClip hitSound;

    // Start is called before the first frame update
    void Start()
    {
        audioSoucePlayer = GetComponent<AudioSource>();
    }

    public void PlayAudio(AudioClip clip)
    {
        audioSoucePlayer.PlayOneShot(clip);
    }
}
