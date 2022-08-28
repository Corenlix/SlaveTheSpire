using System.Collections.Generic;
using System.Linq;
using Infrastructure.Factories;
using Infrastructure.StaticData;
using UnityEngine;
using Zenject;

namespace Entities.Buffs
{
    public class BuffsHolder : MonoBehaviour
    {
        private readonly List<BuffHolder> _buffs = new();
        private IGameFactory _gameFactory;

        [Inject]
        private void Inject(IGameFactory gameFactory)
        {
            _gameFactory = gameFactory;
        }
        
        public void Add(Buff buff)
        {
            var buffHolder = GetBuffForType(buff.GetBuffId());
            if (buffHolder == null)
            {
                buffHolder = _gameFactory.SpawnBuffHolder(buff, transform);
                _buffs.Add(buffHolder);
                buffHolder.Ended += OnBuffEnd;
            }
            else
            {
                buffHolder.AddSteps(buff.StepsRemain);
            }
        }
        
        public void Step() {
            _buffs.ToList().ForEach(x=>x.Step());
        }

        private void OnBuffEnd(BuffHolder buff)
        {
            _buffs.Remove(buff);
            buff.Ended -= OnBuffEnd;
            Destroy(buff.gameObject);
        }

        private BuffHolder GetBuffForType(BuffId buffId) => _buffs.FirstOrDefault(x => x.Buff.GetBuffId() == buffId);
    }
}