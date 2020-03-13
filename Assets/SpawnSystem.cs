using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using static Unity.Mathematics.math;

public class SpawnSystem : JobComponentSystem
{
    EndSimulationEntityCommandBufferSystem endSimulationEntityCommandBufferSystem;

    [BurstCompile]
    struct SpawnSystemJob : IJobForEachWithEntity<Spawn>
    {
        public EntityCommandBuffer.Concurrent concurrent;
        public void Execute(Entity entity, int index, [ReadOnly] ref Spawn spawn)
        {
            concurrent.Instantiate(index, spawn.prefab);
            concurrent.DestroyEntity(index, entity);
        }
    }

    protected override JobHandle OnUpdate(JobHandle inputDependencies)
    {
        var ecb = endSimulationEntityCommandBufferSystem.CreateCommandBuffer().ToConcurrent();
        var job = new SpawnSystemJob() { concurrent = ecb };
        var jobHandle = job.Schedule(this, inputDependencies);
        endSimulationEntityCommandBufferSystem.AddJobHandleForProducer(jobHandle);
        return jobHandle;
    }



    protected override void OnCreate()
    {
        base.OnCreate();
        endSimulationEntityCommandBufferSystem = World
            .GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
    }

    protected override void OnStartRunning()
    {
        base.OnStartRunning();
    }

    protected override void OnStopRunning()
    {
        base.OnStopRunning();
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
    }
}