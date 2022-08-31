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
            _buffView.Init(buffIcon, buff);
            _buff = buff;
            _buff.Ended += OnBuffEnd;
        }

        public void Step()
        {
            _buff.Step();
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