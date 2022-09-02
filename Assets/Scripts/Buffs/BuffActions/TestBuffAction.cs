using UnityEngine;

namespace Entities.Buffs
{
    public class TestBuffAction : IBuffAction
    {
        public void Step()
        {
            Debug.Log("Test buff.");
        }

        public void End()
        {
            Debug.Log("Test buff ended");
        }
    }
}