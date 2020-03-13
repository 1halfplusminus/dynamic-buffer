using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    private BlobAssetStore blobAssetStore;
    [SerializeField] GameObject playerPrefab;
    [SerializeField] GameObject projectilPrefab;
    // Start is called before the first frame update
    void Start()
    {
        blobAssetStore = new BlobAssetStore();

        var settings = GameObjectConversionSettings.FromWorld(World.DefaultGameObjectInjectionWorld, blobAssetStore);
        InitPlayer(settings);
    }

    void InitPlayer(GameObjectConversionSettings settings)
    {
        var convertedTargetPrefab = GameObjectConversionUtility.ConvertGameObjectHierarchy(playerPrefab, settings);
        EntityManager entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
        entityManager.SetName(convertedTargetPrefab, "Player prefab");

        var spawn = entityManager.CreateEntity(typeof(Spawn));
        entityManager.AddComponentData(spawn, new Spawn() { prefab = convertedTargetPrefab });
        /*     var entity = entityManager.Instantiate(convertedTargetPrefab);
            entityManager.SetName(entity, "Player");
            entityManager.SetComponentData(entity, new Translation() { Value = float3.zero }); */
    }
    // Update is called once per frame
    void Update()
    {

    }

    private void OnDestroy()
    {
        if (blobAssetStore != null) { blobAssetStore.Dispose(); }
    }
}
