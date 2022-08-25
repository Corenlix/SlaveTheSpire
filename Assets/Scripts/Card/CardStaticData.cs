using System.Collections.Generic;
using System.Linq;
using Card.Actions;
using Card.Actions.Data;
using Card.Activators;
using UnityEngine;


[CreateAssetMenu(menuName = "Card/Card", order = 0)]
public class CardStaticData : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private string _description;
    [SerializeField] private int _cost;
    [SerializeField] private Sprite _icon;
    [SerializeField] private CardActivatorType _cardActivatorType;
    [SerializeField] private List<ActionData> _actionsData;

    public string Name => _name;
    public string Description => _description;
    public int Cost => _cost;
    public Sprite Icon => _icon;
    public CardActivatorType CardActivatorType => _cardActivatorType;
    public List<ICardAction> CardActions => _actionsData.Select(x=>x.GetCardAction()).ToList();
}