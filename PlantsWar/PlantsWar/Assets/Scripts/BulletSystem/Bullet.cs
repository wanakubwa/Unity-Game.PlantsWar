using UnityEngine;

public class Bullet : MonoBehaviour
{
    #region Fields
    
    
    
    #endregion
    
    #region Propeties
    
    public Vector3 Direction {
        get;
        set;
    }

    public float Damage {
        get;
        set;
    }

    public float Speed
    {
        get;
        set;
    }
    
    #endregion
    
    #region Methods

    private void Update() 
    {
        transform.Translate(Direction.x * Speed * Time.deltaTime * 0.01f, 0f, 0f);
    }
    
    #endregion
    
    #region Handlers
    
    private void OnTriggerEnter2D(Collider2D positiveCharacter) 
    {
        CharacterBase character =  positiveCharacter.gameObject.GetComponent<CharacterBase>();
        if(character != null)
        {
            character.ReciveDamage(Damage);
            Destroy(gameObject);
        }
    }

    #endregion
    
    #region Enums
    
    
    
    #endregion    
}