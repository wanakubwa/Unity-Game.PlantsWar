using System;
using UnityEngine;

[Serializable]
public class BettleEnemieCharacter : CharacterBase
{
    #region Fields
    
    
    
    #endregion
    
    #region Propeties
    
    public bool IsColliding {
        get;
        private set;
    }

    public CharacterBase PlayerCharacter {
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
            EnemyManager.Instance?.KillSpawnedCharacter(this);
        }
    }

    protected override bool CanAttack(float time)
    {
        if(IsColliding == true)
        {
            if (AttackDelayCounter >= AttackDelay)
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
            if(AttackDelayCounter < AttackDelay)
            {
                AttackDelayCounter += time;
            }
            return false;
        }
    }

    protected override bool CanMove()
    {
        if(IsColliding == true)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    protected override void OnAttackAction(float time)
    {
        if (PlayerCharacter != null)
        {
            PlayerCharacter.ReciveDamage(AttackDamage);
        }
    }

    protected override void OnMoveAction(float time)
    {
        transform.Translate(-1 * MoveSpeed * time * 0.00001f, 0f, 0f);
    }
    
    #endregion
    
    #region Handlers
    
    private void OnTriggerEnter2D(Collider2D positiveCharacter) 
    {
        PlayerCharacter = positiveCharacter.gameObject.GetComponent<CharacterBase>();
        if(PlayerCharacter != null)
        {
            IsColliding = true;
        }
    }

    private void OnTriggerExit2D(Collider2D positiveCharacter) 
    {
        IsColliding = false;
        PlayerCharacter = null;
    }
    
    #endregion
    
    #region Enums
    
    
    
    #endregion  
}