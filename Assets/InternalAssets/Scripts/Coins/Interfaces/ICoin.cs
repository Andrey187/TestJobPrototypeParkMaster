using System;
using UnityEngine;

public interface ICoin
{
    public ParticleSystem ParticleSystem { get; }
    public MeshRenderer MeshRenderer { get; }
    public MeshCollider MeshCollider { get; }

}
