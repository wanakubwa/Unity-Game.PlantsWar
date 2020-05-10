using Boo.Lang.Environments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "AudioContainerSetup.asset", menuName = "Settings/AudioContainerSetup")]
public class AudioContainerSetup : ScriptableObject
{
    #region Fields

    private static AudioContainerSetup instance;

    [SerializeField]
    private List<SingleAudio> audioCollection = new List<SingleAudio>();

    #endregion

    #region Propeties

    public static AudioContainerSetup Instance
    {
        get
        {
            if (instance == null)
            {
                instance = Resources.Load<AudioContainerSetup>("Setups/AudioContainerSetup");
            }

            return instance;
        }
        set
        {
            instance = value;
        }
    }

    public List<SingleAudio> AudioCollection { 
        get => audioCollection; 
        private set => audioCollection = value; 
    }

    #endregion

    #region Methods

    public AudioElement GetAudioElementByLabel(AudioLabel label)
    {
        if(AudioCollection == null)
        {
            Debug.Log("Brak elementow dzwiekow podpietych!");
            return null;
        }

        for(int i = 0; i < AudioCollection.Count; i++)
        {
            if(AudioCollection[i].Label == label)
            {
                return AudioCollection[i].Audio;
            }
        }

        return null;
    }

    #endregion

    #region Handlers



    #endregion

    [Serializable]
    public class SingleAudio
    {
        #region Fields

        [SerializeField]
        private AudioLabel label;
        [SerializeField]
        private AudioElement audio;

        #endregion

        #region Propeties

        public AudioLabel Label { 
            get => label; 
            private set => label = value; 
        }

        public AudioElement Audio { 
            get => audio; 
            private set => audio = value; 
        }

        #endregion

        #region Methods



        #endregion

        #region Handlers



        #endregion
    }

    public enum AudioLabel
    {
        SPAWN_POSITIVE,
        SPAWN_ENEMIE,
        DEAD,
        SHOOT,
        UI_BUTTON_PRESS
    }
}
