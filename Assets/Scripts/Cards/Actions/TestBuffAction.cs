using System.Collections.Generic;
using Entities;
using Infrastructure.StaticData.Buffs;

namespace Cards.Actions
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
            targets.ForEach(x=>x.AddBuff(_buffId, _steps));
        }
    }
}