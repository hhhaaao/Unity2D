using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
   private  Animator anim;
    public  string checkPonitID;
    public bool isActive;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        
    }

    [ContextMenu("Generate checkpoint id")]
    private void GenerateID()
    {
        checkPonitID = System.Guid.NewGuid().ToString();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Player>()!=null)
        {
            ActivateCheckPoint();
        }

        
    }

    public void ActivateCheckPoint()
    {
        if (anim == null)
            Debug.Log("anim null");
        isActive = true;
        anim.SetBool("Active", true);
    }
}
