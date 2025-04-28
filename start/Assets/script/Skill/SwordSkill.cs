using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum SwordType
{
    Regular,
    Bounce,
    Pierce,
    Spin
    

}
public class SwordSkill : Skill
{
    public SwordType swordType=SwordType.Regular;

    [Header("Sword Unlock")]
    [SerializeField] private bool isSwordUnlock;
    [SerializeField] private bool swordUnlockButton;
    [SerializeField] private bool isPierceUnlock;
    [SerializeField] private bool isBounceUnlock;

    [Header("Bounce Sword Info")]
    [SerializeField] private UI_SkillTreeSlot bounceUnlockButton;
    [SerializeField] private int bounceAmount;
    [SerializeField] private float bounceGravity;

    [Header("Pierce Sword Info")]
    [SerializeField] private UI_SkillTreeSlot PierceUnlockButton;
    [SerializeField] private int pierceAmount;
    [SerializeField] private float pierceGravity;

    [Header("Skill Info")]
    [SerializeField] private GameObject swordPrefab;
    [SerializeField] Vector2 launchForce;
    [SerializeField] float gravityScale;

    [Header("Aim Dots")]
    [SerializeField] private int dotNum;
    [SerializeField] private float spaceBetweenDots;
    [SerializeField] private GameObject dotPrefab;
    [SerializeField] private Transform dotsParent;

    private GameObject[] dots;


    public Vector2 finalDir;

    protected override void Start()
    {
        base.Start();
        GenerateDots();



    }
    protected override void Update()
    {
        if (Input.GetKeyUp(KeyCode.Mouse1))
            finalDir = new Vector2(AimDir().normalized.x * launchForce.x, AimDir().normalized.y * launchForce.y);

        if(Input.GetKey(KeyCode.Mouse1))
        {
            for(int i = 0; i < dots.Length; i++)
            {
                dots[i].transform.position = DotsPosition(i * spaceBetweenDots);
            }
        }

        SetGravity();
    }
    public void CreateSword()
    {
        GameObject newSword=Instantiate(swordPrefab,player.transform.position,transform.rotation);
        SwordSkillController newSwordScript=newSword.GetComponent<SwordSkillController>();
         

        if(swordType==SwordType.Bounce)
        {
            newSwordScript.SetupBounce(true, bounceAmount);
        }
        else if(swordType==SwordType.Pierce)
        {
            newSwordScript.SetUpPierce(pierceAmount);
        }

        newSwordScript.SetUpSword(/*new Vector2(player.facingDir*10,0)*/finalDir, gravityScale);

        player.AssignNewSword(newSword);//player gets a sword

        DotsActive(false);
    }

    private void SetGravity()
    {
       switch(swordType)
        {
            case SwordType.Bounce:
                gravityScale = bounceGravity;break;
            case SwordType.Pierce:
                gravityScale = pierceGravity;break;
         
        }
    }

    #region Aiming
    public Vector2 AimDir()
    {
        Vector2 playerPositon = player.transform.position;
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction=mousePosition- playerPositon;

        return direction;
    }

    public void DotsActive(bool isActive)
    {
        for(int i = 0; i < dots.Length; i++)
        {
            dots[i].SetActive(isActive);
        }
    }
    private void GenerateDots()
    {
        dots =new GameObject[dotNum];
        for(int i=0;i<dotNum;i++)
        {
            dots[i] = Instantiate(dotPrefab, player.transform.position, Quaternion.identity, dotsParent);
            dots[i].SetActive(true);
        }
    }

    private Vector2 DotsPosition(float t)
    {
        Vector2 position = (Vector2)player.transform.position + new Vector2(
            AimDir().normalized.x *  launchForce.y,
            AimDir().normalized.y * launchForce.y) * t + .5f * (Physics2D.gravity * gravityScale) * t * t;//d=v0t+1/2at^2
        return position;
    }
    #endregion



    #region UnlockRegion




    #endregion

}
