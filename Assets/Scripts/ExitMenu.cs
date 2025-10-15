using UnityEngine;
using TMPro; 

public class ExitMenu : MonoBehaviour
{
    public TextMeshProUGUI finalScoreText; 

    void Start()
    {
        
        if (GameManager.instance != null)
        {
            
            finalScoreText.text = "FINAL SCORE : " + GameManager.instance.score;
        }
        else
        {
            
            finalScoreText.text = "SCORE NOT FOUND";
        }
    }

    public void QuitGame()
    {
        Debug.Log("Tombol Quit ditekan!");
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}