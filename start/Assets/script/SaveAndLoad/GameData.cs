using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData 
{

    public int currency;
    
    //regain money
    public float lostCurrencyX;
    public float lostCurrencyY;
    public int lostCurrency;

    //equipment and skill
    public List<string> equipmentID;
    public SerializableDictionary<string, bool> skillTree;
    public SerializableDictionary<string, int> inventory;

    //check point
    public SerializableDictionary<string, bool> checkPoints;
    public string closestCheckPointID;

    public GameData()
    {
        this.lostCurrencyX = 0;
        this.lostCurrencyY = 0;
        this.lostCurrency = 0;

        this.currency = 0;
        inventory = new SerializableDictionary<string, int>();
        equipmentID=new List<string>();
        skillTree=new SerializableDictionary<string, bool>{};
        closestCheckPointID = string.Empty;
        checkPoints=new SerializableDictionary<string, bool>();
    }

}
