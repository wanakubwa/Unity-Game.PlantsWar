using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using OdinSerializer;

[Serializable]
public class CharacterBase : SerializedMonoBehaviour
{
    #region Fields

    [Space]
    [SerializeField]
    private int id = -1;
    [SerializeField]
    private int upgradeLvl;

    [Space]
    [SerializeField]
    private CharacterType type;
    [SerializeField]
    private float prize;
    [SerializeField]
    private float healthPoints;
    [SerializeField]
    private int range;
    [SerializeField]
    private float attackDelay;
    [SerializeField]
    private float attackDamage;
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private Sprite sprite;
    [SerializeField]
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private Vector3 spriteScale = new Vector3(1f, 1f, 1f);
    [SerializeField]
    private Sprite shopCardBackground;

    [Space(10)]
    [Header("Animator setup")]
    [SerializeField]
    private CharacterAnimatorController characterAnimator;

    #endregion

    #region Propeties

    public CharacterType Type
    {
        get => type;
        private set => type = value;
    }

    public float Prize
    {
        get => prize;
        private set => prize = value;
    }

    public float HealthPoints
    {
        get => healthPoints;
        private set => healthPoints = value;
    }

    public int Range
    {
        get => range;
        private set => range = value;
    }

    public float AttackDelay
    {
        get => attackDelay;
        private set => attackDelay = value;
    }

    public Sprite Sprite { 
        get => sprite; 
        private set => sprite = value; 
    }

    public SpriteRenderer SpriteRenderer { 
        get => spriteRenderer; 
        private set => spriteRenderer = value; 
    }
    
    public Vector3 SpriteScale { 
        get => spriteScale; 
        private set => spriteScale = value; 
    }

    public int Id { 
        get => id; 
        private set => id = value; 
    }

    public Sprite ShopCardBackground { 
        get => shopCardBackground; 
        private set => shopCardBackground = value; 
    }

    public float AttackDamage { 
        get => attackDamage; 
        private set => attackDamage = value; 
    }

    public float MoveSpeed { 
        get => moveSpeed; 
        private set => moveSpeed = value; 
    }

    public float AttackDelayCounter{
        get; 
        protected set;
    } = 0f;

    public int CellId{
        get;
        set;
    } = -1;

    public CharacterAnimatorController CharacterAnimator { 
        get => characterAnimator; 
        private set => characterAnimator = value; 
    }

    #endregion

    #region Methods

    public void Move(float time)
    {
        if(CanMove() == true)
        {
            OnMoveAction(time);
        }
    }

    public void Attack(float time)
    {
        if(CanAttack(time) == true)
        {
            OnAttackAction(time);
        }
    }

    public virtual void ReciveDamage(float damage)
    {

    }

    public string GetCharacterName()
    {
        string key = CharactersContainerSetup.Instance.GetKeyByTypeAndId(Type, Id);
        if(key != null)
        {
            return FileContainerSetup.Instance.GetNameByLocalizedKey(key);
        }

        return string.Empty;
    }

    protected virtual bool CanMove()
    {
        return false;
    }

    protected virtual void OnMoveAction(float time)
    {

    }

    protected virtual bool CanAttack(float time)
    {
        return false;
    }

    protected virtual void OnAttackAction(float time)
    {
        CharacterAnimator.PlayAttackAnimation();
        if(AudioSelectingManager.Instance != null)
        {
            AudioSelectingManager.Instance.OnCharacterAttack(this);
        }
    }

    protected virtual void OnEnable()
    {
        SpriteRendererInitialize();
    }

    protected virtual void OnDisable() 
    {
        GridManager.Instance?.FreeCellById(CellId);
    }

    public void AddHealthPoints(float value)
    {
        HealthPoints += value;
    }

    private void SpriteRendererInitialize()
    {
        SpriteRenderer.sprite = Sprite;
        SpriteRenderer.transform.localScale = SpriteScale;
    }

    #endregion

    #region Handlers



    #endregion
}
