using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitPortal : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Player"))
        {
            
            if (GameManager.instance != null)
            {
                GameManager.instance.AddScore(150);
            }

            
            SceneManager.LoadScene("Exit");
        }
    }
}