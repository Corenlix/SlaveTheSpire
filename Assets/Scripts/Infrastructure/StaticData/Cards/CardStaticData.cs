using System.Collections.Generic;
using Card.Actions.Data;
using Card.TargetSelectors;
using UnityEngine;

namespace Infrastructure.StaticData
{
    [CreateAssetMenu(menuName = "Card/Card", order = 0)]
    public class CardStaticData : ScriptableObject
    {
        [SerializeField] private string _name;
        [SerializeField] private string _description;
        [SerializeField] private int _cost;
        [SerializeField] private Sprite _icon; 
        [SerializeField] private CardTargetSelectorType _cardTargetSelectorType;
        [SerializeField] private List<CardActionStaticData> actionsData;
        [SerializeField] private CardId _id;

        public string Name => _name;
        public string Description => _description;
        public int Cost => _cost;
        public Sprite Icon => _icon;
        public CardTargetSelectorType CardTargetSelectorType => _cardTargetSelectorType;
        public List<CardActionStaticData> CardActionsStaticData => actionsData;
        public CardId Id => _id;
    }
}