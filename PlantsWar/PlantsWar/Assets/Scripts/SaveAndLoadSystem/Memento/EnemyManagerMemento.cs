﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OdinSerializer;

[Serializable]
public class EnemyManagerMemento
{
    #region Fields

    [OdinSerialize]
    private List<CharacterBase> enemyCharactersSpawned;

    #endregion

    #region Propeties

    public List<CharacterBase> EnemyCharactersSpawned {
        get => enemyCharactersSpawned;
        set => enemyCharactersSpawned = value;
    }

    #endregion

    #region Methods


    #endregion

    #region Handlers



    #endregion
}
