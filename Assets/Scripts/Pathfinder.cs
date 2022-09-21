using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
    Este script se encarga de hacer que los enemigos sigan la ruta establecida.
    Está adherido como componente al prefab de los enemigos.
    La información sobre su ruta y la ola están definidas en el GO EnemySpawner.
*/
public class Pathfinder : MonoBehaviour
{
    EnemySpawner enemySpawner;
    WaveConfigSO waveConfig;
    List<Transform> waypoints;
    int waypointIndex = 0;

    void Awake()
    {
        enemySpawner = FindObjectOfType<EnemySpawner>();
    }

    void Start()
    {
        waveConfig = enemySpawner.GetCurrentWave();
        waypoints = waveConfig.GetWaypoints();
        transform.position = waypoints[waypointIndex].position;
    }

    void Update()
    {
        FollowPath();
    }

    void FollowPath()
    {
        // Si el punto de ruta no es el último
        if (waypointIndex < waypoints.Count)
        {
            Vector3 targetPosition = waypoints[waypointIndex].position;
            float delta = waveConfig.GetMoveSpeed() * Time.deltaTime;
            // El GO se moverá hasta llegar a [targetPosition] durante un tiempo [delta]
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, delta);
            // Si la posición es igual a [targetPosition] salta al siguiente punto de ruta
            if (transform.position == targetPosition)
            {
                waypointIndex++;
            }
        }
        else
        {
            // Si es el último punto de ruta se destruye el GO
            Destroy(gameObject);
        }
    }
}