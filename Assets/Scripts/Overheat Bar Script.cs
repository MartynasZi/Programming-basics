using UnityEngine;
using UnityEngine.UI;
public class OverheatBarScript : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;
    public void SetMaxHeat(int heatAmount)
    {
        slider.maxValue= heatAmount;
        slider.value = heatAmount;
        fill.color = gradient.Evaluate(1f);
        
    }
    public void SetCurrentHeat(float heatAmount)
    {
        slider.value = heatAmount;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
