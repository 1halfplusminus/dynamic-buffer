using System;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;

[Serializable]
public struct Spawn : IComponentData
{
    public Entity prefab;
}
