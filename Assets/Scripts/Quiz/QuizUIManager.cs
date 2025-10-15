using System.Collections;
using UnityEngine;
using UnityEngine.UI; 
public class QuizUIManager : MonoBehaviour
{
    public static QuizUIManager instance;

    [Header("UI Elements")]
    public GameObject quizPanel;
    public Text questionText;
    public Image soalImage; 
    public Button[] answerButtons;
    public Button backButton;

   
    [Header("Feedback Settings")]
    public Color correctColor = Color.green;
    public Color wrongColor = Color.red;
    public Color normalColor = Color.white;
    public float feedbackDelay = 1.0f;

    private QuizChest activeChest;

    private void Awake()
    {
        if (instance == null) { instance = this; }
    }

    void Start()
    {
        quizPanel.SetActive(false);
    }

    public void ShowQuiz(QuestionAndAnswers data, QuizChest chest)
    {
        activeChest = chest;
        SetAllButtonsInteractable(true);
        quizPanel.SetActive(true);

       
        if (data.questionImage != null)
        {
            // ada soal gambar
            soalImage.gameObject.SetActive(true);
            questionText.gameObject.SetActive(false);

            soalImage.sprite = data.questionImage;
        }
        else
        {
            // hanya ada soal teks
            soalImage.gameObject.SetActive(false);
            questionText.gameObject.SetActive(true);

            questionText.text = data.Question;
        }
       

        for (int i = 0; i < answerButtons.Length; i++)
        {
            answerButtons[i].GetComponentInChildren<Text>().text = data.Answers[i];
            answerButtons[i].GetComponent<Image>().color = normalColor;

            Button currentButton = answerButtons[i];
            int buttonIndex = i;
            currentButton.onClick.RemoveAllListeners();
            currentButton.onClick.AddListener(() => OnAnswerSelected(currentButton, buttonIndex + 1, data.CorrectAnswer));
        }
    }

    public void OnAnswerSelected(Button selectedButton, int selectedIndex, int correctIndex)
    {
        SetAllButtonsInteractable(false);
        bool isCorrect = (selectedIndex == correctIndex);

        if (isCorrect)
        {
            StartCoroutine(CorrectAnswerRoutine(selectedButton));
        }
        else
        {
            StartCoroutine(WrongAnswerRoutine(selectedButton));
        }
    }

    private IEnumerator CorrectAnswerRoutine(Button selectedButton)
    {
        selectedButton.GetComponent<Image>().color = correctColor;

        
        // Ambil poin dari peti, bukan angka tetap 150 (karena bisa berkurang)
        int pointsToAdd = activeChest.GetCurrentPoints();
        GameManager.instance.AddScore(pointsToAdd);
        

        activeChest.CompleteQuiz();

        yield return new WaitForSeconds(feedbackDelay);
        HideQuiz();
    }

    private IEnumerator WrongAnswerRoutine(Button selectedButton)
    {
        activeChest.UseAttempt(); // kesempatan min 1
        selectedButton.GetComponent<Image>().color = wrongColor;

        yield return new WaitForSeconds(feedbackDelay);

        if (activeChest.HasAttemptsLeft())
        {
            // kalau masih ada kesempatan, kembalikan warna tombol dan aktifkan lagi
            selectedButton.GetComponent<Image>().color = normalColor;
            SetAllButtonsInteractable(true);
        }
        else
        {
            // kalau kesempatan habis, kunci peti dan tutup quiz
            activeChest.CompleteQuiz();
            HideQuiz();
        }
    }

    // Fungsi bantuan untuk mengaktifkan/menonaktifkan semua tombol
    private void SetAllButtonsInteractable(bool state)
    {
        foreach (Button btn in answerButtons)
        {
            btn.interactable = state;
        }
        if (backButton != null)
        {
            backButton.interactable = state;
        }
    }

    public void HideQuiz()
    {
        quizPanel.SetActive(false);
        activeChest = null;
    }

    public void OnBackButtonClicked()
    {
        quizPanel.SetActive(false);
        activeChest = null;
    }
}