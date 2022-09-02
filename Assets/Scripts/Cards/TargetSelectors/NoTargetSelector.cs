using Entities;
using UnityEngine;

namespace Cards.TargetSelectors
{
    public class NoTargetSelector : CardTargetSelector
    {
        [SerializeField] private float _smoothless = 60;
        
        protected override void OnStartSelecting()
        {
        }
        
        protected override void OnSelectingUpdate()
        {
            SelectedCard.transform.position =
                Vector3.Slerp(SelectedCard.transform.position, Input.mousePosition, Time.deltaTime * _smoothless);
            if (Input.GetMouseButtonDown(0))
            {
                SelectTargets(null);
            }
        }

        protected override void OnFinishSelecting()
        {
        }
    }
}