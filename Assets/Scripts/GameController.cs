﻿using Entitas;
using UnityEngine;

public class GameController : MonoBehaviour {

    Systems _systems;

    void Start() {
        Random.seed = 42;

        _systems = createSystems(Pools.pool);
        _systems.Initialize();
    }

    void Update() {
        _systems.Execute();
    }

    Systems createSystems(Pool pool) {
        return new Feature("Systems")

            // Input
            .Add(pool.CreateSystem<ProcessNormalElementDestroy>())
            //.Add(pool.CreateSystem<ProcessInputSystem>())

            .Add(pool.CreateSystem<BombDestroySystem>())
            //.Add(pool.CreateSystem<LaserComponent>())

            // Update
            .Add(pool.CreateSystem<CreateGameBoardCacheSystem>())
            .Add(pool.CreateSystem<GameBoardSystem>())
            .Add(pool.CreateSystem<FallSystem>())
            .Add(pool.CreateSystem<FillSystem>())
            .Add(pool.CreateSystem<ScoreSystem>())

            // Render
            .Add(pool.CreateSystem<RemoveViewSystem>())
            .Add(pool.CreateSystem<AddViewSystem>())
            .Add(pool.CreateSystem<RenderPositionSystem>())

            // Destroy
            .Add(pool.CreateSystem<DestroySystem>());
    }
}
