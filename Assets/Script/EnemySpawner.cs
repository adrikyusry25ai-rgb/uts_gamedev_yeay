using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private Transform[] titikSpawn;
    [SerializeField] private float jedaSpawn = 3f;
    [SerializeField] private int maxEnemyAktif = 5;

    private float timer;

    void Update()
    {
        if (enemyPrefabs.Length == 0 || titikSpawn.Length == 0) return;

        timer += Time.deltaTime;
        if (timer < jedaSpawn) return;
        timer = 0f;

        int aktif = FindObjectsByType<Enemy>(FindObjectsSortMode.None).Length;
        if (aktif >= maxEnemyAktif) return;

        GameObject prefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
        Transform titik = titikSpawn[Random.Range(0, titikSpawn.Length)];
        Instantiate(prefab, titik.position, Quaternion.identity);
    }
}