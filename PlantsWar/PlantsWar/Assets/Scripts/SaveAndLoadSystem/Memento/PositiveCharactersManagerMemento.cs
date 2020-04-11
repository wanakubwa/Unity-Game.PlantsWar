using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OdinSerializer;


public class PositiveCharactersManagerMemento
{
    #region Fields



    #endregion

    #region Propeties

    [OdinSerialize]
    public List<CharacterBaseMemento> SpawnedCharacters
    {
        get;
        set;
    } = new List<CharacterBaseMemento>();

    #endregion

    #region Methods

    public void CreateCharactersMementoCollection(List<CharacterBase> characterBases)
    {
        for(int i = 0; i < characterBases.Count; i++)
        {
            AddCharacterToCollection(characterBases[i]);
        }
    }

    public void AddCharacterToCollection(CharacterBase character)
    {
        CharacterBaseMemento characterMemento = new CharacterBaseMemento(character.Id, character.Type, character.CellId, character.HealthPoints);
        SpawnedCharacters.Add(characterMemento);
    }

    #endregion

    #region Handlers



    #endregion
}
