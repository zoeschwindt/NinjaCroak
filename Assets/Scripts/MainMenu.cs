using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{

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
        // Carga la última escena antes de morir
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
