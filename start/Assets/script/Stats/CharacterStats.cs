using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public enum StatType
{
    strength,
    agility,
    intelligence,
    vitality,
    damage,
    critChance,
    critPower,
    maxHp,
    armor,
    evasion,
    magicResistance,
    fireDamage,
    iceDamage,
    lightningDamage

}
public class CharacterStats : MonoBehaviour 
{
    private EntityFX fx;

    #region Major
    [Header("Major Stats")]
    public Stat strength;//increase damage by 1 and crit.damage by 1
    public Stat agility;//increase evasion by 1% and crit.chance by 1%
    public Stat intelligence;//increase magic damage by 1 and magic.resistance by 1%
    public Stat vitality;//increase health by 1
    #endregion

    #region Defensive
    [Header("Defensive Stats")]
    public Stat maxHp;
    public Stat armor;
    public Stat evasion;
    public Stat magicResistance;
    #endregion

    #region Offensive
    [Header("Offensive Stats")]
    public Stat damage;
    public Stat critChance;
    public Stat critPower;
    #endregion

    #region Magic
    [Header("Magic Stats")]
    public Stat fireDamage;
    public Stat iceDamage;
    public Stat lightningDamage;
    #endregion

    #region ElementInfo
    //fire
    public bool isIgnited;//constant damage
    private float igniteTimer;//total ignite time
    private float igniteCoolDown = .3f;
    private float igniteDamageTimer;//each damage time
    private int igniteDamage;
    //ice
    public bool isChilled;//-20% armer
    private float chillTimer;
    //lightning
    public bool isShocked;//+20% miss chance
    private float shockTimer;
    private int shockDamage;
    [SerializeField] private GameObject thunderPrefab;

    #endregion

    public int currentHealth;

    public System.Action onHealthChanged;
    public bool isDead { get; private set; }
    public bool isInvincible { get; private set; }

    public void IncreaseStatsBy(int _modifer,float _duration,Stat _statToModify)
    {
        StartCoroutine(StatModCoroutine(_modifer, _duration, _statToModify));
    }

    private IEnumerator StatModCoroutine(int _modifer, float _duration, Stat _statToModify)
    {
        _statToModify.AddModifier(_modifer);
        yield return new WaitForSeconds(_duration);
        _statToModify.RemoveModifier(_modifer);
    }

    protected virtual void Start()
    {
        critPower.SetDefaultValue(150);
        currentHealth = GetMaxHP();
        fx=GetComponent<EntityFX>();
        //damage.AddModifier(14);
    }

    protected virtual void Update()
    {
        igniteTimer -= Time.deltaTime;
        chillTimer -= Time.deltaTime;
        shockTimer -= Time.deltaTime;
        igniteDamageTimer -= Time.deltaTime;


        if (igniteTimer < 0)
            isIgnited = false;
        if (chillTimer < 0)
            isChilled = false;
        if (shockTimer < 0)
            isShocked = false;

        ApplyIgniteDamage();
    }

    

    public virtual void DoDamage(CharacterStats _targetStats)// _targetStats为承伤方
    {
        if (CheckEvasion(_targetStats))
            return;

        _targetStats.GetComponent<Entity>().SetupKnockBackDir(transform);

        int totalDamage = damage.GetValue() + strength.GetValue();
        if(CheckCrit())
        {
            //Debug.Log("CCCCCCCRIT ATTACK!!!!!");
            totalDamage = CalculateCritDamage(totalDamage);


        }
        totalDamage = CheckArmor(_targetStats, totalDamage);
        _targetStats.TakeDamage(totalDamage);//physical
        TakeMagicDamage(_targetStats);//magical
    }


    public int GetMaxHP()
    {
        return maxHp.GetValue() + vitality.GetValue() * 5;
    }
    public virtual void TakeDamage(int _damage)
    {
        if (isInvincible)
            return;

        DecreaseHealthBy(_damage);


        //不要死好几次
        if (currentHealth <= 0&&!isDead)
            Die();
    }
    protected virtual void Die()
    {
        isIgnited = false;
        isDead = true;
        
        //Destroy(gameObject);
    }

    
    protected virtual void DecreaseHealthBy(int _damage)//-hp
    {
        currentHealth -= _damage;
        if (onHealthChanged != null)
            onHealthChanged();

        if (_damage > 0)
            fx.CreatePopUpText(_damage.ToString());
    }

