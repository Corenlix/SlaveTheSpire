using UnityEngine;

namespace Entities.Buffs
{
    public class TestBuffAction : IBuffAction
    {
        public void Activate()
        {
            Debug.Log($"Test buff.");
        }
    }
}