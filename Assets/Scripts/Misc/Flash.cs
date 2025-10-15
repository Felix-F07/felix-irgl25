using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flash : MonoBehaviour
{
    [SerializeField] private Material whiteFlashMat;
    [SerializeField] private float restoreDefaultMatTime = .2f;

    private Material defaultMat;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        defaultMat = spriteRenderer.material;
    }

    public IEnumerator FlashRoutine()
    {
        // Ganti material menjadi putih
        spriteRenderer.material = whiteFlashMat;

        // Tunggu sesaat
        yield return new WaitForSeconds(restoreDefaultMatTime);

        // Kembalikan ke material semula
        spriteRenderer.material = defaultMat;

        // Baris enemyHealth.DetectDeath(); sudah dihapus dari sini
    }
}