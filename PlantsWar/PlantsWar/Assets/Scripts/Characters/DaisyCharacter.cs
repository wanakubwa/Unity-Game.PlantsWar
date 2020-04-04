using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public class DaisyCharacter : CharacterBase
{
    #region Fields



    #endregion

    #region Propeties



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

    #endregion

    #region Handlers



    #endregion
}
