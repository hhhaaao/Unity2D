using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour,ISaveManager
{
  public static PlayerManager instance;
    public Player player;
    public int currency;

    private void Awake()
    {
        if (instance != null)
            Destroy(instance);

        else
              instance = this;
        
    
    }

    private void Start()
    {
        //Debug.Log("CURRENCY when start : " + currency);
    }

    private void Update()
    {
        Debug.Log("Actual currency: "+currency);
    }

    public bool HaveEnoughCurrency(int _price)
    {
        if (_price>currency)
        {
            
            return false;
        }
        else
        {
            currency-=_price;
            return true;
        }
    }

    public int GetCurrency()
    {
        return currency;
    }

    public void LoadData(GameData _data)
    {
        this.currency = _data.currency;
    }

    public void SaveData(ref GameData _data)
    {
        _data.currency = this.currency;
    }
}
