using System;
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
        [SerializeField] private Entity _owner;
        private readonly List<Buff> _buffs = new();
        private IGameFactory _gameFactory;
        private IStaticDataService _staticDataService;

        [Inject]
        private void Inject(IGameFactory gameFactory, IStaticDataService staticDataService)
        {
            _staticDataService = staticDataService;
            _gameFactory = gameFactory;
        }

        public void Add(BuffId id, int steps)
        {
            BuffStaticData buffData = _staticDataService.ForBuff(id);
            switch (buffData.BuffStackStrategy)
            {
                case BuffStackStrategy.Multiple:
                    StackByMultipleStrategy(id, steps);
                    break;
                case BuffStackStrategy.Steps:
                    StackByStepsStrategy(id, steps);
                    break;
                case BuffStackStrategy.None:
                    StackByNoneStrategy(id, steps);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void Step() {
            _buffs.ToList().ForEach(x=>x.Step());
        }

        private void StackByMultipleStrategy(BuffId id, int steps)
        {
            SpawnNewBuff(id, steps);
        }

        private void StackByStepsStrategy(BuffId id, int steps)
        {
            Buff existBuff = GetExistBuff(id);
            if (existBuff == null)
                SpawnNewBuff(id, steps);
            else existBuff.SetStepsRemain(steps + existBuff.StepsRemain);
        }

        private void StackByNoneStrategy(BuffId id, int steps)
        {
            Buff existBuff = GetExistBuff(id);
            if (existBuff == null)
                SpawnNewBuff(id, steps);
            else existBuff.SetStepsRemain(Math.Max(existBuff.StepsRemain, steps));
        }

        private void SpawnNewBuff(BuffId id, int steps)
        {
            Buff buff = _gameFactory.SpawnBuff(id, steps, transform, _owner);
            _buffs.Add(buff);
            buff.Ended += OnBuffEnd;
        }

        private void OnBuffEnd(Buff buff)
        {
            _buffs.Remove(buff);
            buff.Ended -= OnBuffEnd;
            Destroy(buff.gameObject);
        }

        private Buff GetExistBuff(BuffId buffId) => _buffs.FirstOrDefault(x => x.Id == buffId);
    }
}