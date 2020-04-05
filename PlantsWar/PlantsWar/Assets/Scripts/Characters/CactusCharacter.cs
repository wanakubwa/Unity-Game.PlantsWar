using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class CactusCharacter : CharacterBase
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

    public CharacterBase Character {
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
            PositiveCharactersManager.Instance?.KillSpawnedCharacter(this);
        }
    }

    protected override bool CanAttack(float time)
    {
        return IsEnemieInRange();
    }

    protected override void OnAttackAction(float time)
    {
        // TODO:
    }

    private bool IsEnemieInRange()
    {
        Debug.DrawRay(transform.position, Vector3.right * Range, Color.green);
        RaycastHit2D hit =  Physics2D.Raycast(transform.position,  Vector3.right, Range, TriggerLayer.value);

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
}
