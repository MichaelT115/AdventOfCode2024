namespace Day15_WarehouseWoes;

internal record struct Position(int X, int Y)
{
    public static Position operator +(Position a, Position b)
        => new(a.X + b.X, a.Y + b.Y);
}