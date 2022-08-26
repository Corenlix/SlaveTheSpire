using DG.Tweening;
using UnityEngine;

namespace Card.TargetSelectors
{
    public class Defense : CardTargetSelector
    {
        protected override void OnStartSelecting()
        {
        }
        
        protected override void OnSelectingUpdate()
        {
            SelectedCard.transform.DOMove(Input.mousePosition, Time.deltaTime * 5f);
            if (Input.GetMouseButtonDown(0))
            {
                var hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
                if (hit.collider != null && hit.transform.TryGetComponent<DefenseTrigger>(out var trigger))
                {
                    SelectTargets(null);
                }
            }
        }

        protected override void OnFinishSelecting()
        {
        }
    }
}