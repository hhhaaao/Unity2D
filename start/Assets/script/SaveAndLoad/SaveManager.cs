using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;
    [SerializeField] private string fileName;
    [SerializeField] private bool encryptData;
    private GameData gameData;

    private List<ISaveManager> saveManagers;

    private FileDataHandler dataHandler;

    [ContextMenu("Delete save file")]
    public void DeleteSavedData()
    {
        dataHandler = new FileDataHandler(Application.persistentDataPath, fileName,encryptData);
        dataHandler.Delete();
    }


    public void Awake()
    {
        if(instance!=null)
            Destroy(instance.gameObject);
        else
            instance = this;
    }

    public void NewGame()
    {
        gameData = new GameData();

    }

    private void Start()
    {
        dataHandler = new FileDataHandler(Application.persistentDataPath,fileName,encryptData);

        saveManagers = FindAllSaveManagers();

        LoadGame();
    }

    public void LoadGame()
    {
        gameData = dataHandler.Load();
        //gameData=data from data handler
        if (this.gameData == null)
        {
            Debug.Log("No Sava Data found");
            NewGame();
        }
        //找到所有实现IsaveManager的脚本
        foreach(ISaveManager saveManager in saveManagers)
        {
            saveManager.LoadData(gameData);
            
        }

        //Debug.Log("Loaded currency: "+gameData.currency);
    }

    public void SaveGame()
    {

        //gamehandler save gamedata
        Debug.Log("Game was saved");
        foreach(ISaveManager saveManager in saveManagers)
        {
            saveManager.SaveData(ref gameData);
        }
        dataHandler.Save(gameData);
        //Debug.Log("saved currency: " + gameData.currency);
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }


    private List<ISaveManager> FindAllSaveManagers()
    {
        IEnumerable<ISaveManager> saveManagers = FindObjectsOfType<MonoBehaviour>().OfType<ISaveManager>();
        return new List<ISaveManager>(saveManagers);
    }

    public bool HasSaveData()
    {
        if (dataHandler.Load() != null)
        {
            return true;
        }
        else
            return false;
    }
}
