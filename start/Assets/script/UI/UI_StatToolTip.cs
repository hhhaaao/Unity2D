using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UI_StatToolTip : UI_ToolTip
{

    [SerializeField] private TextMeshProUGUI description;
    
    public void ShowStatToolTip(string _text)
    {
        description.text = _text;
        gameObject.SetActive(true);
        AdjustPosition();
        
    }

    public void HideStatToolTip()
    {
        gameObject.SetActive(false);
         
    }
}
