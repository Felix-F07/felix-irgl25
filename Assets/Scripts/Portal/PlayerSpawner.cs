using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public static string nextEntryPointName;

    private void Awake()
    {
        if (!string.IsNullOrEmpty(nextEntryPointName))
        {
            Portal[] allPortals = FindObjectsOfType<Portal>();
            foreach (Portal portal in allPortals)
            {
                if (portal.portalName == nextEntryPointName)
                {
                    GameObject player = GameObject.FindGameObjectWithTag("Player");
                    if (player != null)
                    {
                        // UBAH BARIS INI
                        // Sebelumnya: player.transform.position = portal.transform.position;
                        // Menjadi:
                        player.transform.position = portal.spawnPoint.position;

                        Debug.Log("Player spawned at: " + portal.spawnPoint.name);
                    }

                    nextEntryPointName = null;
                    break;
                }
            }
        }
    }
}