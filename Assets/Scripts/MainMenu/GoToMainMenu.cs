using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GoToMainMenu : MonoBehaviour
{
    public DataPersistenceManager dataPersistenceManager;

    private void Awake()
    {
        dataPersistenceManager = GameObject.FindObjectOfType<DataPersistenceManager>();
    }
    public void GoToMenu()
    {
        dataPersistenceManager.SaveTool();
        SceneManager.LoadScene(0);
    }
}
