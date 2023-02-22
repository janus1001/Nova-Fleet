namespace NovaFleetCore.GameStructs
{
    // 00 00 = -1r -1q
    // 01 00 =  0r -1q
    // 10 10 = +1r +1q
    // etc
    public enum Direction
    {
        NE = 0b0110,
        E = 0b1001,
        SE = 0b1000,
        SW = 0b0100,
        W = 0b0001,
        NW = 0b0010
    }

    public static class DirectionExtensions
    {
        public static Hex Multiply(this Direction direction, int magnitude)
        {
            Hex hex = new Hex();
            for (int i = 0; i < magnitude; i++)
            {
                hex += direction;
            }
            return hex;
        }
    }
}
