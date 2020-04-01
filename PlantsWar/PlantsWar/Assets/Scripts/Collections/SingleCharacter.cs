using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class SingleCharacter
{
    #region Fields

    #endregion

    #region Propeties

    public CharacterType Type
    {
        get;
        private set;
    }

    public List<CharacterBase> Characters
    {
        get;
        private set;
    }

    #endregion

    #region Methods

    public SingleCharacter(CharacterType type, List<CharacterBase> character)
    {
        Type = type;
        Characters = character;
    }

    #endregion

    #region Handlers



    #endregion
}
