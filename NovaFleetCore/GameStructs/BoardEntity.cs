using System;
using System.Collections.Generic;
using System.Text;
using NovaFleetCore.Structures;

namespace NovaFleetCore.GameStructs
{
    public abstract class BoardEntity
    {
        public readonly bool obstructsTileMovement;
        public readonly bool obstructsTileAttacks;
    }

    public class StaticObstacleEntity : BoardEntity
    {

    }

    public class PlasmaObstacleEntity : BoardEntity
    {

    }

    public class PlayerShipEntity : BoardEntity, IDamagable, IMoveable
    {
        const int MaxHealth = 5;
        public int Health { get; private set; }

        public int Damage(int damage)
        {
            int startingHealth = Health;

            Health -= damage;

            if(Health <= 0)
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

        public void Push(Direction direction)
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
        void Push(Direction direction);
    }
}
