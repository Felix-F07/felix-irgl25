using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    [Header("Portal Settings")]
    public string sceneNameToLoad;

    [Header("Spawn Point Settings")]
    public string portalName;
    public string destinationPortalName;

    // TAMBAHKAN BARIS INI
    // Ini adalah tempat pemain akan muncul setelah keluar dari portal ini.
    public Transform spawnPoint;

    private static List<string> usedPortals = new List<string>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (usedPortals.Contains(sceneNameToLoad))
        {
            Debug.Log("Portal ke " + sceneNameToLoad + " sudah digunakan.");
            return;
        }

        if (collision.CompareTag("Player"))
        {
            PlayerSpawner.nextEntryPointName = destinationPortalName;
            usedPortals.Add(sceneNameToLoad);
            SceneManager.LoadScene(sceneNameToLoad);
        }
    }

    public static void ResetUsedPortals()
    {
        usedPortals.Clear();
        Debug.Log("Semua status portal telah direset.");
    }
}