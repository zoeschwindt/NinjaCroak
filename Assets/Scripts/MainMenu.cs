using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None; 
    }

    public void Jugar()
    {
        SceneManager.LoadScene("Game");
    }

    public void Salir()
    {
        Debug.Log("Saliendo...");
        Application.Quit();
    }

    public void VolverJugar()
    {
        SceneManager.LoadScene(GameSession.LastSceneName);
    }

    public void VolverMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Credits()
    {
        SceneManager.LoadScene("Credits");
    }
}
