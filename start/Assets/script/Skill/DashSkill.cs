using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DashSkill : Skill
{
    [Header("Dash")]
    public bool dashUnlocked;
    [SerializeField] private UI_SkillTreeSlot dashUnlockButton;

    [Header("Clone on dash")]
    public bool cloneOnDashUnlocked;
    [SerializeField] private UI_SkillTreeSlot cloneOnDashUnlockButton;

    [Header("Clone on arrival")]
    public bool cloneOnArrivalUnlocked;
    [SerializeField] private UI_SkillTreeSlot cloneOnArrivalUnlockButton;
    
    protected override void UseSkill()
    {
        base.UseSkill();

        //Debug.Log("Left Clone Behind");
    }

    protected override void Start()
    {
        base.Start();
        dashUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockDash);
        cloneOnDashUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockCloneOnDash);
        cloneOnArrivalUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockCloneOnArrival);
        

    }

    private void UnlockDash()
    {
        //Debug.Log("dash unlock check: " + dashUnlockButton.unlocked);
        if(dashUnlockButton.unlocked)
        {
         
         dashUnlocked= true;

        }
    }
    private void UnlockCloneOnDash()
    {
        if(cloneOnDashUnlockButton.unlocked)
        cloneOnDashUnlocked= true;
    }

    private void UnlockCloneOnArrival()
    {
        if(cloneOnArrivalUnlockButton.unlocked) 
        cloneOnArrivalUnlocked= true;
    }

    protected override void CheckUnlock()
    {
        Debug.Log("skill save check");
        UnlockDash();
        UnlockCloneOnDash();
        UnlockCloneOnArrival();
    }
}