    //治疗
    public virtual void IncreaseHealthBy(int _heal)
    {
        currentHealth += _heal;
        if (currentHealth >= GetMaxHP())
            currentHealth = GetMaxHP();

        if (onHealthChanged != null)
            onHealthChanged();
    }

    #region Caculate
    private int CheckArmor(CharacterStats _targetStats, int totalDamage)
    {
        if (_targetStats.isChilled)
            totalDamage -= Mathf.RoundToInt(_targetStats.armor.GetValue() * 0.8f);
        else
            totalDamage -= _targetStats.armor.GetValue();

        totalDamage = Mathf.Clamp(totalDamage, 0, int.MaxValue);
        return totalDamage;
    }
    private bool CheckEvasion(CharacterStats _targetStats)
    {
        int totalEvasion = _targetStats.evasion.GetValue() + _targetStats.agility.GetValue();//target evasion chance

        if(isShocked)//自己shock，对面加evasion
        {
            totalEvasion += 20;
        }
        if (Random.Range(0, 100) < totalEvasion)
        {
            //Debug.Log("Attack MISS");
            return true;

        }
        else
            return false;
    }
    private  int CheckMagicResistance(CharacterStats _targeStats, int totalMagicDamage)
    {
        totalMagicDamage -= _targeStats.magicResistance.GetValue() - (_targeStats.intelligence.GetValue() * 3);
        totalMagicDamage = Mathf.Clamp(totalMagicDamage, 0, int.MaxValue);
        return totalMagicDamage;
    }
    private bool CheckCrit()
    {
        int totalCritChance=critChance.GetValue()+agility.GetValue();
        if (Random.Range(0, 100) <= totalCritChance)
            return true;
        else
            return false;
    }
    private int CalculateCritDamage(int _damage)
    {
        float totalCritPower = (strength.GetValue() + critPower.GetValue()) * .01f;
        //Debug.Log("CritPower added is " + totalCritPower);

        float critDamage = _damage + totalCritPower;
        return Mathf.RoundToInt(critDamage);
    }
    #endregion

    #region Magic Damage
    public virtual void TakeMagicDamage(CharacterStats _targetStats)
    {
        int _fireDamage = fireDamage.GetValue();
        int _iceDamage = iceDamage.GetValue();
        int _lightningDamage = lightningDamage.GetValue();
        if(_fireDamage==0&& _iceDamage==0&&_lightningDamage==0)
            return;
        int totalMagicDamage = _fireDamage + _iceDamage + _lightningDamage + intelligence.GetValue();
        totalMagicDamage = CheckMagicResistance(_targetStats, totalMagicDamage);
        //Debug.Log("MAGICDAMAGE IS "+totalMagicDamage);
        _targetStats.TakeDamage(totalMagicDamage);

        if (Mathf.Max(_fireDamage, _iceDamage, _lightningDamage) < 0)
        {
            Debug.Log("NO ELEMENTAL DAMAGE");
            return;
        }
        //Debug.Log("isSHOCKED? " + _targetStats.isShocked);
        //伤害最高的效果
        AttemptToApplyAliment(_targetStats, _fireDamage, _iceDamage, _lightningDamage);
    }

    private  void AttemptToApplyAliment(CharacterStats _targetStats, int _fireDamage, int _iceDamage, int _lightningDamage)
    {
        bool canIgnite = _fireDamage > _iceDamage && _fireDamage > _lightningDamage;
        bool canChill = _iceDamage > _fireDamage && _iceDamage > _lightningDamage;
        bool canShock = _lightningDamage > _iceDamage && _lightningDamage > _fireDamage;
        //fire=ice=lightning
        while (!canIgnite && !canChill && !canShock)
        {
            if (Random.value < 0.5f && _fireDamage > 0)
            {
                canIgnite = true;
                _targetStats.ApplyAilments(canIgnite, canChill, canShock);
                Debug.Log("Apply fire");
                return;

            }
            if (Random.value < 0.5f && _iceDamage > 0)
            {
                canChill = true;
                _targetStats.ApplyAilments(canIgnite, canChill, canShock);
                Debug.Log("Apply ice");
                return;
            }
            if (Random.value < 0.5f && _lightningDamage > 0)
            {
                canShock = true;
                _targetStats.ApplyAilments(canIgnite, canChill, canShock);
                Debug.Log("Apply lightning");
                return;
            }

        }

        if (canIgnite)
            _targetStats.SetIgniteDamage(Mathf.RoundToInt(_fireDamage * 0.15f));

        _targetStats.ApplyAilments(canIgnite, canChill, canShock);
    }

