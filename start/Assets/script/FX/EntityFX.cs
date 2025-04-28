using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using TMPro;
public class EntityFX : MonoBehaviour
{

    

    protected Player player;
    protected SpriteRenderer sr;

    [Header("PopUpText")]
    [SerializeField] private GameObject popUpTextPrefab;

    [Header("After Image FX")]
    [SerializeField] private float afterImageCooldown;
    [SerializeField] private GameObject afterImagePrefab;
    [SerializeField] private float colorLosingRate;
    private float afterImageCooldownTimer;

    [Header("Screen shake FX")]
    [SerializeField] private float shakeMultiper;
    private CinemachineImpulseSource screenShake;
    public Vector3 shakeSwordImpact;
    public Vector3 shakeHighDamage;
    public Vector3 bossTriggeredImpact;






    [Header("Flash FX")]
    [SerializeField] private Material hitMat;
    private float flashDuration=.2f;
     private Material originalMat;

    [Header("Aliment Color")]
    [SerializeField]private Color chillColor;
    [SerializeField] private Color[] igniteColor;
    [SerializeField] private Color[] shockColor;

    [Header("Aliment FX")]
    [SerializeField] private ParticleSystem igniteFX;
    [SerializeField] private ParticleSystem chillFX;
    [SerializeField] private ParticleSystem shockFX;

    [Space]
    [SerializeField] private ParticleSystem dustFX;

    //bossË²ÒÆ
    private GameObject myHealthBar;

    protected virtual void Start()
    {
        sr=GetComponentInChildren<SpriteRenderer>();
        originalMat=sr.material;
        screenShake = GetComponent<CinemachineImpulseSource>();
        player = PlayerManager.instance.player;
        //Debug.Log("igniteColor length: " + igniteColor.Length); 

        myHealthBar = GetComponentInChildren<UI_HealthBar>().gameObject;
      
    }

    

    private IEnumerator FlashFX()
    {
        sr.material = hitMat;
        Color currentColor = sr.color;
        sr.color = Color.white;
        yield return new WaitForSeconds(flashDuration);

        sr.color = currentColor;
        sr.material = originalMat;
    }

    private void RedColorBlink()
    {
        if (sr.color != Color.white)
            sr.color = Color.white;
        else
            sr.color = Color.red; 

    }

    private void CancelColorChange()
    {
        CancelInvoke();//Çå³ýËùÓÐinvoke
        sr.color = Color.white;
        igniteFX.Stop();
        chillFX.Stop();
        shockFX.Stop();
    }

    public IEnumerator ChillFor(float _seconds)
    {
        chillFX.Play();

        yield return new WaitForSeconds(flashDuration);
        //Debug.Log("chillFX called");
        ChillColorFX();
        Invoke("CancelColorChange", _seconds);
    }
        public IEnumerator IgniteFor(float _seconds)
    {

        igniteFX.Play();
        yield return new WaitForSeconds(flashDuration);
        //Debug.Log("igniteFX called");
        InvokeRepeating("IgniteColorFX",0,.3f);
        Invoke("CancelColorChange",_seconds);
    }
    public IEnumerator ShockFor(float _seconds)
    {
        shockFX.Play();
        yield return new WaitForSeconds(flashDuration);
        //Debug.Log("shockFX called");
        InvokeRepeating("ShockColorFX", 0, .3f);
        Invoke("CancelColorChange", _seconds);
    }
    private void IgniteColorFX()//change between two color
    {
        if (sr.color != igniteColor[0])
        {
            sr.color = igniteColor[0];
        }
        else
            sr.color = igniteColor[1];
    }

    private void ChillColorFX() => sr.color = chillColor;

    private void ShockColorFX()
    {
        if (sr.color != shockColor[0])
        {
            sr.color = shockColor[0];
        }
        else
            sr.color = shockColor[1];
    }

    public void PlayDustFX()
    {
        if(dustFX!=null)
            dustFX.Play();


    }

    private void Update()
    {
        afterImageCooldownTimer -= Time.deltaTime;

    }

   
    public void CreateAfterImage()
    {
        if (afterImageCooldownTimer < 0)
        {
            afterImageCooldownTimer = afterImageCooldown;
            GameObject newAfterImage = Instantiate(afterImagePrefab, transform.position, transform.rotation);
            newAfterImage.GetComponent<AfterImage>().SetupAfterImage(colorLosingRate, sr.sprite);

        }

    }

    //screen shake
    public void ScreenShake(Vector3 _shakePower)
    {
        screenShake.m_DefaultVelocity = new Vector3(_shakePower.x * player.facingDir, _shakePower.y) * shakeMultiper;
        screenShake.GenerateImpulse();
    }



    public void CreatePopUpText(string _text)
    {
        float randomX = Random.Range(-1, 1);
        float randomY = Random.Range(1, 3);
        Vector3 positionOffset=new Vector3(randomX,randomY,0);
 
        GameObject newText = Instantiate(popUpTextPrefab, transform.position+positionOffset, Quaternion.identity);
        newText.GetComponent<TextMeshPro>().text = _text;

    }

    public void MakeTransparent(bool _transparent)
    {
        if (_transparent)
        {
            myHealthBar.SetActive(false);
            sr.color = Color.clear ;
            
        }
        else
        {

            myHealthBar.SetActive(true);
            sr.color = Color.white ;
        }
    }
    
}
