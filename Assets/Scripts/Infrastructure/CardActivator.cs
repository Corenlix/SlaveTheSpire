using System.Collections.Generic;
using Card;
using Entities;
using Zenject;

namespace Infrastructure
{
    public class CardActivator
    {
        private readonly DiContainer _diContainer;
        
        public CardActivator(DiContainer diContainer)
        {
            _diContainer = diContainer;
        }
        
        public void Use(CardHolder cardHolder, List<Entity> targets) {
            cardHolder.CardStaticData.GetCardActions(_diContainer).ForEach(x=>x.Activate(targets));
        }
    }
}