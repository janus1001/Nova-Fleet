using System;
using System.Collections.Generic;
using System.Text;

namespace NovaFleetCore.GameStructs
{
    public class Board
    {
        public int MapWidth { get; private set; }
        public int MapHeight { get; private set; }

        private TileEntry[,] tiles;

        /// <summary>
        /// Returns a tile with the given coordinates. Returns null if the tile is not valid.
        /// </summary>
        public TileEntry GetTile(int q, int r)
        {
            // Check for array bounds
            if (q < 0 || q >= MapWidth || r < 0 || r >= MapHeight)
                return null;

            // Check if the tile isn't empty
            if(!tiles[r,q].exists)
                return null;

            return tiles[r, q];
        }
        /// <summary>
        /// Returns a tile with the given coordinates. Returns null if the tile is not valid.
        /// </summary>
        public TileEntry GetTile(Hex pos)
        {
            return GetTile(pos.q, pos.r);
        }

        /// <summary>
        /// Checks if the tile with coordinates exists.
        /// </summary>
        private bool CheckForValidTile(int q, int r)
        {
            return q >= 0 && q < MapWidth && r >= 0 && r < MapHeight && tiles[q, r].exists;
        }

        public Board(int width, int height)
        {
            tiles = HexagonMap(width, height);
        }

        /// <summary>
        /// Returns a default, hexagonal map
        /// </summary>
        public TileEntry[,] HexagonMap(int edgeWidth, int edgeHeight)
        {
            MapWidth = edgeWidth + edgeHeight - 1;
            MapHeight = edgeHeight * 2 - 1;

            TileEntry[,] tileMapData = new TileEntry[MapWidth, MapHeight];

            // Initalise the tiles
            for (int q = 0; q < MapWidth; q++)
            {
                for (int r = 0; r < MapHeight; r++)
                {
                    tileMapData[q, r] = new TileEntry(new Hex(q, r), false, this);
                }
            }

            // Set the hexagon to normal tile type
            for (int q = 0; q < MapHeight; q++)
            {
                int offset = MapHeight - q - 1;

                for (int r = 0; r < MapWidth; r++)
                {
                    if (r + offset >= MapWidth || r + offset < 0)
                        continue;

                    tileMapData[r + offset, q] = new TileEntry(new Hex(q, r), true, this);
                }
            }

            return tileMapData;
        }

        public override string ToString()
        {
            return $"{MapHeight} x {MapWidth}";
        }
    }
}