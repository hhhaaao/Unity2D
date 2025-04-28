using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class UI_MainMenu : MonoBehaviour
{
    [SerializeField] private string sceneName = "MainScene";//要转到的场景
    [SerializeField] private GameObject continueButton;
    [SerializeField] UI_FadeScreen fadeScreen;
    public void ContinueGame()
    {
        Debug.Log("continue");
        StartCoroutine("LoadScreenWithEffect", 1.5f);


    }

    private void Start()
    {
        if (SaveManager.instance.HasSaveData()==false)
            continueButton.SetActive(false);
    }

    public void NewGame()
    {
        Debug.Log("new");
        SaveManager.instance.DeleteSavedData();
        StartCoroutine("LoadScreenWithEffect", 1.5f);
    }

    public void ExitGame()
    {
        Debug.Log("Exit Game"); 
        Application.Quit();
    }

    IEnumerator LoadScreenWithEffect(float _delay)
    {
        Debug.Log("called1");
        fadeScreen.FadeOut();
        Debug.Log("called2");
        Debug.Log(Time.timeScale);
        yield return new WaitForSeconds(_delay);
        Debug.Log("called3");
        SceneManager.LoadScene(sceneName);
        Debug.Log("called4");
    }
}
