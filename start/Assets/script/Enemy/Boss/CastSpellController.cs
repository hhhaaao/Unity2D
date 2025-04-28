using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastSpellController : MonoBehaviour
{
    [SerializeField] private Transform check;
    [SerializeField] private Vector2 boxSize;
    [SerializeField] private LayerMask whatIsPlayer;

    private CharacterStats myStats;

    public void SetupSpell(CharacterStats _stats) => myStats=_stats;
    

  private void AnimTrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(check.position,boxSize,whatIsPlayer);
        foreach(var hit in colliders)
        {
            if(hit.GetComponent<Player>()!=null)
            {
                hit.GetComponent<Entity>().SetupKnockBackDir(transform);
                myStats.DoDamage(hit.GetComponent<CharacterStats>());
                Debug.Log("player damaged");

            }
        }
    }

    private void OnDrawGizmos()=> Gizmos.DrawWireCube(check.position, boxSize);

    public void SelfDestory() => Destroy(gameObject);
    
}
