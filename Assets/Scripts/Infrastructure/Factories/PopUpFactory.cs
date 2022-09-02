using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PopUpFactory : IPopUpFactory
{
    private const string PopUpPrefabs = "PopUpPrefabs";
    private Dictionary<PopUpType, PopUp> _popUpPrefabs;

    public PopUpFactory()
    {
        Load();
    }

    private void Load()
    {
        _popUpPrefabs = Resources.LoadAll<PopUp>(PopUpPrefabs)
            .ToDictionary(x => x.PopUpType, x => x);
    }
    
    public PopUp ForType(PopUpType popUpType)
    {
        return Object.Instantiate(_popUpPrefabs[popUpType]);
    }
}