using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesManager : MonoBehaviour
{
    public static ParticlesManager Current;
    [SerializeField] private GameObject knifesCollisionPrefab;
    [SerializeField] private GameObject knifesStuckPrefab;
    [SerializeField] private GameObject juicePrefab;
    [SerializeField] List<GameObject> _knifeCollisionCount = new List<GameObject>();

    public void CreateKnifeStuckParticles(Vector3 position, Transform parent)
    {
        var go = Instantiate(knifesStuckPrefab, position, Quaternion.identity, parent);
    }
    public void CreateJuiceParticles(Vector3 position)
    {
        var go = Instantiate(juicePrefab, position, Quaternion.identity, transform);
    }

    public void CreateKnifesCollisionParticles(Vector3 position)
    {
        _knifeCollisionCount.RemoveAll(item => item == null);
        if(_knifeCollisionCount.Count > 1) return;
        var go = Instantiate(knifesCollisionPrefab, position, Quaternion.identity, transform);
        _knifeCollisionCount.Add(go);
    }
    private void Awake()
    {
        Current = this;
    }
}
