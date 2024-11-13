using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PaperCollected : MonoBehaviour
{
    public AudioClip paperCollectedClip;
    private AudioSource paperCollectedAudioSource;
    public void OnTriggerEnter(Collider other)
    {
        paperCollectedAudioSource = gameObject.AddComponent<AudioSource>();
        paperCollectedAudioSource.clip = paperCollectedClip;
        paperCollectedAudioSource.Play();
        gameObject.SetActive(false);
    }
}
