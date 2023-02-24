using System;
using System.Collections.Generic;
using System.Text;

namespace NovaFleetCore.GameStructs
{
    public class MatchState
    {
        public List<Player> players;
        public Board board;

        public MatchState()
        {
            players = new List<Player> { new Player(), new Player() };
            board = new Board(5, 5);
        }
    }
}
