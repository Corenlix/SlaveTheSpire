using TMPro;
using UnityEngine;

namespace UIElements
{
    public class ValueView : ViewObserver
    {
        [SerializeField] private TextMeshProUGUI _text;
   
        protected override void UpdateView(int currentValue, int maxValue)
        {
            _text.text = $"{currentValue}/{maxValue}";
        }
    }
}
