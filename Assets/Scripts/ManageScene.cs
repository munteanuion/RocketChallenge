using UnityEngine.SceneManagement;
using UnityEngine;

public class ManageScene : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L) && Debug.isDebugBuild)
            LoadNextLevel();
    }
    public void LoadNextLevel()
    {
        if (SceneManager.sceneCountInBuildSettings > SceneManager.GetActiveScene().buildIndex + 1)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        else
            LoadMainMenu();
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void ReloadScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
