using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class ScoreUI : MonoBehaviour
{
    [Header("Referencias UI")]
    public TMP_Text collectiblesText;
    public TMP_Text enemiesText;
    public Image iconoRecoleccion;
    public AudioSource pickupSound;

    [Header("Icono y sonido por enemigos derrotados")]
    public Image iconoEnemigo;
    public AudioSource enemyKillSound;

    [Header("Config")]
    public float iconDisplayTime = 2f;

    void Start()
    {
        GameManager.Instance.OnCollectiblesChanged += UpdateCollectiblesUI;
        GameManager.Instance.OnEnemiesChanged += UpdateEnemiesUI;

        if (iconoRecoleccion != null)
            iconoRecoleccion.gameObject.SetActive(false);

        if (iconoEnemigo != null)
            iconoEnemigo.gameObject.SetActive(false);

        UpdateCollectiblesUI(0, GameManager.Instance.totalCollectibles);
        UpdateEnemiesUI(0, GameManager.Instance.totalEnemies);
    }

    void UpdateCollectiblesUI(int current, int total)
    {
        if (collectiblesText != null)
            collectiblesText.text = $"{current}/{total}";

        if (iconoRecoleccion != null)
            StartCoroutine(ShowIcon());

        if (pickupSound != null)
            pickupSound.Play();
    }

    void UpdateEnemiesUI(int current, int total)
    {
        if (enemiesText != null)
            enemiesText.text = $"{current}/{total}";

        if (iconoEnemigo != null)
            StartCoroutine(ShowEnemyIcon());

        if (enemyKillSound != null)
            enemyKillSound.Play();
    }

    IEnumerator ShowIcon()
    {
        iconoRecoleccion.gameObject.SetActive(true);
        yield return new WaitForSeconds(iconDisplayTime);
        iconoRecoleccion.gameObject.SetActive(false);
    }

    IEnumerator ShowEnemyIcon()
    {
        iconoEnemigo.gameObject.SetActive(true);
        yield return new WaitForSeconds(iconDisplayTime);
        iconoEnemigo.gameObject.SetActive(false);
    }
}
