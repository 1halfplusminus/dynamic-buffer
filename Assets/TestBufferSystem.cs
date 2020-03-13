using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using static Unity.Mathematics.math;

public class TestBufferSystem : JobComponentSystem
{
    [BurstCompile]
    struct TestBufferSystemJob : IJobForEach_B<IntBufferElement>
    {
        public void Execute(DynamicBuffer<IntBufferElement> bufferElements)
        {
            for (int i = 0; i < bufferElements.Length; i++)
            {
                IntBufferElement intBufferElement = bufferElements[i];
                intBufferElement.Value++;
                bufferElements[i] = intBufferElement;
            }
        }
    }

    protected override JobHandle OnUpdate(JobHandle inputDependencies)
    {
        var job = new TestBufferSystemJob();
        return job.Schedule(this, inputDependencies);
    }
}