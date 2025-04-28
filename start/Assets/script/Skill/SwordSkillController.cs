using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSkillController : MonoBehaviour
{
    private Animator anime;
    private Rigidbody2D rb;
    private CircleCollider2D cd;
    private Player player;
    private bool canRotate;
    private bool isReturning;

    [Header("Pierce Info")]
    private int pierceNum;

    [Header("bounce info")]
    private bool isBouncing;
    private int bounceNum=4;
    public List<Transform> enemyTarget;
    private int targetIndex;
    [SerializeField] private float bouncingSpeed;
    [SerializeField]private float returnSpeed=10f;

    private void Awake()
    {
        player = PlayerManager.instance.player;//improving sword 12.03
        canRotate = true;
        anime = GetComponentInChildren<Animator>();
        rb = GetComponentInChildren<Rigidbody2D>();
        cd = GetComponentInChildren<CircleCollider2D>();
    }

    public void SetUpSword(Vector2 dir,float gravity)
    {

        rb.velocity = dir;
        rb.gravityScale = gravity;
        if (pierceNum == 0)
            anime.SetBool("Rotate", true);
    }

   

    public void ReturnSword()
    {
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        //rb.isKinematic = false;
        transform.parent = null;
        isReturning = true;
        

    }

    private void Update()
    {
        if (canRotate)
            transform.right = rb.velocity;

        if (isReturning)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, returnSpeed * Time.deltaTime);
            if (Vector2.Distance(transform.position, player.transform.position) < 2)
                player.CatchSword();
        }

        BounceLogic();

    }

    private void BounceLogic()
    {
        if (isBouncing && enemyTarget.Count > 0)
        {
            //Debug.Log("bouncing");
            transform.position = Vector2.MoveTowards(transform.position, enemyTarget[targetIndex].position, bouncingSpeed * Time.deltaTime);
            if (Vector2.Distance(transform.position, enemyTarget[targetIndex].position) < 0.1)
            {
                targetIndex++;
                bounceNum--;
                if (bounceNum <= 0)
                {
                    isBouncing = false;
                    isReturning = true;
                }

                if (targetIndex >= enemyTarget.Count)
                    targetIndex = 0;
            }

        }
    }

    //Åö×²
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        //collision.GetComponent<Enemy>()?.DamageEffect();

        if (collision.GetComponent<Enemy>() != null)
        {
            if (isBouncing && enemyTarget.Count <= 0)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 10);

                foreach (var hit in colliders)
                {
                    if (hit.GetComponent<Enemy>() != null)
                    {
                        Enemy enemy = hit.GetComponent<Enemy>();
                        SwordSkillDamage(enemy);
                        
                        enemyTarget.Add(hit.transform);
                    }
                }
            }
            else if(!isBouncing&& enemyTarget.Count <= 0)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 10);

                foreach (var hit in colliders)
                {
                    if (hit.GetComponent<Enemy>() != null)
                    {
                        Enemy enemy = hit.GetComponent<Enemy>();
                        SwordSkillDamage(enemy);

                        enemyTarget.Add(hit.transform);
                    }
                }
            }
        }

        StuckInto(collision);

    }

    private void SwordSkillDamage(Enemy _enemy)
    {
        player.stats.DoDamage(_enemy.GetComponent<CharacterStats>());
        //_enemy.DamageEffect();

        //¼ì²âÎäÆ÷À¸µÄbuff
        //ItemData_Equipment weaponData = Inventory.instance.GetEquipmentType(EquipmentType.Weapon);
        //if (weaponData != null)
        //    weaponData.ExecuteItemEffect(_enemy.transform);

        //¼ì²âÊÎÆ·À¸buff
        ItemData_Equipment amuletData = Inventory.instance.GetEquipmentByType(EquipmentType.Amulet);
        if (amuletData != null)
            amuletData.ExecuteItemEffect(_enemy.transform);


        //Inventory.instance.GetEquipmentType(EquipmentType.Weapon).ExecuteItemEffect(_enemy.GetComponent<EnemyStats>().transform);

    }

    private void StuckInto(Collider2D collision)
    {
        if (pierceNum > 0 && collision.GetComponent<Enemy>() != null)
        {
            pierceNum--;
            return;
        }
        canRotate = false;
        cd.enabled = false;
        rb.isKinematic = true;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        GetComponentInChildren<ParticleSystem>().Play();


        if (isBouncing&&enemyTarget.Count>0)
            return;//isBouncingÓÐÐ§Ê±²»Í£Ö¹¶¯»­
        anime.SetBool("Rotate", false);
        transform.parent = collision.transform;
    }

    public void SetupBounce(bool _isBouncing,int amountOfBounce)
    {
        isBouncing = _isBouncing;
        bounceNum = amountOfBounce;
    }

    public void SetUpPierce(int amountOfPierce)
    {
        pierceNum = amountOfPierce;
    }
}
