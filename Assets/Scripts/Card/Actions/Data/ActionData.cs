using UnityEngine;
using Zenject;

namespace Card.Actions.Data
{
    public abstract class ActionData : ScriptableObject
    {
        public abstract ICardAction GetCardAction(DiContainer diContainer);
    }
}