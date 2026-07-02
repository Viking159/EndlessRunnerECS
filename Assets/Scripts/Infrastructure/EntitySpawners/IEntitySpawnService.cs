using Leopotam.EcsLite;
using UnityEngine;

namespace EndlessRunnerECS.Infrastructure.EntitySpawners
{
    public interface IEntitySpawnService
    {
        int Spawn(EcsWorld world, Vector3 position);
    }
}
