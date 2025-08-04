using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{
    public int maxLives = 4; 
    public int currentLives;

    public Image[] pizzaImages; // Array de imágenes que representan las vidas (UI)

    void Start()
    {
        currentLives = maxLives;
        UpdateLivesUI();
    }


    public void TakeDamage(int amount)
    {
        currentLives -= amount;
        if (currentLives < 0) currentLives = 0;

        UpdateLivesUI();

        if (currentLives <= 0)
        {
            // Guarda la escena actual para poder volver a ella
            GameSession.LastSceneName = SceneManager.GetActiveScene().name;
            // Cambia a escena de "Moriste"
            SceneManager.LoadScene("Moriste");
        }
    }

    void UpdateLivesUI()
    {
        // Actualiza el UI activando/desactivando imágenes según vida actual
        for (int i = 0; i < pizzaImages.Length; i++)
        {
            pizzaImages[i].enabled = (i < currentLives);
        }
    }
}
