using System;
using UnityEngine;

public class Coin : MonoBehaviour, ICoin
{
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private MeshRenderer _meshRenderer;
    [SerializeField] private MeshCollider _meshCollider;

    public ParticleSystem ParticleSystem => _particleSystem;

    public MeshRenderer MeshRenderer => _meshRenderer;

    public MeshCollider MeshCollider => _meshCollider;
}
