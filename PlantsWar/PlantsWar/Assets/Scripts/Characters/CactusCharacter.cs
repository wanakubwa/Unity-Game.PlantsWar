using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class CactusCharacter : CharacterBase
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
            PositiveCharactersManager.Instance?.KillSpawnedCharacterOfId(Id);
        }
    }

    protected override bool CanAttack(float time)
    {
        //TODO:
        return base.CanAttack(time);
    }

    protected override void OnAttackAction(float time)
    {
        // TODO:
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
