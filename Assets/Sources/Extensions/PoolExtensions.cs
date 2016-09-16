using Entitas;
using UnityEngine;

public static class PoolExtensions {

    static readonly string[] _pieces = {
        Res.Piece0,
        Res.Piece1,
        Res.Piece2,
        Res.Piece3,
        Res.Piece4,
        Res.Piece5
    };

    public static bool isInGameboard(this Pool _pool, int x, int y)
    {
        return (x >= 0 && x < _pool.gameBoard.columns)
            && (y >= 0 && y < _pool.gameBoard.rows);
    }

    public static Entity CreateRandomPiece(this Pool pool, int x, int y) {
        return pool.CreateEntity()
            .IsGameBoardElement(true)
            .AddPosition(x, y)
            .IsMovable(true)
            .IsInteractive(true)
            .AddResource(_pieces[Random.Range(0, _pieces.Length)])
            .IsChainable(true);
    }

    public static Entity CreateBlocker(this Pool pool, int x, int y) {
        return pool.CreateEntity()
            .IsGameBoardElement(true)
            .AddPosition(x, y)
            .AddResource(Res.Blocker);
    }

    // Note: нужна цепная реакция так что подписываться нужно не его убивание, а не клик
    // тоже самое и для обычной - просто добаивть чтобы она сама на себя не зацикливалась.
    // а нет, те не должны цепную реакцию делать. если бомба взорвет обычную - то ей пофиг должно быть (не должна тотже цвет вокруг себя убирать)
    public static Entity CreateBomb(this Pool pool, int x, int y)
    {
        return pool.CreateEntity()
            .IsGameBoardElement(true)
            .AddPosition(x, y)
            .AddResource(Res.Bomb)
            .IsMovable(true)
            .IsInteractive(true)
            .IsBomb(true);
    }

    public static Entity CreateLaser(this Pool pool, int x, int y)
    {
        return pool.CreateEntity()
            .IsGameBoardElement(true)
            .AddPosition(x, y)
            .AddResource(Res.Laser)
            .IsMovable(true)
            .IsInteractive(true)
            .IsLaser(true);
    }
}

