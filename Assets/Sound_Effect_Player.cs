using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound_Effect_Player : MonoBehaviour
{
    public AudioSource src;
    public AudioClip jump;

    public void Jump(){
        src.clip = jump;
        src.Play();
    }

}
