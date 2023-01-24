using System;
using System.Collections.Generic;
using System.Text;

namespace NovaFleetCore.GameStructs
{
    class Board
    {
        public int MapWidth { get; private set; }
        public int MapHeight { get; private set; }

        public TileEntry[,] tiles;

        private bool CheckForValidTile(int q, int r)
        {
            return q >= 0 && q < MapWidth && r >= 0 && r < MapHeight && tiles[q, r].exists;
        }
    }
}