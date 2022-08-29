using System.Collections.Generic;
using Entities;
using Entities.Buffs;
using Infrastructure.StaticData;
using Zenject;

namespace Card.Actions
{
    public class TestBuffAction : ICardAction
    {
        private readonly BuffId _buffId;
        private readonly int _steps;

        public TestBuffAction(BuffId buffId, int steps)
        {
            _buffId = buffId;
            _steps = steps;
        }
        
        public void Activate(List<Entity> targets)
        {
            targets.ForEach(x=>x.BuffsHolder.Add(_buffId, _steps));
        }
    }
}