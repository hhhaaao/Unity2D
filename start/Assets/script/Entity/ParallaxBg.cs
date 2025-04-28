using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBg : MonoBehaviour
{
    private GameObject cam;
    [SerializeField] private float parallaxEffect;

    private float xPosition;
    private float length;
    void Start()
    {
        cam = GameObject.Find("Main Camera");
        length=GetComponent<SpriteRenderer>().bounds.size.x;
        xPosition = transform.position.x;//当前物体位置
    }


    void Update()
    {
        float disMoved = cam.transform.position.x * (1 - parallaxEffect);
        float moveDis = cam.transform.position.x*parallaxEffect;
       
        transform.position = new Vector3(xPosition + moveDis, transform.position.y);

        if (disMoved >xPosition+length)
        {
             xPosition=xPosition+length;
        }
        else if(disMoved< xPosition-length)
        {
            xPosition=xPosition-length;
        }
    }
}
