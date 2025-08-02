using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{
    public int maxLives = 4; 
    public int currentLives;

    public Image[] pizzaImages; 

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
            SceneManager.LoadScene("Moriste");
        }
    }

    void UpdateLivesUI()
    {
        for (int i = 0; i < pizzaImages.Length; i++)
        {
            pizzaImages[i].enabled = (i < currentLives);
        }
    }
}
