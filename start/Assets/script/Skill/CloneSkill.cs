using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloneSkill : Skill
{

    [Header("Clone Skill")]
    public bool isUnlocked;
    [SerializeField] private UI_SkillTreeSlot cloneUnlockButton;

    

    [Header("Clone Info")]
    [SerializeField] private GameObject clonePrefab;
    [SerializeField] private float cloneDuration;
    [SerializeField] private bool canAttack;

    public void CreateClone(Transform trans)
    {
        GameObject newClone=Instantiate(clonePrefab);
        newClone.GetComponent<CloneSkillController>().SetClone(trans,cloneDuration,canAttack);
    }

    protected override void Start()
    {
        base.Start();
        cloneUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockClone);
    }

    private void UnlockClone()
    {
        if (cloneUnlockButton.unlocked == true)
            isUnlocked = true;
    }

    protected override void CheckUnlock()
    {
        UnlockClone();

    }


    //public void CreateCloneOnDash()

}
