using System.Collections.Generic;
using System.Linq;
using Card.Actions;
using Card.TargetSelectors;
using Infrastructure.StaticData.Cards.CardActions;
using UnityEngine;
using Zenject;

namespace Infrastructure.StaticData.Cards
{
    [CreateAssetMenu(menuName = "Card/Card", order = 0)]
    public class CardStaticData : ScriptableObject
    {
        [SerializeField] private string _name;
        [SerializeField] private string _description;
        [SerializeField] private int _cost;
        [SerializeField] private Sprite _icon;
        [SerializeField] private CardTargetSelectorType cardTargetSelectorType;
        [SerializeField] private List<ActionData> _actionsData;
        [SerializeField] private CardId _id;

        public string Name => _name;
        public string Description => _description;
        public int Cost => _cost;
        public Sprite Icon => _icon;
        public CardTargetSelectorType CardTargetSelectorType => cardTargetSelectorType;
        public List<ICardAction> GetCardActions(DiContainer diContainer) => _actionsData.Select(x=>x.GetCardAction(diContainer)).ToList();
        public CardId Id => _id;
    }
}