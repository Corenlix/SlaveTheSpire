using System;
using Infrastructure.StaticData;
using Infrastructure.StaticData.Buffs;
using Zenject;

namespace Entities.Buffs
{
    public abstract class Buff
    {
        public event Action<Buff> Ended;
        
        public int StepsRemain { get; set; }
        public BuffId Id { get; private set; }
        private bool StepsOver => StepsRemain <= 0;

        public Buff(BuffId buffId, int steps)
        {
            Id = buffId;
            StepsRemain = steps;
        }

        public void Step()
        {
            if (StepsOver)
                throw new InvalidOperationException();

            OnStep();
            StepsRemain -= 1;
            if (StepsOver)
                Ended?.Invoke(this);
        }

        protected abstract void OnStep();
    }
}