using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicController : MonoBehaviour {

	private static MusicController instance = null;

    private AudioSource audioSource;

	public static MusicController Instance {
		get{
			if (instance == null) {
				GameObject go = new GameObject("MusicController");
				instance = go.AddComponent<MusicController>();
				instance.audioSource = go.AddComponent<AudioSource>();
				DontDestroyOnLoad(go);
			}
			return instance;
		}
	}

    private void Awake(){
		if (instance == null){
			instance = this;
			DontDestroyOnLoad(gameObject);
		}else if(instance != this){
			instance.GetComponent<AudioSource>().Stop();
			Destroy(instance.gameObject);
			instance = this;
			DontDestroyOnLoad(gameObject);
        }
		audioSource = GetComponent<AudioSource>();
	}

    public static void Play(AudioClip clip){
        Instance.audioSource.clip = clip;
        Instance.audioSource.loop = true;
        Instance.audioSource.Play();
    }

    public static void Stop(){
        Instance.audioSource.Stop();
    }

}
