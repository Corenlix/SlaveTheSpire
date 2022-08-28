using System.Collections.Generic;
using Card;
using Entities;
using Zenject;

namespace Infrastructure
{
    public class CardActivator : ICardActivator
    {
        private readonly DiContainer _diContainer;
        private readonly PlayerHolder _playerHolder;

        public CardActivator(DiContainer diContainer, PlayerHolder playerHolder)
        {
            _diContainer = diContainer;
            _playerHolder = playerHolder;
        }

        public bool IsAvailableToUse(CardHolder cardHolder)
        {
            return _playerHolder.Energy.CurrentValue >= cardHolder.CardStaticData.Cost;
        }

        public void Use(CardHolder cardHolder, List<Entity> targets) {
            _playerHolder.Energy.Subtract(cardHolder.CardStaticData.Cost);
            cardHolder.CardStaticData.GetCardActions(_diContainer).ForEach(x=>x.Activate(targets));
        }
    }
}