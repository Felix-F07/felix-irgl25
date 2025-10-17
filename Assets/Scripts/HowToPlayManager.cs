using UnityEngine;
using TMPro; // Wajib ada untuk mengontrol TextMeshPro
using UnityEngine.UI; // Wajib ada untuk mengontrol Button
using UnityEngine.SceneManagement;

public class HowToPlayManager : MonoBehaviour
{
    [Header("UI Elements")]
    public TextMeshProUGUI guideDisplayText; // Slot untuk area teks
    public Button nextButton;
    public Button previousButton;

    [Header("Page Content")]
    [TextArea(5, 10)] // Membuat kolom teks lebih besar di Inspector
    public string[] guidePages; // Daftar semua teks halaman panduan

    private int currentPageIndex = 0;

    void Start()
    {
        // Tampilkan halaman pertama saat scene dimulai
        ShowPage(currentPageIndex);
    }

    // Fungsi untuk menampilkan halaman berdasarkan nomornya
    void ShowPage(int index)
    {
        // Ganti teks yang ditampilkan sesuai halaman
        guideDisplayText.text = guidePages[index];

        // Sembunyikan tombol "Previous" jika di halaman pertama
        previousButton.gameObject.SetActive(index > 0);

        // Sembunyikan tombol "Next" jika di halaman terakhir
        nextButton.gameObject.SetActive(index < guidePages.Length - 1);
    }

    // Fungsi ini akan dipanggil oleh tombol "Next"
    public void NextPage()
    {
        if (currentPageIndex < guidePages.Length - 1)
        {
            currentPageIndex++;
            ShowPage(currentPageIndex);
        }
    }

    // Fungsi ini akan dipanggil oleh tombol "Previous"
    public void PreviousPage()
    {
        if (currentPageIndex > 0)
        {
            currentPageIndex--;
            ShowPage(currentPageIndex);
        }
    }

    // Fungsi untuk kembali ke menu utama
    public void BackToMainMenu()
    {
        SceneManager.LoadScene("First"); // Pastikan nama scene menu utama Anda adalah "First"
    }
}