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

public interface IDialogue
{
    string GetDialogueAt(int index);
    int GetDialogueCount();
}

public class TutorialManager : MonoBehaviour, IDialogue
{
    public static Action OnTutorialFinished;

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
        player = FindObjectOfType<Player>();
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

        
        var saltoDialog = dialogues.FirstOrDefault(d => d.text.ToLower().Contains("salto"));
        if (saltoDialog != null)
        {
            Debug.Log("Hay un diálogo que menciona el salto: " + saltoDialog.text);
        }
    }

    private void EndTutorial()
    {
        tutorialActive = false;
        tutorialPanel.SetActive(false);

        if (player != null) player.EnableControl(true);

        OnTutorialFinished?.Invoke();
    }
}
