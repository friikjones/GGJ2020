using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreen : MonoBehaviour
{

    public AudioClip clip;

    void Start()
    {
        MusicController.Play(clip);
    }

}
