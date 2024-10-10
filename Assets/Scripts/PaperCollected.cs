using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PaperCollected : MonoBehaviour
{
    public AudioSource paperCollectedAudioSource; //make sure you assign an actual clip here in the inspector
    private AudioClip paperCollectedClip;
    public void OnTriggerEnter(Collider other)
    {
        paperCollectedAudioSource = gameObject.AddComponent<AudioSource>();
        paperCollectedAudioSource.clip = paperCollectedClip;
        paperCollectedAudioSource.Play();
        gameObject.SetActive(false);
    }
}
