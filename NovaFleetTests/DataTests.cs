using Microsoft.VisualStudio.TestTools.UnitTesting;
using NovaFleetCore.AbilitySystem;
using NovaFleetCore.GameStructs;
namespace NovaFleetTests
{
    [TestClass]
    public class DataTests
    {
        [TestMethod]
        [DataRow(4, 4, 4, 5, Direction.NE)]
        [DataRow(4, 4, 5, 4, Direction.E)]
        [DataRow(4, 4, 5, 3, Direction.SE)]
        [DataRow(4, 4, 4, 3, Direction.SW)]
        [DataRow(4, 4, 3, 4, Direction.W)]
        [DataRow(4, 4, 3, 5, Direction.NW)]
        public void DirectionCheckDifferentVariables(int h1x, int h1y, int h2x, int h2y, Direction dir)
        {
            Hex hex1 = new Hex(h1x, h1y);
            Hex hex2 = new Hex(h2x, h2y);
            Assert.IsTrue(Hex.GetRelativeDirection(hex1, hex2) == dir);
        }

        [TestMethod]
        [DataRow(Direction.E,   1,   1,  0)]
        [DataRow(Direction.E,   3,   3,  0)]
        [DataRow(Direction.NW,  2,  -2,  2)]
        [DataRow(Direction.SW,  5,   0, -5)]
        public void MultiplyDirection(Direction dir, int multitude, int q, int r)
        {
            Hex result = dir.Multiply(multitude);
            Assert.IsTrue(result.Equals(new Hex(q, r)));
        }

        [TestMethod]
        [DataRow(4, 5)]
        [DataRow(915, 2661)]
        [DataRow(-24, -14)]
        public void HexSerialisation(int hexX, int hexY)
        {
            Hex hex = new Hex(hexX, hexY);
            byte[] serialisedHex = hex.Serialize();

            Assert.IsTrue(hex.Equals(new Hex(serialisedHex)));
        }

        [TestMethod]
        [DataRow("Name:Blast Wave Artillery\nType:M\nCost:1\nDescription:Choose a tile in a line. Push away all adjacent units.\\n\\nThe blast tile must be between 2 to 5 tiles away from the attacking unit.\n{\n--Selector--\nTSArtillery\nTSRangeOutwards_6 -\nTSRangeInwards_2 -\n\n--Execute--\nESelectTile\nEMoveOrigin\nESelectArea_1\nEKnockback_1\n}")]
        [DataRow("Name:Boosters\nType:M\nCost:1\nDescription:Go forward up to three tiles.\n{\n--Selector--\nTSForwards ^\nTSForwards &^\nTSForwards &^\n\n--Execute--\nESelectTile\nEMoveToTile\n}")]
        public void LoadAbility(string abilityText)
        {
            ModuleCard ability = AbilityLoader.LoadAbility(abilityText);
            System.Console.WriteLine(ability);

            Assert.IsNotNull(ability);
        }

        [TestMethod]
        public void Test()
        {
            string input =
@"Name:Blast Wave Artillery
Type:M
Cost:1
Description:Choose a tile in a line. Push away all adjacent units.\n\nThe blast tile must be between 2 to 5 tiles away from the attacking unit.

--Selector--
TSArtillery
TSRangeOutwards_6 -
TSRangeInwards_2 -

--Execute--
ESelectTile
EMoveOrigin
ESelectArea_1
EKnockback_1";

            
        }
    }
}
