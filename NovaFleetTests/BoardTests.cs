using Microsoft.VisualStudio.TestTools.UnitTesting;
using NovaFleetCore.GameStructs;

namespace NovaFleetTests
{
    [TestClass]
    public class BoardTests
    {
        [TestMethod]
        [DataRow(10, 10)]
        public void BoardCreateTest(int x, int y)
        {
            var board = new Board(x, y);
            Assert.IsNotNull(board);
        }

        [TestMethod]
        [DataRow(10, 10)]
        public void BoardAddObstacleTest(int x, int y)
        {
            var board = new Board(x, y);
            var tile = board.GetTile(x, y);
            BoardEntity boardEntity = new StaticObstacleEntity();
            tile.entities.Add(boardEntity);

            Assert.IsTrue(tile.entities.Count > 0);
        }
    }
}
