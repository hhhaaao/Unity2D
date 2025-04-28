using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI : MonoBehaviour
{
    [Header("End Screen")]
    [SerializeField] public UI_FadeScreen fadeScreen;
    [SerializeField] private GameObject endText;
    [SerializeField] private GameObject restartButton;
    [Space]

    [SerializeField] private GameObject characterUI;
    [SerializeField] private GameObject skillTreeUI;
    [SerializeField] private GameObject craftUI;
    [SerializeField] private GameObject optionUI;
    [SerializeField] private GameObject inGameUI;

    public UI_ItemToolTip itemToolTip;
    public UI_StatToolTip statToolTip;
    public UI_SkillToolTip skillToolTip;
    public UI_CraftWindow craftWindow;

    private void Awake()
    {
        SwitchTo(skillTreeUI);//assign events on skillTreeSlot before we assign events on skill script
    }

    void Start()
    {
      itemToolTip=GetComponentInChildren <UI_ItemToolTip>(true);
        statToolTip = GetComponentInChildren<UI_StatToolTip>(true);

        SwitchTo(inGameUI);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            if (!characterUI.activeSelf && !skillTreeUI.activeSelf && !craftUI.activeSelf && !optionUI.activeSelf)
                SwitchTo(characterUI);
            else
                SwitchTo(inGameUI);
            //SwitchTo(characterUI);
        }
       
    }

    public void SwitchTo(GameObject _menu)
    {
        

        for (int i = 0; i < transform.childCount; i++)
        {
            bool fadeScreen = transform.GetChild(i).GetComponent<UI_FadeScreen>() != null;//keep fadeScreen  gameObject active
            
            if (!fadeScreen)
            transform.GetChild(i).gameObject.SetActive(false);

        }

        if (_menu!=null)
        {
            if (_menu.activeSelf)
            {
            _menu.SetActive(false);
            //CheckForInGameUI();
            return;

            }

            _menu.SetActive(true);

        }
       
        if(GameManager.instance!=null&&_menu!=null)
        {
            if (_menu == inGameUI)
                GameManager.instance.PauseGame(false);
            else
                GameManager.instance.PauseGame(true);
        }
       
        
    }

    public void SwitchWithKey(GameObject _menu)
    {
        if(_menu!=null&&_menu.activeSelf)
        {
            _menu.SetActive(false);
            return;
        }
        SwitchTo(_menu);


    }

    private void CheckForInGameUI()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).gameObject.activeSelf != false)
            {
                return;
            }

            else
                SwitchTo(inGameUI);

        }
    }

    public void SwitchOnEndScreen()
    {
        Debug.Log("1");
        SwitchTo(null);
        Debug.Log("2");
        fadeScreen.FadeOut();
        Debug.Log("3");
        StartCoroutine(EndScreenCorutine());
        Debug.Log("4");

    }

    IEnumerator EndScreenCorutine()
    {
        yield return new WaitForSeconds(1);
        endText.SetActive(true);
        yield return new WaitForSeconds(1);
        restartButton.SetActive(true);
    }

    public void RestartGameButton()=>GameManager.instance.RestartScene();

    public void BackToMenuButton() => GameManager.instance.BackToMenu();


}
