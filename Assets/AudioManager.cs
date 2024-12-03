using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("------Audio Source------")]
   [SerializeField] public AudioSource mainSource;
   [SerializeField] public AudioSource SFXSource;

    [Header("------Audio Clips------")]

   public AudioClip BGMMain;
   public AudioClip shootAcorn;
   public AudioClip shootslingshot;
   public AudioClip jump;
   public AudioClip walk;
   public AudioClip damage;
   public AudioClip Death;

   public AudioClip BossDeath;

   private void Start()
   {
    mainSource.clip = BGMMain;
    mainSource.Play();
   }

   public void PlaySFX(AudioClip clip)
   {
    SFXSource.PlayOneShot(clip);
   }


}
