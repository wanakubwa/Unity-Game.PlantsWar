using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class DaisyCharacter : CharacterBase
{
    #region Fields



    #endregion

    #region Propeties



    #endregion

    #region Methods

    public override void ReciveDamage(float damage)
    {
        AddHealthPoints(-damage);
        if(HealthPoints <= 0f)
        {
            PositiveCharactersManager.Instance?.KillSpawnedCharacterOfId(Id);
        }
    }

    protected override bool CanAttack(float time)
    {
        if(AttackDelayCounter > AttackDelay)
        {
            AttackDelayCounter = 0f;
            return true;
        }
        else
        {
            AttackDelayCounter += time;
            return false;
        }
    }

    protected override void OnAttackAction(float time)
    {
        PlayerWalletManager.Instance?.TryAddMoney((int)AttackDamage);
    }

    #endregion

    #region Handlers



    #endregion
}
