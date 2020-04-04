using UnityEngine;

public class BettleEnemieCharacter : CharacterBase
{
    #region Fields
    
    
    
    #endregion
    
    #region Propeties
    
    public bool IsColliding {
        get;
        private set;
    }

    public float AttackDelayCounter{
        get; 
        private set;
    } = 0f;

    public CharacterBase PlayerCharacter {
        get;
        private set;
    }
    
    #endregion
    
    #region Methods
    
    protected override bool CanAttack()
    {
        if(IsColliding == true)
        {
            return true;
        }
        else
        {
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
        if (AttackDelayCounter > AttackDelay)
        {
            if (PlayerCharacter != null)
            {
                PlayerCharacter.ReciveDamage(AttackDamage);
            }

            AttackDelayCounter = 0f;
        }
        else
        {
            AttackDelayCounter += time;
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
        IsColliding = true;

        PlayerCharacter = positiveCharacter.gameObject.GetComponent<CharacterBase>();
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