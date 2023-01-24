// This is a helper class for Rounding and Lerping operations.

namespace NovaFleetCore.GameStructs
{
    public struct CubeFloat
    {
        public float r, q, s;

        public CubeFloat(float r, float q, float s)
        {
            this.r = r;
            this.q = q;
            this.s = s;
        }

        public static CubeFloat Lerp(Cube a, Cube b, float delta)
        {
            return new CubeFloat(Lerp(a.r, b.r, delta), Lerp(a.q, b.q, delta), Lerp(a.s, b.s, delta));
        }

        static float Lerp(float a, float b, float delta)
        {
            return a + (b - a) * delta;
        }
    }
}