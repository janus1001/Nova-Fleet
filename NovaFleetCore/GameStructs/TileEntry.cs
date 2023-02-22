using System;
using System.Collections.Generic;
using System.Text;

namespace NovaFleetCore.GameStructs
{
    public class TileEntry
    {
        public Hex TileLocation { get; private set; }
        public Board ParentBoard { get; private set; }
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

        public TileEntry(Hex position, bool inBounds, Board parentBoard)
        {
            TileLocation = position;
            ParentBoard = parentBoard;

            exists = inBounds;

            if(exists)
                entities = new List<BoardEntity>();
        }

        public override string ToString()
        {
            return exists ? $"{entities.Count}" : "-";
        }
    }
}
