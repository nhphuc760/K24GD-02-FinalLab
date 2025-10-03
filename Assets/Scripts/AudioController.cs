using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioController : MonoBehaviour
{
    public static AudioController Ins { get; private set; }
    public float Volume { get => volume; set { 
            volume = value;
            m_AudioMusic.volume = 0.3f * volume;
            m_AudioSound.volume = volume;
            PlayerPrefs.SetFloat(VOLUME, value);
        } }

    public AudioSource m_AudioMusic;
    public  AudioSource m_AudioSound;
    public  List<AudioClip> music = new List<AudioClip>();
     float volume;
    public AudioClip die;
    public AudioClip hit;
    public AudioClip sword;
    public const string VOLUME = "volume";
    public bool dontDestroyOnLoad = false;
  
    AudioClip curMusic;
    public  void Awake()
    {
       if(Ins != null && Ins != this )
        {
            Destroy(gameObject);
            return;
        }
       Ins = this;
       
        if (dontDestroyOnLoad)
        {
            DontDestroyOnLoad(this);
        }
       
    }

    public  void Start()
    {
        volume = PlayerPrefs.GetFloat(VOLUME, 1f);
        if (dontDestroyOnLoad)
        {
            StartCoroutine(PlayAndWait());
        }
      
    }

    public void PlaySound(AudioClip clip, AudioSource audio = null)
    {
        audio ??= m_AudioSound;
        if (audio)
        {
            audio.volume=volume;
            audio.PlayOneShot(clip);
        }
    }
    public void PlayMusic(AudioClip music, bool loop = true)
    {
        if(music != null)
        {
            m_AudioMusic.volume=.3f * volume; //30% volumesound
            m_AudioMusic.clip = music;
            m_AudioMusic.loop = loop;
            m_AudioMusic.Play();
                       
        }
    }
    public void PlayMusic(List<AudioClip> music, bool loop = true)
    {
        if (music != null)
        {
            m_AudioMusic.volume = .3f * volume; //30% volumesound
            int index = Random.Range(0, music.Count);
            m_AudioMusic.clip = music[index];
            curMusic = m_AudioMusic.clip;
            m_AudioMusic.loop = loop;
            m_AudioMusic.Play();

        }
    }


    public void StopMusic()
    {
        m_AudioMusic.Stop();
    }
   
    IEnumerator PlayAndWait()
    {
        
        while(true) {
        PlayMusic(music);
        float time = curMusic.length;
            yield return new WaitForSecondsRealtime(time);
        }
        
    }

}
