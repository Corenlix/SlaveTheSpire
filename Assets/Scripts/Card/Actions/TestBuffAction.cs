using System.Collections.Generic;
using Entities;
using Entities.Buffs;
using Zenject;

namespace Card.Actions
{
    public class TestBuffAction : ICardAction
    {
        private readonly DiContainer _diContainer;

        public TestBuffAction(DiContainer diContainer)
        {
            _diContainer = diContainer;
        }
        
        public void Activate(List<Entity> targets)
        {
            targets.ForEach(x=>x.BuffsHolder.Add(new TestBuff(_diContainer, 3)));
        }
    }
}