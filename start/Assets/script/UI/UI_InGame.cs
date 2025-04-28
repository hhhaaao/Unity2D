using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UI_InGame : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private Slider slider;
    [SerializeField] private Image dashImage;//添加技能流程1
    [SerializeField] private Image flaskImage;



    private SkillManager skills;

    [Header("Souls Info")]
    [SerializeField] private TextMeshProUGUI currentSouls;
    [SerializeField] private float soulsAmount;
    [SerializeField] private float increaseRate = 1000;
 

    void Start()
    {
        if (!playerStats)
            playerStats.onHealthChanged += UpdateHealthUI;

        skills = SkillManager.instance;

    }

    // Update is called once per frame
    void Update()
    {
        UpdateSoulsUI();
        UpdateHealthUI();
        if (Input.GetKeyDown(KeyCode.L) && skills.dashSkill.dashUnlocked)
            SetCoolDownOf(dashImage);//2
        if (Input.GetKeyDown(KeyCode.O) && Inventory.instance.GetEquipmentByType(EquipmentType.Flask) != null)
            SetCoolDownOf(flaskImage);

        CheckCooldownOf(dashImage, skills.dashSkill.coolDown);//3
        CheckCooldownOf(flaskImage, Inventory.instance.flaskCooldown);
    }

    private void UpdateSoulsUI()
    {
        //随时间恢复存档中的currency
        if (soulsAmount < PlayerManager.instance.GetCurrency())
            soulsAmount += Time.deltaTime * increaseRate;
        else
            soulsAmount = PlayerManager.instance.GetCurrency();

        currentSouls.text = "Souls: "+((int)soulsAmount).ToString( );
        //currentSouls.text =  + PlayerManager.instance.GetCurrency().ToString("#,#");
    }

    private void UpdateHealthUI()
    {
        slider.maxValue = playerStats.GetMaxHP();
        slider.value = playerStats.currentHealth;


        
    }

    private void SetCoolDownOf(Image _image)
    {
        if (_image.fillAmount <= 0)
            _image.fillAmount = 1;

    }

    private void CheckCooldownOf(Image _image,float _cooldown)
    {
        if (_image.fillAmount > 0)
            _image.fillAmount -= 1 / _cooldown * Time.deltaTime;
    }

    
}
