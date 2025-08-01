using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LifeManager : MonoBehaviour
{
    public int vidaMaxima = 3;
    public Image[] pizzas; 
    private int vidaActual;

    void Start()
    {
        vidaActual = vidaMaxima;
        ActualizarUI();
    }

    public void QuitarVida(int cantidad)
    {
        vidaActual -= cantidad;
        vidaActual = Mathf.Clamp(vidaActual, 0, vidaMaxima);
        ActualizarUI();

        if (vidaActual <= 0)
        {
            SceneManager.LoadScene("Moriste"); 
        }
    }

    void ActualizarUI()
    {
        for (int i = 0; i < pizzas.Length; i++)
        {
            pizzas[i].enabled = i < vidaActual;
        }
    }
}
