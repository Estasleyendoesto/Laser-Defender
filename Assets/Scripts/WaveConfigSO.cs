using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
    Script persistente que almacena datos, almacena la información de las rutas y los enemigos
*/
[CreateAssetMenu(menuName = "Wave Config", fileName = "New Wave Config")]
public class WaveConfigSO : ScriptableObject
{
    [SerializeField] List<GameObject> enemyPrefabs;
    [SerializeField] Transform pathPrefab;
    [SerializeField] float moveSpeed = 1.0f;
    [SerializeField] float timeBetweenEnemySpawns = 1.0f;
    [SerializeField] float spawnTimeVariance;
    [SerializeField] float minimumSpawnTime = 0.2f;

    // Punto de salida
    public Transform GetStartingWaypoint()
    {
        return pathPrefab.GetChild(0);
    }

    // Todos los puntos de la ruta dentro de una lista Transform
    public List<Transform> GetWaypoints()
    {
        List<Transform> waypoints = new List<Transform>();
        foreach(Transform child in pathPrefab)
        {  
            waypoints.Add(child);
        }
        return waypoints;
    }

    public float GetMoveSpeed()
    {
        return moveSpeed;
    }

    public int GetEnemyCount()
    {
        return enemyPrefabs.Count;
    }

    public GameObject GetEnemyPrefab(int index)
    {
        return enemyPrefabs[index];
    }

    // Spawn aleatorio, pero con variaciones y clamp
    public float GetRandomSpawnTime()
    {
        float spawnTime = Random.Range(timeBetweenEnemySpawns - spawnTimeVariance, timeBetweenEnemySpawns + spawnTimeVariance);
        return Mathf.Clamp(spawnTime, minimumSpawnTime, float.MaxValue);
    }
}
