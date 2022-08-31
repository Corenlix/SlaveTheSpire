using UnityEngine;

namespace Entities.Buffs
{
    public class TestBuffAction : IBuffAction
    {
        public void Step()
        {
            Debug.Log("Test buff.");
        }
    }
}