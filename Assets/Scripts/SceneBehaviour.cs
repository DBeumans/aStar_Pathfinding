using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneBehaviour : MonoBehaviour {

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ResetLevel()
    {
        SceneManager.LoadScene(0);
    }
}
