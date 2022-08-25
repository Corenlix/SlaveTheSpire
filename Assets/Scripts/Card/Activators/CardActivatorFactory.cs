using System.ComponentModel;
using UnityEngine;

namespace Card.Activators
{
    public static class CardActivatorFactory
    {
        public static CardActivator InstantiateActivator(GameObject parent, CardActivatorType activatorType)
        {
            return activatorType switch
            {
                CardActivatorType.OnClick => parent.AddComponent<OnClick>(),
                _ => throw new InvalidEnumArgumentException(activatorType.ToString())
            };
        }
    }
}