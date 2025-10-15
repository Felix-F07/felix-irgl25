using UnityEngine;

public class QuizChest : MonoBehaviour
{
    [Header("Unique Chest ID")]
    public string chestID;

    public QuestionAndAnswers question;
    public bool hasBeenAnswered = false;

    private int attemptsRemaining = 3;

    // TAMBAHKAN INI: Variabel untuk menyimpan poin saat ini
    private int currentPoints = 150;

    private Animator animator;

    private void Awake()
    {
        // Hubungkan skrip dengan komponen Animator pada objek peti
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        // Cek ke GameManager apakah peti ini sudah pernah dijawab
        if (GameManager.instance.IsChestAnswered(chestID))
        {
            // Jika sudah, langsung set ke status "terjawab"
            hasBeenAnswered = true;
            if (animator != null)
            {
                // Langsung mainkan state akhir animasi (terbuka)
                animator.Play("Wooden Chest 1 Opening", 0, 1.0f);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !hasBeenAnswered)
        {
            QuizUIManager.instance.ShowQuiz(question, this);
        }
    }

    // Fungsi ini akan dipanggil saat jawaban salah
    public void UseAttempt()
    {
        if (attemptsRemaining > 0)
        {
            attemptsRemaining--;

            // TAMBAHKAN INI: Kurangi poin setiap kali salah
            currentPoints -= 25;
            if (currentPoints < 100)
            { // Batas bawah poin adalah 100 di kesempatan ke-3
                currentPoints = 100;
            }

            Debug.Log("Kesempatan tersisa: " + attemptsRemaining + ", Poin saat ini: " + currentPoints);
        }
    }

    // Fungsi baru untuk mendapatkan poin saat ini
    public int GetCurrentPoints()
    {
        return currentPoints;
    }

    public bool HasAttemptsLeft()
    {
        return attemptsRemaining > 0;
    }

    public void CompleteQuiz()
    {
        hasBeenAnswered = true;

        GameManager.instance.MarkChestAsAnswered(chestID);
        Debug.Log("Peti ini sudah selesai dijawab.");

        // TAMBAHKAN INI: Panggil trigger animasi
        if (animator != null)
        {
            animator.SetTrigger("Open");
        }
    }
}