using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class PositiveCharactersManager : ManagerSingletonBase<PositiveCharactersManager>
{
    #region Fields

    private List<GameObject> characters;

    #endregion

    #region Propeties

    public List<GameObject> Characters {
        get => characters;
        private set => characters = value;
    }

    #endregion

    #region Methods

    public void SpawnCharacterInCell(GridCell cell)
    {
        if(cell.IsEmpty == false)
        {
            return;
        }

        ShopManager shopManager = ShopManager.Instance;
        if(shopManager == null)
        {
            return;
        }

        GameObject characterToSpawn = shopManager.CharacterPrefab;
        if(characterToSpawn == null)
        {
            return;
        }

        GameObject newCharacter = Instantiate(characterToSpawn);
        newCharacter.transform.position = cell.SpawnPosition.position;
        newCharacter.transform.SetParent(transform);

        cell.IsEmpty = false;
        Characters.Add(newCharacter);
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        // todo: przerobic na normalne wywoalnie.
        characters = new List<GameObject>();

        Debug.LogFormat("[{0}] Zainicjalizowany.".SetColor(Color.green), this.GetType());
    }
    #endregion

    #region Handlers



    #endregion
}
