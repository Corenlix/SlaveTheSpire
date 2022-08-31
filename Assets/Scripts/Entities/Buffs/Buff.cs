using System;
using Infrastructure.StaticData.Buffs;

namespace Entities.Buffs
{
    public abstract class Buff
    {
        public event Action<Buff> Ended;
        public event Action<Buff> StepRemainChanged;

        public int StepsRemain
        {
            get => _stepsRemain;
            protected set
            {
                _stepsRemain = value;
                StepRemainChanged?.Invoke(this);
                if (StepsOver)
                    Ended?.Invoke(this);
            }
        }

        public BuffId Id { get; }
        private bool StepsOver => StepsRemain <= 0;
        private int _stepsRemain;

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
        }

        public abstract void Stack(int steps);

        protected abstract void OnStep();
    }
}