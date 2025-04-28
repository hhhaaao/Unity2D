using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UI_ToolTip : MonoBehaviour
{
    [SerializeField] private float xLimit=960;
    [SerializeField] private float yLimit=540;

    //[SerializeField] private float xOffset = 150;
    //[SerializeField] private float yOffset = 150;
    public virtual void AdjustPosition()
    {
        Vector2 mousePosition = Input.mousePosition;

        float new_xOffset = 0;
        float new_yOffset = 0;

        new_xOffset = mousePosition.x > xLimit ? -150 : 250;
        new_yOffset = mousePosition.y > yLimit ? -150 : 250;
        transform.position = new Vector2(mousePosition.x + new_xOffset, mousePosition.y + new_yOffset);
    }

    public void AdjustFontSize(TextMeshProUGUI _text)
    {
        if (_text.text.Length > 12)
            _text.fontSize *= 0.7f;
    }
}
