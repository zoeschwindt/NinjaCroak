using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Linq;

[System.Serializable]
public class DialogueData
{
    [TextArea]
    public string text;
    public Sprite image;
}
// Interfaz para obtener diálogos (polimorfismo)
public interface IDialogue
{
    string GetDialogueAt(int index);
    int GetDialogueCount();
}

public class TutorialManager : MonoBehaviour, IDialogue
{
    public static Action OnTutorialFinished;// Evento para notificar que el tutorial terminó

    [Header("UI del Tutorial")]
    public GameObject tutorialPanel;
    public TMP_Text dialogueText;
    public Image dialogueImage;  
    public Button nextButton;
    public Button prevButton;

    [Header("Configuración")]
    public DialogueData[] dialogues; 
    private int currentIndex = 0;
    private bool tutorialActive = true;

    private Player player;

    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        player = UnityEngine.Object.FindFirstObjectByType<Player>();
        if (player != null) player.EnableControl(false);

        ShowDialogue(currentIndex);

        nextButton.onClick.AddListener(() => ShowNextDialogue());
        prevButton.onClick.AddListener(() => ShowPreviousDialogue());
    }

    public string GetDialogueAt(int index)
    {
        if (index >= 0 && index < dialogues.Length)
            return dialogues[index].text;
        return string.Empty;
    }

    public int GetDialogueCount() => dialogues.Length;

    private void ShowNextDialogue()
    {
        if (currentIndex < dialogues.Length - 1)
        {
            currentIndex++;
            ShowDialogue(currentIndex);
        }
        else
        {
            EndTutorial();
        }
    }

    private void ShowPreviousDialogue()
    {
        if (currentIndex > 0)
        {
            currentIndex--;
            ShowDialogue(currentIndex);
        }
    }

    private void ShowDialogue(int index)
    {
        
        dialogueText.text = dialogues[index].text;

        
        if (dialogues[index].image != null && dialogueImage != null)
        {
            dialogueImage.sprite = dialogues[index].image;
            dialogueImage.gameObject.SetActive(true);
        }
        else if (dialogueImage != null)
        {
            dialogueImage.gameObject.SetActive(false);
        }

        
       
    }

    private void EndTutorial()
    {
        tutorialActive = false;
        tutorialPanel.SetActive(false);

        if (player != null) player.EnableControl(true);

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;



        OnTutorialFinished?.Invoke();
    }
}
