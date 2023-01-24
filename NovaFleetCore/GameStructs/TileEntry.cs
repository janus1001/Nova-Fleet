using System;
using System.Collections.Generic;
using System.Text;

namespace NovaFleetCore.GameStructs
{
    struct TileEntry
    {
        public readonly bool exists;

        public List<BoardEntity> entities;

        public bool IsWalkable()
        {
            foreach (var entity in entities)
            {
                if (entity.obstructsTileMovement)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
