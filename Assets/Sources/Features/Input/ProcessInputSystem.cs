using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class ProcessInputSystem : IReactiveSystem, ISetPool {
    public TriggerOnEvent trigger { get { return Matcher.Input.OnEntityAdded(); } }

    Pool _pool;

    public void SetPool(Pool pool) {
        _pool = pool;
    }

    public void Execute(List<Entity> entities) {
        Debug.Log("Process Input");

        var inputEntity = entities.SingleEntity();
        var input = inputEntity.input;

        if (isInGameboard(input.x, input.y)) {
            var e = _pool.gameBoardCache.grid[input.x, input.y];

            //e.resource.name

            // todo go recursive in all directions and mark e.isDestroy on all of the same color
            // либо это должна быть система котора€ подписана на убивание штучек конкретного типа - и котора€ после нажати€ и удалени€ первой сделает что-то в зависимости от типа
            // одна система обычна€ - удалит все воеруг такого же цвета.   input отношени€ не имеет же.  ак бы
            // IreactiveSystem &     public TriggerOnEvent trigger { get { return Matcher.GameBoardElement.OnEntityRemoved(); } }


            if (e != null && e.isInteractive) {
                e.isDestroy = true;
            }
        }

        _pool.DestroyEntity(inputEntity);
    }

    bool isInGameboard(int x, int y) {
        return (x >= 0 && x < _pool.gameBoard.columns)
            && (y >= 0 && y < _pool.gameBoard.rows);
    }
}

