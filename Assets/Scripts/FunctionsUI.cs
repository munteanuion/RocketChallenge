using UnityEngine;
using UnityEngine.SceneManagement;

public class FunctionsUI : MonoBehaviour
{
    [SerializeField] private GameObject _losePanel;
    [SerializeField] private GameObject _winPanel;
    [SerializeField] private GameObject _pausePanel;
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && 
            SceneManager.GetActiveScene().buildIndex != 0 &&
            _winPanel.activeSelf == false &&
            _losePanel.activeSelf == false
            )
            PauseGame();
    }

    public void PauseGame()
    {
        if (_pausePanel.activeSelf == true)
        {
            DisablePausePanel();
            Time.timeScale = 1f;
        }
        else if (_pausePanel.activeSelf == false)
        {
            EnablePausePanel();
            Time.timeScale = 0f;
        }
    }

    public void EnableLosePanel()
    {
        EnablePanel(_losePanel);
    }

    public void EnableWinPanel()
    {
        EnablePanel(_winPanel);
    }

    public void DisablePausePanel()
    {
        DisablePanel(_pausePanel);
    }

    public void EnablePausePanel()
    {
        EnablePanel(_pausePanel);
    }

    private void DisablePanel(GameObject panel)
    {
        DisableCursor();
        panel.SetActive(false);
    }

    private void EnablePanel(GameObject panel)
    {
        EnableCursor();
        panel.SetActive(true);
    }

    public void EnableCursor()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    public void DisableCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
}
