using System;
using Infrastructure.StaticData;
using Zenject;

namespace Entities.Buffs
{
    public abstract class Buff
    {
        public event Action<Buff> Ended; 

        public int StepsRemain { get; set; }

        public abstract BuffId GetBuffId();
        private bool StepsOver => StepsRemain <= 0;
        protected DiContainer DiContainer;


        public Buff(DiContainer diContainer, int steps)
        {
            StepsRemain = steps;
            DiContainer = diContainer;
        }

        public void Step()
        {
            if (StepsOver)
                throw new InvalidOperationException();
            
            OnStep();
            StepsRemain -= 1;
            if(StepsOver)
                Ended?.Invoke(this);
        }

        protected abstract void OnStep();
    }
}