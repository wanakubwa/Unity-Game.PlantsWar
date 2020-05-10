using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

public class AudioElement : MonoBehaviour
{
    #region Fields

    [SerializeField]
    private AudioSource audio;
    [SerializeField]
    private bool playOnAwake;

    #endregion

    #region Propeties

    public AudioSource Audio { 
        get => audio; 
        private set => audio = value; 
    }

    public bool PlayOnAwake { 
        get => playOnAwake; 
        private set => playOnAwake = value; 
    }

    #endregion

    #region Methods

    public void SetVolume(float volume)
    {
        Audio.volume = volume;
    }

    public void PlayOneShotAudio()
    {
        Audio.PlayOneShot(Audio.clip);
    }

    public void StopAudio()
    {
        Audio.Stop();
    }

    public void DestroyAudio()
    {
        StopAudio();
        Destroy(gameObject);
    }

    private void Awake()
    {
        if (PlayOnAwake == true)
        {
            PlayOneShotAudio();
        }
    }

    #endregion

    #region Handlers



    #endregion
}
