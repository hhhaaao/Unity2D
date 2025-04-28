using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderController : MonoBehaviour
{
    [SerializeField] private CharacterStats targetStats;
    [SerializeField] private float speed;
    private int damage;
    private Animator anime;
    private bool triggered;

    private void Start()
    {
        anime = GetComponentInChildren<Animator>();
    }

    public void SetUp(int _damage,CharacterStats _targetStats)
    {
        damage = _damage;
        targetStats = _targetStats;
    }

    private void Update()
    {
        if (!targetStats)
            return;
        
        if(triggered)
            return;
        

       
        transform.position = Vector2.MoveTowards(transform.position, targetStats.transform.position, speed * Time.deltaTime);
        transform.right = targetStats.transform.position- transform.position ;

        if (Vector2.Distance(transform.position,targetStats.transform.position)<.1f)
        {   //on hit vertical
            anime.transform.localRotation= Quaternion.identity;
            transform.localRotation=Quaternion.identity;
            //increase scale
            transform.localScale = new Vector3(3, 3);
            //location detail
            anime.transform.localPosition = new Vector3(0, .4f);


            triggered = true;
            anime.SetTrigger("Hit");
            Invoke("DamageAndSelfDestory", .2f);
        }
    }

    private void DamageAndSelfDestory()
    {
        targetStats.ApplyShock(true);
        targetStats.TakeDamage(damage);
        Destroy(gameObject, .4f);
    }
}
