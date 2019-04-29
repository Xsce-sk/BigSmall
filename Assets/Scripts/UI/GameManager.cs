using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public void MoveToNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void MoveToScene(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void QuitApplication()
    {
        Application.Quit();
    }
}
