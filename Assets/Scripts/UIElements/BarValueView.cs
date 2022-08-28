using UnityEngine;
using UnityEngine.UI;

namespace UIElements
{
    public class BarValueView : ViewObserver
    {
        [SerializeField] private Image _fill;
        
        protected override void UpdateView(int currentValue, int maxValue)
        {
            _fill.fillAmount = currentValue/maxValue;
        }
    }
}