using System;
using System.Collections.Generic;
using System.Text;

namespace NovaFleetCore.GameStructs
{
    public class TileEntry
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

        internal TileEntry(bool inBounds)
        {
            exists = inBounds;

            if(exists)
                entities = new List<BoardEntity>();
        }
    }
}
