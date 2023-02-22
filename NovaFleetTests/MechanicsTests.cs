using Microsoft.VisualStudio.TestTools.UnitTesting;
using NovaFleetCore.GameStructs;

namespace NovaFleetTests
{
    [TestClass]
    public class MechanicsTests
    {
        [TestMethod]
        public void MoveEntity()
        {
            /*var board = new Board(x, y);
            var tile = board.GetTile(x, y);
            BoardEntity boardEntity = new StaticObstacleEntity();
            tile.entities.Add(boardEntity);

            boardEntity.Move();

            Assert.IsTrue(tile.entities.Count > 0);*/
            throw new System.NotImplementedException();
        }
        
        [TestMethod]
        public void PushEntity()
        {
            var board = new Board(5, 5);
            var tile = board.GetTile(5, 5);
            BoardEntity boardEntity = new PushableObstacleEntity(tile);
            tile.entities.Add(boardEntity);

            // Pre-movement
            Assert.IsTrue(tile.entities.Count > 0);

            // Movement
            boardEntity.Push(Direction.E, 1);

            // Post movement
            TileEntry newTile = board.GetTile(tile.TileLocation + Direction.E);
            Assert.IsTrue(tile.entities.Count == 0);
            Assert.IsTrue(newTile.entities.Count > 0);
        }

        [TestMethod]
        public void DamageDamageableEntity()
        {
            Board board = new Board(1, 1);
            TileEntry tileEntry = board.GetTile(0, 0);

            BoardEntity boardEntity = new PlayerShipEntity(tileEntry);

            PlayerShipEntity playerShipEntity = boardEntity as PlayerShipEntity;

            int previousHealth = playerShipEntity.Health;

            playerShipEntity.Damage(1);

            Assert.IsTrue(playerShipEntity.Health < previousHealth);
        }

        [TestMethod]
        public void HealEntity()
        {
            Board board = new Board(1, 1);
            TileEntry tileEntry = board.GetTile(0, 0);

            BoardEntity boardEntity = new PlayerShipEntity(tileEntry);

            PlayerShipEntity playerShipEntity = boardEntity as PlayerShipEntity;

            playerShipEntity.Damage(1);

            int previousHealth = playerShipEntity.Health;

            playerShipEntity.Heal(1);

            Assert.IsTrue(playerShipEntity.Health > previousHealth);
        }
    }
}
