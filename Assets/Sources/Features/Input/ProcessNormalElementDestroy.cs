using System;
using System.Collections.Generic;
using Entitas;
using UnityEngine;


//IClearReactiveSystem не допустит вызов системы самой на себ€
//IEnsureComponents сделает что уже удаленные не приход€т (если тебе нужны только живиые тоесть)
public class ProcessNormalElementDestroy : IReactiveSystem, ISetPool
{

    //    public TriggerOnEvent trigger { get { return Matcher.AllOf(Matcher.Destroy).OnEntityAdded(); } }
    public TriggerOnEvent trigger { get { return Matcher.Input.OnEntityAdded(); } }


    Pool _pool;

    public void SetPool(Pool pool)
    {
        _pool = pool;
    }

    void removeSameColorOnWholeBoard(List<Entity> entities) {
        foreach (var removedEntity in entities)
        {
            string removedResource = removedEntity.resource.name;

            for (int x = 0; x < _pool.gameBoard.columns; ++x)
            {
                for (int y = 0; y < _pool.gameBoard.rows; ++y)
                {
                    var e = _pool.gameBoardCache.grid[x, y];
                    if (e.resource.name == removedResource)
                    {
                        e.isDestroy = true;
                    }
                }
            }
        }
    }

    private void checkCell(string removedResource, int x, int y)
    {
        // stop recursion for this conditions
        if (!isInGameboard(x, y)) return;

        var e = _pool.gameBoardCache.grid[x, y];        
        if (e == null || e.isDestroy) return;

        // found new one - do the logic
        if (e.resource.name == removedResource)
        {
            e.isDestroy = true;
        }
        else {
            return;
        }

        // Diagonals do not work
        checkCell(removedResource, x + 1, y);
        checkCell(removedResource, x - 1, y);
        checkCell(removedResource, x, y + 1);
        checkCell(removedResource, x, y - 1);
    }

    void removeConnectedColor(Entity removedEntity)
    {
        //foreach (var removedEntity in entities)
        //{
            string removedResource = removedEntity.resource.name;

            int x = removedEntity.position.x;
            int y = removedEntity.position.y;
            // Diagonals do not work
            checkCell(removedResource, x + 1, y);
            checkCell(removedResource, x - 1, y);
            checkCell(removedResource, x, y + 1);
            checkCell(removedResource, x, y - 1);
        //}
    }

    public void Execute(List<Entity> entities)
    {
        var inputEntity = entities.SingleEntity();
        var input = inputEntity.input;

        if (isInGameboard(input.x, input.y))
        {
            var e = _pool.gameBoardCache.grid[input.x, input.y];

            // TODO fix - doesn't work
            removeConnectedColor(e);
        }
        

        //removeConnectedColor(entities);
        return;

        // Ќе идти обратно где уже поставлен isDestroy = true

        /*
         var input = inputEntity.input;

         if (isInGameboard(input.x, input.y))
         {
             var e = _pool.gameBoardCache.grid[input.x, input.y];

             //e.resource.name

             // todo go recursive in all directions and mark e.isDestroy on all of the same color
             // либо это должна быть система котора€ подписана на убивание штучек конкретного типа - и котора€ после нажати€ и удалени€ первой сделает что-то в зависимости от типа
             // одна система обычна€ - удалит все воеруг такого же цвета.   input отношени€ не имеет же.  ак бы
             // IreactiveSystem &     public TriggerOnEvent trigger { get { return Matcher.GameBoardElement.OnEntityRemoved(); } }


             if (e != null && e.isInteractive)
             {
                 e.isDestroy = true;
             }
         }

         _pool.DestroyEntity(inputEntity);*/
        }



    
    bool isInGameboard(int x, int y)
    {
        return (x >= 0 && x < _pool.gameBoard.columns)
            && (y >= 0 && y < _pool.gameBoard.rows);
    }
}

