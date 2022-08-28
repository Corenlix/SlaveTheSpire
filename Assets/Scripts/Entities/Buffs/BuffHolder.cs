using System;
using UnityEngine;

namespace Entities.Buffs
{
    public class BuffHolder : MonoBehaviour
    {
        public event Action<BuffHolder> Ended;
        
        [SerializeField] private BuffView _buffView;
        private Buff _buff;

        public Buff Buff => _buff;

        public void Init(Buff buff, Sprite buffIcon)
        {
            _buffView.Init(buffIcon, buff.StepsRemain);
            _buff = buff;
            _buff.Ended += OnBuffEnd;
        }

        public void AddSteps(int count)
        {
            _buff.StepsRemain += count;
            _buffView.UpdateSteps(_buff.StepsRemain);
        }

        public void Step()
        {
            _buff.Step();
            _buffView.UpdateSteps(_buff.StepsRemain);
        }

        private void OnBuffEnd(Buff buff)
        {
            Ended?.Invoke(this);
        }

        private void OnDestroy()
        {
            _buff.Ended -= OnBuffEnd;
        }
    }
}