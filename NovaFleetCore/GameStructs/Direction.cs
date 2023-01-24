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
}
