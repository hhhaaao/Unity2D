using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneStats : CharacterStats
{
    protected override void Start()
    {
        base.Start();
        //Debug.Log("CLONEDAMAGE IS " + damage.GetValue());
    }

}
