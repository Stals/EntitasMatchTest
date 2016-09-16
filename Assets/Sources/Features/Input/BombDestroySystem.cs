using System;
using System.Collections.Generic;
using Entitas;
using UnityEngine;


//IClearReactiveSystem не допустит вызов системы самой на себя
//IEnsureComponents сделает что уже удаленные не приходят (если тебе нужны только живиые тоесть)
public class BombDestroySystem : IReactiveSystem, ISetPool, IEnsureComponents
{

    public TriggerOnEvent trigger { get { return Matcher.AllOf(Matcher.Bomb, Matcher.Destroy).OnEntityAdded(); } }

    public IMatcher ensureComponents
    {
        get
        {
            return Matcher.AllOf(Matcher.Bomb, Matcher.Destroy);
        }
    }

    //public TriggerOnEvent trigger { get { return Matcher.Input.OnEntityAdded(); } }


    Pool _pool;

    public void SetPool(Pool pool)
    {
        _pool = pool;
    }

    // TODO - эту сучка не чейнится
    public void Execute(List<Entity> entities)
    {
        foreach (var e in entities)
        {
            if (!e.isBomb) continue;
            if (e == null || e.isInteractive == false) continue;


            int x = e.position.x;
            int y = e.position.y;

            removeIfPresent(x, y);
            removeIfPresent(x, y - 1);
            removeIfPresent(x, y + 1);

            removeIfPresent(x + 1, y);
            removeIfPresent(x + 1, y + 1);
            removeIfPresent(x + 1, y - 1);

            removeIfPresent(x - 1, y);
            removeIfPresent(x - 1, y + 1);
            removeIfPresent(x - 1, y - 1);

        }
    }

    private void removeIfPresent(int x, int y)
    {
        if (_pool.isInGameboard(x, y)) {
            var e = _pool.gameBoardCache.grid[x, y];
            if (e != null)
            {
                e.isDestroy = true;
            }
        }
    }
}

