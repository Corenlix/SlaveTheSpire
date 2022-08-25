using UnityEngine;

public class CardActionDestroyEntity : ICardAction
{
    public void Activate(Entity target)
    {
        Object.Destroy(target.gameObject);
    }
}