using UnityEngine;
using UnityEngine.UI;

namespace UIElements
{
    public class BarValueView : ViewObserver
    {
        [SerializeField] private Slider _bar;
        
        protected override void UpdateView(int currentValue, int maxValue)
        {
            _bar.maxValue = maxValue;
            _bar.value = currentValue;
        }
    }
}