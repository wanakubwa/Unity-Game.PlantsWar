using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OdinSerializer;

[Serializable]
public class CharacterBaseMemento
{

    #region Fields



    #endregion

    #region Propeties

    [OdinSerialize]
    public int Id {
        get;
        set;
    } = -1;

    [OdinSerialize]
    public CharacterType Type {
        get;
        set;
    }

    [OdinSerialize]
    public int CellId {
        get;
        set;
    } = -1;

    [OdinSerialize]
    public float HealthPoints
    {
        get;
        set;
    }

    #endregion

    #region Methods

    public CharacterBaseMemento(int id, CharacterType type, int cellId, float healthPoints)
    {
        Id = id;
        Type = type;
        CellId = cellId;
        HealthPoints = healthPoints;
    }

    #endregion

    #region Handlers



    #endregion
}
