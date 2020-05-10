using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class AudioManager : ManagerSingletonBase<AudioManager>
{
    #region Fields

    [ShowInInspector ,NonSerialized]
    private AudioTrack currentSoundTrack = null;

    #endregion

    #region Propeties

    public AudioTrack CurrentSoundTrack { 
        get => currentSoundTrack; 
        private set => currentSoundTrack = value; 
    }

    #endregion

    #region Methods

    public void PlayAudioSoundByLabel(AudioContainerSetup.AudioLabel label)
    {
        AudioContainerSetup audioContainer = AudioContainerSetup.Instance;
        if(audioContainer == null)
        {
            return;
        }

        AudioElement audioElement = audioContainer.GetAudioElementByLabel(label);
        if(audioElement != null)
        {
            if(CurrentSoundTrack != null)
            {
                if(CurrentSoundTrack.IsTrackEqual(label) == true)
                {
                    CurrentSoundTrack.ResetAudio();
                }
                else
                {
                    PlayAudioSoundTrack(audioElement, label);
                }
            }
            else
            {
                PlayAudioSoundTrack(audioElement, label);
            }
        }
    }

    private void PlayAudioSoundTrack(AudioElement audio, AudioContainerSetup.AudioLabel label)
    {
        if(CurrentSoundTrack != null)
        {
            CurrentSoundTrack.DestroyAudio();
        }

        AudioElement audioElement = Instantiate(audio);
        audioElement.transform.SetParent(transform);
        CurrentSoundTrack = new AudioTrack(audioElement, label);
    }

    #endregion

    #region Handlers



    #endregion

    [Serializable]
    public class AudioTrack
    {
        #region Fields

        [SerializeField]
        private AudioContainerSetup.AudioLabel label;
        [SerializeField]
        private AudioElement audioElement;

        #endregion

        #region Propeties

        public AudioContainerSetup.AudioLabel Label { 
            get => label; 
            private set => label = value; 
        }

        public AudioElement AudioElement { 
            get => audioElement; 
            private set => audioElement = value; 
        }

        #endregion

        #region Methods

        public AudioTrack(AudioElement audio, AudioContainerSetup.AudioLabel label)
        {
            AudioElement = audio;
            Label = label;
        }

        public bool IsTrackEqual(AudioContainerSetup.AudioLabel Label)
        {
            if(Label == label)
            {
                return true;
            }

            return false;
        }

        public void ResetAudio()
        {
            StopAudio();
            PlayAudio();
        }

        public void StopAudio()
        {
            AudioElement.StopAudio();
        }

        public void PlayAudio()
        {
            AudioElement.PlayOneShotAudio();
        }

        public void DestroyAudio()
        {
            AudioElement.DestroyAudio();
        }

        #endregion

        #region Handlers



        #endregion
    }
}
