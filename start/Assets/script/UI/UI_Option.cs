using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UI_Option : MonoBehaviour
{
    [SerializeField]private string sceneName = "MainMenu";
    [SerializeField] private Button exitButton;

    private void Start()
    {
        exitButton.onClick.AddListener(ExitAndSave);
    }

    private void ExitAndSave()
    {
        SaveManager.instance.SaveGame();
        SceneManager.LoadScene(sceneName);
    }
}
