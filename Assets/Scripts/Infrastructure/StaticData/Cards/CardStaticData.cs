using Cards;
using Cards.TargetSelectors;
using UnityEngine;
using Zenject;

namespace Infrastructure.StaticData.Cards
{
    public abstract class CardStaticData : ScriptableObject
    {
        [SerializeField] private string _name;
        [SerializeField] private string _description;
        [SerializeField] private int _cost;
        [SerializeField] private Sprite _icon;
        [SerializeField] private CardTargetSelectorType cardTargetSelectorType;
        [SerializeField] private CardId _id;

        public string Name => _name;
        public string Description => _description;
        public int Cost => _cost;
        public Sprite Icon => _icon;
        public CardTargetSelectorType CardTargetSelectorType => cardTargetSelectorType;
        public CardId Id => _id;
        public abstract ICardAction GetCardAction(DiContainer diContainer);
    }
}