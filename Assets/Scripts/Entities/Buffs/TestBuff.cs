using Infrastructure.StaticData;
using UnityEngine;
using Zenject;

namespace Entities.Buffs
{
    public class TestBuff : Buff
    {
        public TestBuff(DiContainer diContainer, int steps) : base(diContainer, steps)
        {
        }

        public override BuffId GetBuffId() => BuffId.Test;

        protected override void OnStep()
        {
            Debug.Log($"Test buff. {StepsRemain} steps remain");
        }
    }
}