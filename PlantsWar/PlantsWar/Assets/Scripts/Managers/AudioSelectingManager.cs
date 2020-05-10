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

    public void OnCharacterAttack(CharacterBase character)
    {
        AudioManager audioManager = AudioManager.Instance;
        if (audioManager != null)
        {
            audioManager.PlayAudioSoundByLabel(AudioContainerSetup.AudioLabel.SHOOT);
        }
    }

    protected override void AttachEvents()
    {
        base.AttachEvents();

        PositiveCharactersManager.Instance.OnCharacterSpawn += OnPositiveCharacterSpawnHandler;
        PositiveCharactersManager.Instance.OnCharacterKill += OnPositiveCharacterKillHandler;
        EnemyManager.Instance.OnEnemieSpawned += OnEnemieSpawnedHandler;
    }

    protected override void DetachEvents()
    {
        base.DetachEvents();

        PositiveCharactersManager.Instance.OnCharacterSpawn -= OnPositiveCharacterSpawnHandler;
        PositiveCharactersManager.Instance.OnCharacterKill -= OnPositiveCharacterKillHandler;
        EnemyManager.Instance.OnEnemieSpawned -= OnEnemieSpawnedHandler;
    }

    private void OnPositiveCharacterSpawnHandler(CharacterBase character)
    {
        AudioManager audioManager = AudioManager.Instance;
        if (audioManager != null)
        {
            audioManager.PlayAudioSoundByLabel(AudioContainerSetup.AudioLabel.SPAWN_POSITIVE);
        }
    }

    private void OnPositiveCharacterKillHandler(CharacterBase character)
    {
        AudioManager audioManager = AudioManager.Instance;
        if (audioManager != null)
        {
            audioManager.PlayAudioSoundByLabel(AudioContainerSetup.AudioLabel.DEAD);
        }
    }

    private void OnEnemieSpawnedHandler(CharacterBase character)
    {
        AudioManager audioManager = AudioManager.Instance;
        if (audioManager != null)
        {
            audioManager.PlayAudioSoundByLabel(AudioContainerSetup.AudioLabel.SPAWN_ENEMIE);
        }
    }

    #endregion

    #region Handlers



    #endregion
}
