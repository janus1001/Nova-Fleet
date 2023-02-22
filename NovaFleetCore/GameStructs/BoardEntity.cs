using System;
using System.Collections.Generic;
using System.Text;
using NovaFleetCore.Structures;

namespace NovaFleetCore.GameStructs
{
    public abstract class BoardEntity
    {
        public TileEntry currentTile;

        public readonly bool obstructsTileMovement;
        public readonly bool obstructsTileAttacks;

        public void Push(Direction direction, int distance)
        {
            TileEntry targetTile = currentTile;

            for (int i = 0; i < distance; i++)
            {
                Hex targetLocation = targetTile.TileLocation + direction;
                TileEntry checkedTile = currentTile.ParentBoard.GetTile(targetLocation);

                if (!checkedTile.IsWalkable())
                {
                    break;
                }
                targetTile = checkedTile;
            }

            if (targetTile != currentTile)
            {
                ChangeLocation(targetTile);
            }
        }

        void ChangeLocation(TileEntry newTile)
        {
            currentTile.entities.Remove(this);
            newTile.entities.Add(this);
        }

        public BoardEntity(TileEntry newCurrentTile)
        {
            currentTile = newCurrentTile;
        }
    }

    public class StationaryObstacleEntity : BoardEntity
    {
        public StationaryObstacleEntity(TileEntry newCurrentTile) : base(newCurrentTile)
        {
        }
    }

    public class PlasmaObstacleEntity : BoardEntity
    {
        public PlasmaObstacleEntity(TileEntry newCurrentTile) : base(newCurrentTile)
        {
        }
    }

    public class PushableObstacleEntity : BoardEntity
    {
        public PushableObstacleEntity(TileEntry newCurrentTile) : base(newCurrentTile)
        {
        }
    }

    public class PlayerShipEntity : BoardEntity, IDamagable, IMoveable
    {
        public PlayerShipEntity(TileEntry newCurrentTile) : base(newCurrentTile)
        {
        }

        const int MaxHealth = 5;
        public int Health { get; private set; }

        public int Damage(int damage)
        {
            int startingHealth = Health;

            Health -= damage;

            if (Health <= 0)
            {
                // Destroy
            }

            return startingHealth - Health;
        }

        public int Heal(int heal)
        {
            int startingHealth = Health;

            Health += heal;

            if (Health > MaxHealth)
            {
                Health = MaxHealth;
            }

            return Health - startingHealth;
        }

        public void Move(TileEntry targetTile)
        {
            throw new NotImplementedException();
        }
    }

    // Entity interfaces

    public interface IDamagable
    {
        int Damage(int damage);
        int Heal(int heal);
    }

    public interface IMoveable
    {
        void Move(TileEntry targetTile);
    }
}
