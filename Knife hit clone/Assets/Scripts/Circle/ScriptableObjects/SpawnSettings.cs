using UnityEngine;

namespace Circle.ScriptableObjects
{
    [CreateAssetMenu(fileName = "SpawnData", menuName = "SpawnSettings", order = 0)]
    public class SpawnSettings : ScriptableObject
    {
        public float spawnRate;
    }
}