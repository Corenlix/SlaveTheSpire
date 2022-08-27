using UnityEngine;

namespace UIElements
{
    public class BarView : ViewObserver
    {
        [SerializeField] private Bar _bar;
        
        protected override void UpdateView(int currentValue, int maxValue)
        {
            _bar.UpdateValue(currentValue, maxValue);
        }
    }
}