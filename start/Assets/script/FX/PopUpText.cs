using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PopUpText : MonoBehaviour
{
    private TextMeshPro myText;


    [SerializeField] private float speed;
    [SerializeField] private float disappearSpeed;
    [SerializeField] private float colorDisappearSpeed;

    [SerializeField] private float lifeTime;

    private float textTimer;


    private void Start()
    {
        myText = GetComponent<TextMeshPro>();
        textTimer = lifeTime;
    }
    private void Update()
    {
        textTimer -= Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, transform.position.y + 1), speed * Time.deltaTime);
        if (textTimer<0)
            {
                float alpha=myText.color.a-colorDisappearSpeed*Time.deltaTime;
                myText.color=new Color(myText.color.r, myText.color.g,myText.color.b, alpha);

            if (myText.color.a > 0)
                speed = disappearSpeed;
            if (myText.color.a <= 0)
                Destroy(gameObject);
            }

       
          
            
    }
}
