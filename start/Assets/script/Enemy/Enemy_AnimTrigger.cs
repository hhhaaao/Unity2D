using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_AnimTrigger : MonoBehaviour
{

    private Enemy enemy => GetComponentInParent<Enemy>();

    private void AnimationTrigger()
    {
        enemy.AnimeFinishTrigger();
    }

   

    private void OpenCounter() => enemy.OpenCounterWindow();

    private void CloseCounter() => enemy.CloseCounterWindow();
}
