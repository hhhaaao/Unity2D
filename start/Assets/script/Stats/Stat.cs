using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]//显示自定义字段
public class Stat  //往基础值baseValue上加buff/装备/
{
    [SerializeField]private int baseValue;
    public List<int> modifiers;
    public int GetValue()
    {
        int finalValue=baseValue;
        foreach(var md in modifiers)
        {
            finalValue += md;
        }
        return finalValue;
    }
  
    public void SetDefaultValue(int _value)
    {
        baseValue = _value;
    }

    public void AddModifier(int _modifier)
    {
        modifiers.Add(_modifier);  

    }

    public void RemoveModifier(int _modifier)
    {
        modifiers.Remove(_modifier);

    }
}
