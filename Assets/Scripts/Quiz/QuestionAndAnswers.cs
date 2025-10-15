using UnityEngine;

[System.Serializable]
public class QuestionAndAnswers
{
    public string Question;
    public string[] Answers;
    public int CorrectAnswer;

    // TAMBAHKAN BARIS INI: Variabel baru untuk soal gambar
    public Sprite questionImage;
}