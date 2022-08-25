using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
  [SerializeField] private Slider _slider;
  
  public void InitHealth(int value)
  {
    _slider.maxValue = value;
    _slider.value = value;
  }

  public void SetCurrentHealth(int value)
  {
    _slider.value = value;
  }
} 
