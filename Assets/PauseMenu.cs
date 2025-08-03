using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [Header("Panel del Menú de Pausa")]
    public GameObject pausePanel;

    private bool isPaused = false;

    void Start()
    {
        if (pausePanel != null)
            pausePanel.SetActive(false); // Ocultar al iniciar
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f; // Pausa el juego
        if (pausePanel != null)
            pausePanel.SetActive(true);
    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f; // Restaura el tiempo
        if (pausePanel != null)
            pausePanel.SetActive(false);
    }
}
