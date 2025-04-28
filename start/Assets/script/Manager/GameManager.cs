using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour,ISaveManager
{
    public static GameManager instance;
     

    private Transform player;

    [SerializeField] private CheckPoint[] checkPoints;
    [SerializeField] private string checkpointID;
    [SerializeField] private string closeCheckPointLoaded;

    [Header("Lost Currency")]
    [SerializeField] private GameObject lostCurrencyPrefab;
    public int LostCurrencyAmount;
    [SerializeField] private float lostCurrencyPositionX;
    [SerializeField] private float lostCurrencyPositionY;

    

    private void Awake()
    {
        if (instance != null)
            Destroy(instance);

        else
            instance = this;

        checkPoints = FindObjectsOfType<CheckPoint>();
    }


    public void RestartScene()
    {

        SaveManager.instance.SaveGame();
        Scene scene =SceneManager.GetActiveScene();

        SceneManager.LoadScene(scene.name);
    }


    public void BackToMenu()
    {
        SaveManager.instance.SaveGame();
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }

    private void Start()
    {
        player = PlayerManager.instance.player.transform;
    }

    
    public void LoadData(GameData _data)
    {
        
        StartCoroutine(DelayStartLoad(_data));

    }

    private void LoadCheckPoint(GameData _data)
    {
        foreach (KeyValuePair<string, bool> pair in _data.checkPoints)
        {
            foreach (CheckPoint _checkPoint in checkPoints)
            {
                if (_checkPoint.checkPonitID == pair.Key && pair.Value == true)
                {
                    _checkPoint.ActivateCheckPoint();
                }
            }
        }
    }

    private void LoadLostCurrency(GameData _data)
    {
        //dead body position

        Debug.Log("LostCurrency " +_data.lostCurrency);
        LostCurrencyAmount = _data.lostCurrency;
        lostCurrencyPositionX = _data.lostCurrencyX;
        lostCurrencyPositionY = _data.lostCurrencyY;

        if (LostCurrencyAmount > 0)
        {
           
            Debug.Log("deadBody loaded");
            GameObject newLostCurrency = Instantiate(lostCurrencyPrefab, new Vector3(lostCurrencyPositionX, lostCurrencyPositionY),Quaternion.identity);
            newLostCurrency.GetComponent<LostCurrency>().currency=LostCurrencyAmount;
        }

        LostCurrencyAmount = 0;
    }


    public void SaveData(ref GameData _data)
    {
        _data.lostCurrency = LostCurrencyAmount;
        _data.lostCurrencyX = player.position.x;
        _data.lostCurrencyY = player.position.y;

        if(ClosestCheckPoint()!=null)
          _data.closestCheckPointID = ClosestCheckPoint().checkPonitID;
        
        _data.checkPoints.Clear();

        foreach(CheckPoint _checkPoint in checkPoints)
        {
            _data.checkPoints.Add(_checkPoint.checkPonitID,_checkPoint.isActive);
        }
    }

    private IEnumerator DelayStartLoad(GameData _data)
    {
        yield return new WaitForSeconds(.1f);
        
        
        LoadCheckPoint(_data);  
        PlaceManager(_data);
        LoadLostCurrency(_data);
    }


    private CheckPoint ClosestCheckPoint()
    {
        float closestDis = Mathf.Infinity;
        CheckPoint closeCheckPoint = null;
        foreach (var _checkpoint in checkPoints)
        {
            float distanceToCheckpoint = Vector2.Distance(player.position, _checkpoint.transform.position);
            if (distanceToCheckpoint < closestDis)
            {
                closestDis = distanceToCheckpoint;
                closeCheckPoint= _checkpoint;   

            }

        }
        return closeCheckPoint;
    }

    private void PlaceManager(GameData _data)
    {
        if (_data.closestCheckPointID == null)
            return; 

        closeCheckPointLoaded = _data.closestCheckPointID;
        foreach (CheckPoint _checkPoint in checkPoints)
        {
            if (_checkPoint.checkPonitID == closeCheckPointLoaded)
            {
                player.position = _checkPoint.transform.position;
            }

        }
    }

    public void PauseGame(bool _pause)
    {
        if (_pause)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }
}
