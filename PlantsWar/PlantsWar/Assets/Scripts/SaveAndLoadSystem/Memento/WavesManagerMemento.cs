using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OdinSerializer;

[Serializable]
public class WavesManagerMemento
{
    #region Fields

    #endregion

    #region Propeties

[OdinSerialize]
    public float StartDelayCounter {
        get;
        set;
    }

    [OdinSerialize]
    public float RowDelayCounter {
        get;
        set;
    }

    [OdinSerialize]
    public float SpawnCharacterDelayCounter {
        get;
        set;
    }

    [OdinSerialize]
    public int RowsCounter {
        get;
        set;
    }

    [OdinSerialize]
    public int CharactersInRowCounter {
        get;
        set;
    }

    [OdinSerialize]
    public int WavesCounter {
        get;
        set;
    }

    #endregion

    #region Methods

    #endregion

    #region Handlers

    #endregion
}