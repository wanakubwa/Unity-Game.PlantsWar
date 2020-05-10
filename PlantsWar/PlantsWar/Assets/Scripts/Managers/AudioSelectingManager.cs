using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class AudioSelectingManager : ManagerSingletonBase<AudioSelectingManager>
{
    #region Fields



    #endregion

    #region Propeties



    #endregion

    #region Methods

    protected override void AttachEvents()
    {
        base.AttachEvents();

        PositiveCharactersManager.Instance.OnCharacterSpawn += OnPositiveCharacterSpawn;
        PositiveCharactersManager.Instance.OnCharacterKill += OnPositiveCharacterKill;
    }

    protected override void DetachEvents()
    {
        base.DetachEvents();

        PositiveCharactersManager.Instance.OnCharacterSpawn -= OnPositiveCharacterSpawn;
        PositiveCharactersManager.Instance.OnCharacterKill -= OnPositiveCharacterKill;
    }

    private void OnPositiveCharacterSpawn(CharacterBase character)
    {
        AudioManager audioManager = AudioManager.Instance;
        if (audioManager != null)
        {
            audioManager.PlayAudioSoundByLabel(AudioContainerSetup.AudioLabel.SPAWN_POSITIVE);
        }
    }

    private void OnPositiveCharacterKill(CharacterBase character)
    {
        AudioManager audioManager = AudioManager.Instance;
        if (audioManager != null)
        {
            audioManager.PlayAudioSoundByLabel(AudioContainerSetup.AudioLabel.DEAD);
        }
    }

    #endregion

    #region Handlers



    #endregion
}
