using UnityEngine;

namespace Card.Actions.Data
{
    public abstract class ActionData : ScriptableObject
    {
        public abstract ICardAction GetCardAction();
    }
}