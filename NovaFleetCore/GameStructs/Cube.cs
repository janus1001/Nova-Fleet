using System;

namespace NovaFleetCore.GameStructs
{
    /// <summary>
    /// A representation of a Hexagon tile in Cubic coordinate system. Useful for rotations and reflections.
    /// </summary>
    public struct Cube
    {
        public int q, r, s;

        public Cube(int q, int r, int s)
        {
            this.q = q;
            this.r = r;
            this.s = s;

            if (!VerifyCube())
                throw new Exception("Tried to create a degenerate Cube: " + this);
        }

        public Cube(Hex hex)
        {
            this.q = hex.q;
            this.r = hex.r;
            this.s = -(q + r);
        }

        public static Cube operator +(Cube a, Cube b)
        {
            return new Cube(a.q + b.q, a.r + b.r, a.s + b.s);
        }

        public static Cube operator -(Cube a, Cube b)
        {
            return new Cube(a.q - b.q, a.r - b.r, a.s - b.s);
        }

        public Cube Rotate60Clockwise()
        {
            return new Cube(-r, -s, -q);
        }

        public Cube Rotate60CounterClockwise()
        {
            return new Cube(-s, -q, -r);
        }

        public static Cube Round(CubeFloat cubeFloat)
        {
            return Round(cubeFloat.q, cubeFloat.r, cubeFloat.s);
        }

        public static Cube Round(float q, float r)
        {
            float s = -q - r;
            return Round(q, r, s);
        }

        public static Cube Round(float q, float r, float s)
        {
            // Offsets are added to "nudge" the GetLine in one direction to avoid landing on side boundaries.
            q += 0.001f;
            r += 0.001f;
            s += 0.001f;

            int intQ = (int)Math.Round(q, 0);
            int intR = (int)Math.Round(r, 0);
            int intS = (int)Math.Round(s, 0);

            float q_diff = Math.Abs(intR - r);
            float r_diff = Math.Abs(intQ - q);
            float s_diff = Math.Abs(intS - s);

            if (q_diff > r_diff && q_diff > s_diff)
                intR = -intQ - intS;
            else if (r_diff > s_diff)
                intQ = -intR - intS;
            else
                intS = -intR - intQ;

            return new Cube(intQ, intR, intS);
        }

        public static Cube ReflectR(Cube cube)
        {
            return new Cube(cube.q, cube.s, cube.r);
        }

        public static Cube ReflectQ(Cube cube)
        {
            return new Cube(cube.s, cube.r, cube.q);
        }

        public static Cube ReflectS(Cube cube)
        {
            return new Cube(cube.r, cube.q, cube.s);
        }

        public static Direction GetRelativeDirection(Cube originCube, Cube targetCube)
        {
            Cube relativeCoordinates = targetCube - originCube;
            int sq = Math.Abs(relativeCoordinates.s - relativeCoordinates.q);
            int rs = Math.Abs(relativeCoordinates.r - relativeCoordinates.s);
            int qr = Math.Abs(relativeCoordinates.q - relativeCoordinates.r);

            if (sq > rs && sq > qr)
            {
                if (relativeCoordinates.q > 0)
                    return Direction.E;
                return Direction.W;
            }
            else if (rs > qr)
            {
                if (relativeCoordinates.r > 0)
                    return Direction.NE;
                return Direction.SW;
            }
            else
            {
                if (relativeCoordinates.q - relativeCoordinates.r > 0)
                    return Direction.SE;
                return Direction.NW;
            }
        }

        public static implicit operator Cube(Hex hex)
        {
            int s = -hex.q - hex.r;

            return new Cube(hex.q, hex.r, s);
        }

        private bool VerifyCube()
        {
            if (q + r + s != 0)
                return false;
            return true;
        }

        public override string ToString()
        {
            return $"{{{q}, {r}, {s}}}";
        }
    }
}