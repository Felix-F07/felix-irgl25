using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int startingHealth = 3;
    [SerializeField] private GameObject deathVFXPrefab;

    private int currentHealth;
    private KnockBack knockBack;
    private Flash flash;

    private void Awake()
    {
        flash = GetComponent<Flash>();
        knockBack = GetComponent<KnockBack>();
    }

    private void Start()
    {
        currentHealth = startingHealth;
    }

    public void TakeDamage(int damage)
    {
        // Kurangi nyawa
        currentHealth -= damage;

        // Berikan efek knockback dan flash
        if (knockBack != null)
        {
            knockBack.GetKnockedBack(PlayerController.Instance.transform, 15f);
        }
        if (flash != null)
        {
            StartCoroutine(flash.FlashRoutine());
        }

        // Langsung cek apakah musuh mati setelah menerima damage
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Panggil GameManager dan tambahkan 20 poin
        if (GameManager.instance != null)
        {
            GameManager.instance.AddScore(20);
        }

        // Munculkan efek visual kematian jika ada
        if (deathVFXPrefab != null)
        {
            Instantiate(deathVFXPrefab, transform.position, Quaternion.identity);
        }

        // Hancurkan objek musuh
        Destroy(gameObject);
    }
}