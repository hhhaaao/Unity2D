using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_SkillToolTip : UI_ToolTip
{
    [SerializeField] private TextMeshProUGUI skillText;
    [SerializeField] private TextMeshProUGUI skillName;
    [SerializeField] private float defaultFontSize;
    [SerializeField] private TextMeshProUGUI skillCost;
   
    public void ShowToolTip(string _skillDescription,string _skillName,int _price)
    {
        skillName.text = _skillName;
        skillText.text = _skillDescription;
        skillCost.text = "Cost: "+_price.ToString("#,#");

        AdjustPosition();
        gameObject.SetActive(true);
    }

    public void HideToolTip()
    {
        skillName.fontSize = defaultFontSize;
        gameObject.SetActive(false);
    }
}
