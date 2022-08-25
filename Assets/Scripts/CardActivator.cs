using System.Collections.Generic;
using UnityEngine;

public abstract class CardActivator : MonoBehaviour
{
    private Card _card;

    public void Init(Card card)
    {
        _card = card;
    }
    
    protected void Activate(List<Entity> targets)
    {
        _card.Use(targets);
    }
}