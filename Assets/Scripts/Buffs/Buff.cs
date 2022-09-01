using System;
using Infrastructure.StaticData.Buffs;
using UnityEngine;

namespace Entities.Buffs
{
    public class Buff : MonoBehaviour
    {
        public event Action<Buff> Ended;
        public event Action<Buff> StepsRemainChanged;
        public event Action<Buff> Inited;

        public int StepsRemain { get; private set; }
        public BuffId Id { get; private set; }
        public Sprite Icon { get; private set; }
        
        private IBuffAction _buffAction;

        public void Init(BuffStaticData buffStaticData, IBuffAction buffAction, int steps)
        {
            Id = buffStaticData.Id;
            Icon = buffStaticData.Icon;
            StepsRemain = steps;
            _buffAction = buffAction;
            Inited?.Invoke(this);
            StepsRemainChanged?.Invoke(this);

            if (steps <= 0)
                throw new ArgumentOutOfRangeException();
        }

        public void Step()
        {
            if (StepsRemain == 0)
                throw new InvalidOperationException();

            _buffAction.Step();
            StepsRemain -= 1;
            
            StepsRemainChanged?.Invoke(this);
            if (StepsRemain == 0)
                End();
        }

        private void End()
        {
            Ended?.Invoke(this);
            _buffAction.End();
        }

        public void SetStepsRemain(int value)
        {
            if (StepsRemain < 0)
                throw new ArgumentOutOfRangeException();
            
            StepsRemain = value;
            
            StepsRemainChanged?.Invoke(this);
            if (StepsRemain == 0)
                End();
        }
    }
}