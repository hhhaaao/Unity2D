using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneSkillController : MonoBehaviour
{
    public CloneStats st;
    private SpriteRenderer sr;
    private Animator anime;
    private Player player;
    [SerializeField] private float colorLosingSpeed;
    private float cloneTimer;
    [SerializeField] private Transform attackCheck;
    [SerializeField] private float attackCheckRadius=.8f;
    private Transform closestEnemy;
    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        anime = GetComponent<Animator>();
        st = GetComponent<CloneStats>();
        player = PlayerManager.instance.player;
    }
  
    private void Update()
    {
        cloneTimer-=Time.deltaTime;
        if(cloneTimer < 0)
        {
            sr.color = new Color(1, 1, 1, sr.color.a - (Time.deltaTime) * colorLosingSpeed);
            
        }
        if (sr.color.a <= 0)
            Destroy(gameObject);

        
    }
    public void SetClone(Transform _newTransform,float _newDuration,bool canAttack)
    {
        if(canAttack)
        {
            anime.SetInteger("AttackNumber", Random.Range(1, 4));
        }
        transform.position = _newTransform.position;
        cloneTimer = _newDuration;

        FaceDir();
         
    }

    private void CloneAnimationTrigger()
    {
        cloneTimer = -1f;
    }

    private void CloneAsttackTrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(attackCheck.position,attackCheckRadius);
        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Enemy>() != null)
            {
              
                player.stats.DoDamage(hit.GetComponent<CharacterStats>());
                //CharacterStats target =hit.GetComponent<EnemyStats>();
                //hit.GetComponent<Enemy>().DamageEffect();
                //st.DoDamage(target);
            }
        }

    }

    private void FaceDir()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 25);
        float closestDistance=Mathf.Infinity;
        foreach(var a in colliders)
        {
            if (a.GetComponent<Enemy>() != null)
            { 
                float distanceToEnemy = Vector2.Distance(transform.position, a.transform.position);

                if (distanceToEnemy < closestDistance)
                { 
                    closestDistance = distanceToEnemy;
                    closestEnemy = a.transform;
                }
            }
        }

        if(closestEnemy!=null)
        {
            if (transform.position.x > closestEnemy.position.x)
                transform.Rotate(0,180,0);
        }
    }
   
}
