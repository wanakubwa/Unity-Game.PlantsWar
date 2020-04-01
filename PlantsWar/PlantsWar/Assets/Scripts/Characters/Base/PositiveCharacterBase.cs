﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class PositiveCharacterBase : MonoBehaviour
{
    #region Fields

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
    private Sprite sprite;
    [SerializeField]
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private Vector3 spriteScale = new Vector3(1f, 1f, 1f);

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

    public float AttackDamage { 
        get => attackDamage; 
        private set => attackDamage = value; 
    }

    #endregion

    #region Methods

    protected virtual void OnEnable()
    {
        SpriteRendererInitialize();
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
