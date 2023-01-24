using System;
using System.Collections.Generic;

namespace NovaFleetCore.GameStructs
{
    [Serializable]
    public struct Hex : IEquatable<Hex>
    {
        public int q, r;

        public Hex(int q, int r)
        {
            this.q = q;
            this.r = r;
        }

        private Hex(Direction dir)
        {
            int h = (int)dir;
            q = ((h & 0b1100) >> 2) - 1;
            r = (h & 0b0011) - 1;
        }

        public Hex(Cube cube)
        {
            q = cube.q;
            r = cube.r;
        }

        public static Hex operator +(Hex a, Hex b)
        {
            return new Hex(a.q + b.q, a.r + b.r);
        }

        public static Hex operator +(Hex hex, Direction direction) // Move hex by a direction.
        {
            int h = (int)direction;
            int dr = (h & 0b1100) >> 2;
            int dq = (h & 0b0011);

            hex.q += --dr;
            hex.r += --dq;

            return hex;
        }

        public static Hex operator -(Hex a, Hex b)
        {
            return new Hex(a.q - b.q, a.r - b.r);
        }

        public static Hex operator *(Hex a, int b)
        {
            return new Hex(a.q * b, a.r * b);
        }

        public static implicit operator Hex(Cube cube)
        {
            return new Hex(cube.q, cube.r);
        }

        public Hex Rotate60Clockwise(Hex center)
        {
            Hex tempHex = this - center;
            Cube tempCube = new Cube(tempHex);
            tempCube = tempCube.Rotate60Clockwise();
            tempHex = tempCube;
            tempHex += center;
            return tempHex;
        }

        public Hex Rotate60CounterClockwise(Hex center)
        {
            Hex tempHex = this - center;
            Cube tempCube = new Cube(tempHex);
            tempCube = tempCube.Rotate60CounterClockwise();
            tempHex = tempCube;
            tempHex += center;
            return tempHex;
        }

        public static Hex Round(float q, float r)
        {
            Cube tempCube = Cube.Round(q, r);
            Hex tempHex = tempCube;
            return tempHex;
        }

        public static Hex ReflectR(Hex hex)
        {
            return Cube.ReflectR(hex);
        }

        public static Hex ReflectQ(Hex hex)
        {
            return Cube.ReflectQ(hex);
        }

        public static Hex ReflectS(Hex hex)
        {
            return Cube.ReflectS(hex);
        }


        //////////////////////////////////////////////////////
        //  GET SHAPES
        //////////////////////////////////////////////////////

        /// <summary>
        /// Get a ring of width 1 around the tile at a distance.
        /// </summary>
        public static HashSet<Hex> GetRing(Hex centerTile, int distance)
        {
            HashSet<Hex> selectedTiles = new HashSet<Hex>();

            if (distance < 1)
            {
                selectedTiles.Add(centerTile);
                return selectedTiles;
            }

            Hex[] bottomSide = new Hex[distance];

            for (int i = 0; i < distance; i++) // Create bottom line for rotating
            {
                bottomSide[i] = centerTile + SouthWest * distance + East * i;
            }

            for (int i = 0; i < 6; i++) // Rotate the bottom line 6 times and add it to the tiles
            {
                for (int j = 0; j < bottomSide.Length; j++)
                {
                    bottomSide[j] = bottomSide[j].Rotate60Clockwise(centerTile);
                    selectedTiles.Add(bottomSide[j]);
                }
            }

            return selectedTiles;
        }

        /// <summary>
        /// Get an area around the tile at a distance. Radius of 2 is maximum distance of 1.
        /// </summary>
        public static HashSet<Hex> GetArea(Hex centerTile, int radius)
        {
            HashSet<Hex> result = new HashSet<Hex> { centerTile };

            for (int i = 1; i < radius; i++)
            {
                result.UnionWith(GetRing(centerTile, i));
            }

            return result;
        }

        /// <summary>
        /// Get all tiles between and including two selected tiles.
        /// </summary>
        public static List<Hex> GetLine(Hex a, Hex b)
        {
            List<Hex> result = new List<Hex>();

            int distance = Hex.Distance(a, b);
            if (distance == 0)
            {
                result.Add(a);
                return result;
            }

            for (int i = 0; i <= distance; i++)
            {
                Cube cube = Cube.Round(CubeFloat.Lerp(a, b, 1.0f / distance * i)); // Sampled, rounded point
                result.Add(cube);
            }

            return result;
        }

        public static Hex NorthEast { get; } = new Hex(Direction.NE);
        public static Hex East { get; } = new Hex(Direction.E);
        public static Hex SouthEast { get; } = new Hex(Direction.SE);
        public static Hex SouthWest { get; } = new Hex(Direction.SW);
        public static Hex West { get; } = new Hex(Direction.W);
        public static Hex NorthWest { get; } = new Hex(Direction.NW);

        public static int Distance(Hex a, Hex b)
        {
            return (Math.Abs(a.q - b.q) + Math.Abs(a.q + a.r - b.q - b.r) + Math.Abs(a.r - b.r)) / 2;
        }

        public static Direction GetRelativeDirection(Hex originHex, Hex targetHex)
        {
            return Cube.GetRelativeDirection(originHex, targetHex);
        }

        public override string ToString()
        {
            return $"{{{q}, {r}}}";
        }

        public override bool Equals(object obj)
        {
            return obj is Hex hex && Equals(hex);
        }

        public bool Equals(Hex other)
        {
            return q == other.q &&
                   r == other.r;
        }

        public override int GetHashCode()
        {
            int hashCode = 1939305425;
            hashCode = hashCode * -1521134295 + q.GetHashCode();
            hashCode = hashCode * -1521134295 + r.GetHashCode();
            return hashCode;
        }

        /// <summary>
        /// Serialization for sending over network.
        /// </summary>
        public byte[] Serialize()
        {
            long serialized = (r & 0xffffffff) << (sizeof(int) * 8);
            serialized |= (long)q & 0xffffffff;

            return BitConverter.GetBytes(serialized);
        }

        /// <summary>
        /// Deserialization for recieving from network.
        /// </summary>
        public Hex(byte[] src)
        {
            long deserialised = BitConverter.ToInt64(src, 0);
            r = (int)(deserialised >> (sizeof(int) * 8));
            q = (int)deserialised;
        }
    }
}