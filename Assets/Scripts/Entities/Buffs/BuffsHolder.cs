using System.Collections.Generic;
using System.Linq;
using Infrastructure.Factories;
using Infrastructure.StaticData;
using Infrastructure.StaticData.Buffs;
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
        
        public void Add(BuffId id, int steps)
        {
            var buffHolder = GetExistBuff(id);
            if (buffHolder == null)
            {
                buffHolder = _gameFactory.SpawnBuffHolder(id, steps, transform);
                _buffs.Add(buffHolder);
                buffHolder.Ended += OnBuffEnd;
            }
            else
            {
                buffHolder.AddSteps(steps);
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

        private BuffHolder GetExistBuff(BuffId buffId) => _buffs.FirstOrDefault(x => x.Buff.Id == buffId);
    }
}