    public void ApplyAilments(bool _ignite,bool _chill,bool _shock)
    {
        bool canApplyIgnite = !isIgnited && !isChilled && !isShocked;
        bool canApplyChill = !isIgnited && !isChilled && !isShocked;
        bool canApplyShock = !isIgnited && !isChilled ;


        //if (isIgnited || isChilled || isShocked)
        //    return;//草草草草草泥马

        if(_ignite&&canApplyIgnite)
        {
        
        isIgnited = _ignite;
            igniteTimer = 4;
            fx.StartCoroutine("IgniteFor",igniteTimer);
        }
        if(_chill&&canApplyChill)
        {
           isChilled = _chill;
            chillTimer = 3;
            fx.StartCoroutine("ChillFor",chillTimer);

            float slowPercent = .5f;
            GetComponent<Entity>().SlowEntityBy(slowPercent, chillTimer);
            
        }
        if(_shock&&canApplyShock)
        {
           
            if(!isShocked)
            {
                ApplyShock(_shock);
            }
            else
            {
                //find closest enemy
                if (GetComponent<Player>() != null)
                    return;
                ThunderHitNearestEnemy();

                //setup target strike

            }
        }


    }

    public void ApplyShock(bool _shock)
    {
        if (isShocked)
            return;
        isShocked = _shock;
        shockTimer = 5;
        fx.StartCoroutine("ShockFor", shockTimer);
    }

    private void ThunderHitNearestEnemy()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 25);
        float closestDistance = Mathf.Infinity;
        Transform closestEnemy = null;
        foreach (var a in colliders)
        {

            if (a.GetComponent<Enemy>() != null &&/*Vector2.Distance(transform.position,a.transform.position)>1&&*/a.gameObject != gameObject)
            {
                float distanceToEnemy = Vector2.Distance(transform.position, a.transform.position);

                if (distanceToEnemy < closestDistance)
                {
                    closestDistance = distanceToEnemy;
                    closestEnemy = a.transform;
                }
                Debug.Log("closest enemy: " + closestEnemy.gameObject);
            }
        }
        if (closestEnemy == null)
        {
            closestEnemy = transform;
        }
        //instatnitate thunder strike
        if (closestEnemy != null)
        {
            Debug.Log("closest enemy: " + closestEnemy.gameObject);
            GameObject newShockStrike = Instantiate(thunderPrefab, transform.position, Quaternion.identity);
            newShockStrike.GetComponent<ThunderController>().SetUp(shockDamage, closestEnemy.GetComponent<CharacterStats>());

        }
    }

    public void SetIgniteDamage(int _damage) => igniteDamage = _damage;

    private void ApplyIgniteDamage()
    {
        if (isIgnited && igniteDamageTimer < 0)
        {
            //Debug.Log(gameObject+" IS ON FIRRRRRRRE,Damage is "+igniteDamage);
            DecreaseHealthBy(igniteDamage);
            if (currentHealth < 0&&!isDead)
                Die();
            igniteDamageTimer = igniteCoolDown;
        }
    }
    #endregion

    public Stat StatOfType(StatType _statType)
    {
        switch (_statType)
        {
            case StatType.strength: return strength;
            case StatType.agility: return agility;
            case StatType.intelligence: return intelligence;
            case StatType.vitality: return vitality;
            case StatType.damage: return damage;
            case StatType.critChance: return critChance;
            case StatType.critPower: return critPower;
            case StatType.evasion: return evasion;
            case StatType.maxHp: return maxHp;
            case StatType.armor: return armor;
            case StatType.magicResistance: return magicResistance;
            case StatType.fireDamage: return fireDamage;
            case StatType.iceDamage: return iceDamage;
            case StatType.lightningDamage: return lightningDamage;
            default:
                return null;
        }

    }

    public void KillEntity() => Die();

    public void MakeInvicible(bool _isInvicible) => isInvincible = _isInvicible;
   

    
}
