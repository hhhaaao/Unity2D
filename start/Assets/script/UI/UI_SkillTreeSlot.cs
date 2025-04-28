using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class UI_SkillTreeSlot : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler,ISaveManager
{
    private UI ui;

    [SerializeField] private int skillPrice;
    [SerializeField] private string skillName;
    [TextArea]
    [SerializeField] private string skillDescription;

    public bool unlocked;

    [SerializeField] private UI_SkillTreeSlot[] shouldBeUnlocked;//前置需解锁
    [SerializeField] private UI_SkillTreeSlot[] shouldBeLocked;//不能同时解锁

    [SerializeField] private Color lockedSkillColor;


    [SerializeField] private Image skillImage;

    private void OnValidate()
    {
        gameObject.name = "SkillTreeSlot_UI - " + skillName;
    }

    private void Start()
    {
        skillImage = GetComponent<Image>();

        skillImage.color = lockedSkillColor;

        
       
        ui = GetComponentInParent<UI>();
        
        if(unlocked)
            skillImage.color=Color.white;

    }
    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(() => UnlockSkillSlot());
    }
    public void UnlockSkillSlot()
    {
        Debug.Log("click test "+PlayerManager.instance.currency);
        if(unlocked)
            return;

        for(int i=0;i<shouldBeUnlocked.Length;i++)
        {
            if (!shouldBeUnlocked[i].unlocked )
            {
                Debug.Log("Can't unlock skill");
                return; 
            }
        }
        
        for(int i=0;i<shouldBeLocked.Length;i++)
        {
            if(shouldBeLocked[i].unlocked)
            {
                Debug.Log("Can't unlock skill");
                return;
            }
        }

        if (PlayerManager.instance.HaveEnoughCurrency(skillPrice)==false)
        {

            Debug.Log("no enough currency");
            Debug.Log("Price:" + skillPrice);
            
            return;
        }
        unlocked = true;
        skillImage.color=Color.white;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ui.skillToolTip.ShowToolTip(skillDescription,skillName,skillPrice);

      
        
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ui.skillToolTip.HideToolTip();
    }

    public void LoadData(GameData _data)
    {
        //throw new System.NotImplementedException();
        if(_data.skillTree.TryGetValue(skillName,out bool value))
        {
            unlocked = value;
        }

    }

    public void SaveData(ref GameData _data)
    {
        if(_data.skillTree.TryGetValue(skillName,out bool value))
        {
            _data.skillTree.Remove(skillName);
            _data.skillTree.Add(skillName, unlocked); 
        }
        else
            _data.skillTree.Add(skillName,unlocked);
    }
}
