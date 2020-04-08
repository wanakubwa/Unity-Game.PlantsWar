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
    [SerializeField]
    private Bullet spawnBullet;

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

    public Bullet SpawnBullet { 
        get => spawnBullet; 
        private set => spawnBullet = value; 
    }

    #endregion

    #region Methods

    public override void ReciveDamage(float damage)
    {
        //AddHealthPoints(-damage);
        if(HealthPoints <= 0f)
        {
            PositiveCharactersManager.Instance?.KillSpawnedCharacter(this);
        }
    }

    protected override bool CanAttack(float time)
    {
        if (IsEnemieInRange() == true)
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

    protected override void OnAttackAction(float time)
    {
        Bullet bullet = Instantiate(SpawnBullet, transform.position, Quaternion.identity);
        bullet.transform.SetParent(transform);
        bullet.Damage = AttackDamage;
        bullet.Direction = Vector3.right;
        bullet.Speed = 90f;
    }

    private bool IsEnemieInRange()
    {
        Debug.DrawRay(transform.position, Vector3.right * Range, Color.green);
        RaycastHit2D hit =  Physics2D.Raycast(transform.position,  Vector3.right, Range, TriggerLayer.value);

        if(hit.collider && hit.collider.tag != "bullet")
        {
            return true;
        }

        return false;
    }

    #endregion

    #region Handlers


    #endregion
}
