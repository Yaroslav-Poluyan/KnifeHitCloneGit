using System;
using System.Collections.Generic;
using Circle.ScriptableObjects;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Circle
{
    public class ObjectsOnCircleSpawner : MonoBehaviour
    {
        public static ObjectsOnCircleSpawner Current;
        private List<Transform> spawnPoints = new List<Transform>();
        [SerializeField] private GameObject watermelonPrefab;
        [SerializeField] private GameObject staticKnifePrefab;
        [SerializeField] SpawnSettings waterMelonSpawnSettings;

        public void SpawnRandomUnits()
        {
            var knifesCount = Random.Range(1, 4);
            var waterMelonsAtStart = Random.Range(0, 4);
            var chance = (int)(100 / waterMelonSpawnSettings.spawnRate);
            if (waterMelonsAtStart != 0) //25%
            {
                SpawnWaterMelon();
            }

            for (int i = 0; i < knifesCount; i++)
            {
                SpawnKnife();
            }
        }
        private void SpawnWaterMelon()
        {
            var randomPlace = 0;
            while (spawnPoints[randomPlace].childCount != 0)
            {
                randomPlace = Random.Range(0, spawnPoints.Count - 1);
            }
            var obj = Instantiate(watermelonPrefab, spawnPoints[randomPlace]);
        }
        private void SpawnKnife()
        {
            var randomPlace = 0;
            while (spawnPoints[randomPlace].childCount != 0)
            {
                randomPlace = Random.Range(0, spawnPoints.Count - 1);
            }
            var obj = Instantiate(staticKnifePrefab, spawnPoints[randomPlace]);
            var rb = obj.GetComponent<Rigidbody>();
            rb.isKinematic = true; 
            rb.useGravity = false;
        }
        private void Awake()
        {
            Current = this;
            spawnPoints.AddRange(GetComponentsInChildren<Transform>());
        }
    }
}