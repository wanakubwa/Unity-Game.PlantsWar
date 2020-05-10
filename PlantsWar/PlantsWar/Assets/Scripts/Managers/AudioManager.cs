using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : ManagerSingletonBase<AudioManager>
{
    #region Fields

    [ShowInInspector ,NonSerialized]
    private AudioTrack currentSoundTrack = null;
    [ShowInInspector, NonSerialized]
    private AudioAmbientTrack currentAmbientTrack = null;

    [Space(10)]
    [SerializeField]
    private float ambientVolume = 0.5f;

    #endregion

    #region Propeties

    public event Action<float> OnAmbientVolumeChanged = delegate { };

    public AudioTrack CurrentSoundTrack { 
        get => currentSoundTrack; 
        private set => currentSoundTrack = value; 
    }

    public AudioAmbientTrack CurrentAmbientTrack { 
        get => currentAmbientTrack; 
        private set => currentAmbientTrack = value; 
    }

    public float AmbientVolume { 
        get => ambientVolume; 
        private set => ambientVolume = value; 
    }

    #endregion

    #region Methods

    public void SetAmbientVolume(float value)
    {
        AmbientVolume = Mathf.Clamp(value, 0f, 1f);
        if(CurrentAmbientTrack != null)
        {
            CurrentAmbientTrack.SetAudioVolume(AmbientVolume);
        }

        OnAmbientVolumeChanged(AmbientVolume);
    }

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

    public void PlayAmbientSoundBySceneId(int sceneId)
    {
        AudioContainerSetup audioContainer = AudioContainerSetup.Instance;
        if (audioContainer == null)
        {
            return;
        }

        AudioElement audioElement = audioContainer.GetAudioElementBySceneId(sceneId);
        if (audioElement != null)
        {
            PlayAmbientSoundTrack(audioElement, sceneId);
        }
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        PlayAmbientSoundBySceneId(SceneManager.GetActiveScene().buildIndex);
    }

    protected override void AttachEvents()
    {
        base.AttachEvents();

        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    protected override void DetachEvents()
    {
        base.DetachEvents();

        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
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

    private void PlayAmbientSoundTrack(AudioElement audio, int sceneId)
    {
        if (CurrentAmbientTrack != null)
        {
            CurrentAmbientTrack.DestroyAudio();
        }

        AudioElement audioElement = Instantiate(audio);
        audioElement.transform.SetParent(transform);
        CurrentAmbientTrack = new AudioAmbientTrack(audioElement, sceneId);
    }

    #endregion

    #region Handlers

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        PlayAmbientSoundBySceneId(scene.buildIndex);
    }

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

        public AudioTrack(AudioElement audio)
        {
            AudioElement = audio;
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

    [Serializable]
    public class AudioAmbientTrack
    {
        #region Fields

        [SerializeField]
        private int sceneId;
        [SerializeField]
        private AudioElement audioElement;

        #endregion

        #region Propeties

        public AudioElement AudioElement
        {
            get => audioElement;
            private set => audioElement = value;
        }

        public int SceneId { 
            get => sceneId; 
            private set => sceneId = value; 
        }

        #endregion

        #region Methods

        public AudioAmbientTrack(AudioElement audio, int sceneId)
        {
            AudioElement = audio;
            SceneId = SceneId;
        }

        public bool IsTrackEqual(int sceneId)
        {
            if (SceneId == sceneId)
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

        public void SetAudioVolume(float volume)
        {
            AudioElement.SetVolume(volume);
        }

        #endregion

        #region Handlers

        #endregion
    }
}
