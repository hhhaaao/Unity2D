using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UI_HealthBar : MonoBehaviour
{
    private Entity entity=>GetComponentInParent<Entity>();
    private RectTransform myTransform;
    private Slider slider;
    private CharacterStats myStats=>GetComponentInParent<CharacterStats>();

    private void Start()
    {
        myTransform = GetComponent<RectTransform>();
        //entity= GetComponentInParent<Entity>();
        slider = GetComponentInChildren<Slider>();
        //myStats = GetComponentInParent<CharacterStats>();

       

        UpdateHealthUI();
    }

    private void Update()
    {
        UpdateHealthUI();
    }

    private void UpdateHealthUI()
    {
        slider.maxValue = myStats.GetMaxHP();  
        slider.value = myStats.currentHealth;
        
    }

    private void OnEnable()
    {
        entity.onFlipped += FlipUI;
        myStats.onHealthChanged += UpdateHealthUI;
    }

    private void OnDisable()
    {
        if(entity != null)  
        entity.onFlipped -= FlipUI;
        if(myStats != null)
        myStats.onHealthChanged-=UpdateHealthUI;
    }
    private void FlipUI()=> myTransform.Rotate(0, 180, 0);
}
