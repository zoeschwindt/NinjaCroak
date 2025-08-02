using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class ScoreUI : MonoBehaviour
{
    [Header("Referencias UI")]
    public TMP_Text scoreText;
    public Image iconoRecoleccion; 
    public AudioSource pickupSound;

    [Header("Config")]
    public float iconDisplayTime = 2f; 

    void Start()
    {
        
        GameManager.Instance.OnScoreChanged += UpdateScoreUI;

        
        if (iconoRecoleccion != null)
            iconoRecoleccion.gameObject.SetActive(false);
    }

    void UpdateScoreUI(int newScore)
    {
       
        if (scoreText != null)
            scoreText.text = newScore.ToString();


        if (iconoRecoleccion != null)
            StartCoroutine(ShowIcon());

       
        if (pickupSound != null)
            pickupSound.Play();
    }

    IEnumerator ShowIcon()
    {
        iconoRecoleccion.gameObject.SetActive(true);
        yield return new WaitForSeconds(iconDisplayTime);
        iconoRecoleccion.gameObject.SetActive(false);
    }
}
