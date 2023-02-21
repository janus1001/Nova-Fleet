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

        public TileEntry GetTile(int q, int r)
        {
            if (q < 0 || q >= MapWidth || r < 0 || r >= MapHeight)
                return null;

            if(tiles[r,q].exists)
                return tiles[r, q];

            return null;
        }

        private bool CheckForValidTile(int q, int r)
        {
            return q >= 0 && q < MapWidth && r >= 0 && r < MapHeight && tiles[q, r].exists;
        }

        public Board(int width, int height)
        {
            tiles = HexagonMap(width, height);
        }

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
                    tileMapData[q, r] = new TileEntry(false);
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

                    tileMapData[r + offset, q] = new TileEntry(true);
                }
            }

            return tileMapData;
        }
    }
}