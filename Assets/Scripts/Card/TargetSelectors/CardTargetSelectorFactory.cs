using System.ComponentModel;
using UnityEngine;

namespace Card.TargetSelectors
{
    public static class CardTargetSelectorFactory
    {
        public static CardTargetSelector InstantiateActivator(GameObject parent, CardTargetSelectorType targetSelectorType)
        {
            return targetSelectorType switch
            {
                CardTargetSelectorType.Attack => parent.AddComponent<Attack>(),
                CardTargetSelectorType.Defense => parent.AddComponent<Defense>(),
                _ => throw new InvalidEnumArgumentException(targetSelectorType.ToString())
            };
        }
    }
}