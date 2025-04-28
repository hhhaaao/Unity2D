using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]//��ʾ�Զ����ֶ�
public class Stat  //������ֵbaseValue�ϼ�buff/װ��/
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
