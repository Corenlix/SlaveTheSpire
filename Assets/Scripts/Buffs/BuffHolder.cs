using System;
using Infrastructure.StaticData;
using UnityEngine;

namespace Entities.Buffs
{
    public class BuffHolder : MonoBehaviour
    {
        public event Action<BuffHolder> Ended;

        [SerializeField] private BuffView _buffView;
        private IBuffAction _buffAction;
        private BuffId _id;
        private int _steps;

        public BuffId Id => _id;

        public void Init(BuffId id, IBuffAction action, Sprite buffIcon, int steps)
        {
            _buffView.Init(buffIcon, steps);
            _buffAction = action;
            _steps = steps;
            _id = id;
        }

        public void AddSteps(int count)
        {
            _steps += count;
            _buffView.UpdateSteps(_steps);
        }

        public void Step()
        {
            if(_steps <= 0)
                throw new InvalidOperationException();
            
            _buffAction.Activate();
            _steps -= 1;
            _buffView.UpdateSteps(_steps);

            if (_steps < 0)
                Ended?.Invoke(this);
        }
    }
}