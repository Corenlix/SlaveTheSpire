using System.ComponentModel;
using UnityEngine;

namespace Card.Activators
{
    public static class CardActivatorFactory
    {
        public static CardTargetSelector InstantiateActivator(GameObject parent, CardActivatorType activatorType)
        {
            return activatorType switch
            {
                CardActivatorType.Attack => parent.AddComponent<Attack>(),
                _ => throw new InvalidEnumArgumentException(activatorType.ToString())
            };
        }
    }
}