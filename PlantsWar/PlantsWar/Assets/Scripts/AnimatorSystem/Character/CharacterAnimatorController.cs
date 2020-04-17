using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class CharacterAnimatorController : MonoBehaviour
{
    #region Fields

    [Space(5)]
    [SerializeField]
    private Animator characterAnimator;

    [Space]
    [Header("Nazwy triggerow animacji")]
    [SerializeField]
    private string idleTriggerName = "isIdle";
    [SerializeField]
    private string attackTriggerName = "isAttack";
    [SerializeField]
    private string deathTriggerName = "IsDeath";

    #endregion

    #region Propeties

    public Animator CharacterAnimator { 
        get => characterAnimator; 
        private set => characterAnimator = value; 
    }

    #endregion

    #region Methods

    public void PlayIdleAnimation()
    {
        ResetTriggers();
        CharacterAnimator.SetTrigger(idleTriggerName);
    }

    public void PlayAttackAnimation()
    {
        ResetTriggers();
        CharacterAnimator.SetTrigger(attackTriggerName);
    }

    public void PlayDeathAnimation()
    {
        ResetTriggers();
        CharacterAnimator.SetTrigger(deathTriggerName);
    }

    private void ResetTriggers()
    {
        CharacterAnimator.ResetTrigger(idleTriggerName);
        CharacterAnimator.ResetTrigger(attackTriggerName);
        CharacterAnimator.ResetTrigger(deathTriggerName);
    }

    #endregion

    #region Handlers



    #endregion
}
