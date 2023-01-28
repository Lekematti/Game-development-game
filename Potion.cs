using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Potion : MonoBehaviour
{
    public Slider slider;
	public Gradient gradient;
	public Image fill;


	public void SetMaxHeal(int heal)
	{
		slider.maxValue = heal;
		slider.value = heal;

		fill.color = gradient.Evaluate(1f);
	}

    public void SetHeal(int heal)
	{
		slider.value = heal;

		fill.color = gradient.Evaluate(slider.normalizedValue);
	}
}
