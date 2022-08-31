using Infrastructure.StaticData.Buffs;
using UnityEngine;

namespace Entities.Buffs
{
    public class TestBuff : Buff
    {
        public TestBuff(BuffId id, int steps) : base(id, steps)
        {
        }

        protected override void OnStep()
        {
            Debug.Log($"Test buff. {StepsRemain} steps remain");
        }

        public override void Stack(int steps)
        {
        }
    }
}