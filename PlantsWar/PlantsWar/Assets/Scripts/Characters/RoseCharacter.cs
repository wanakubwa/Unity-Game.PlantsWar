using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class RoseCharacter : CharacterBase
{
    #region Fields



    #endregion

    #region Propeties

    public bool IsColliding {
        get;
        private set;
    }

    public CharacterBase Character {
        get;
        private set;
    }

    #endregion

    #region Methods

    public override void ReciveDamage(float damage)
    {
        AddHealthPoints(-damage);
        if(HealthPoints <= 0f)
        {
            PositiveCharactersManager.Instance?.KillSpawnedCharacter(this);
        }
    }

    protected override bool CanAttack(float time)
    {
        if(IsColliding == true)
        {
            if (AttackDelayCounter > AttackDelay)
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
        else
        {
            AttackDelayCounter = 0f;
            return false;
        }
    }

    protected override void OnAttackAction(float time)
    {
        if (Character != null)
        {
            Character.ReciveDamage(AttackDamage);
        }
    }

    #endregion

    #region Handlers

    private void OnTriggerEnter2D(Collider2D positiveCharacter) 
    {
        IsColliding = true;

        Character = positiveCharacter.gameObject.GetComponent<CharacterBase>();
    }

    private void OnTriggerExit2D(Collider2D positiveCharacter) 
    {
        IsColliding = false;

        Character = null;
    }

    #endregion
}
