using UnityEngine;

public class SpiderEnemieCharacter : CharacterBase
{
    #region Fields
    
    [Space]
    [SerializeField]
    private LayerMask triggerLayer;
    
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

    public LayerMask TriggerLayer {
        get => triggerLayer;
        private set => triggerLayer = value;
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
        if (IsEnemieInRange() == true)
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

    protected override bool CanMove()
    {
        return IsEnemieInRange();
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

    private bool IsEnemieInRange()
    {
        Debug.DrawRay(transform.position, Vector3.right * Range, Color.magenta);
        RaycastHit2D hit =  Physics2D.Raycast(transform.position,  Vector3.left, Range, TriggerLayer.value);

        if(hit.collider)
        {
            Debug.Log("Cactus fire!".SetColor(Color.magenta));
            return true;
        }

        return false;
    }
    
    #endregion
    
    #region Handlers

    
    #endregion
    
    #region Enums
    
    
    
    #endregion  
}