namespace Day21_KeypadConundrum;

public record struct IntVector2(int X, int Y)
{
    public static IntVector2 operator +(IntVector2 a, IntVector2 b)
        => new(a.X + b.X, a.Y + b.Y);

    public static IntVector2 operator -(IntVector2 a, IntVector2 b)
        => new(a.X - b.X, a.Y - b.Y);
}