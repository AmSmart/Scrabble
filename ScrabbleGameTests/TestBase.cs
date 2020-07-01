﻿using ScrabbleData;
using ScrabbleGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScrabbleGameTests
{
    public abstract class TestBase
    {
        protected Game GetGameWithEmptyBoard()
        {
            string board = new string(' ', Game.BOARD_HEIGHT * Game.BOARD_WIDTH);
            GameData data = new GameData { Board = board };
            return new Game(data);
        }

        protected Game GetGameWithSingleVerticalWord()
        {
            string board = string.Concat(Enumerable.Repeat("               ", 3)) +
                           "  T            " + // TEST starts at position 2, 3
                           "  E            " +
                           "  S            " +
                           "  T            " + // and extends to position 2, 6
                           string.Concat(Enumerable.Repeat("               ", 8));
            GameData data = new GameData { Board = board };
            return new Game(data);
        }

        protected Game GetGameWithSingleVerticalWordAtTop()
        {
            string board = "  T            " + // TEST starts at position 2, 0
                           "  E            " +
                           "  S            " +
                           "  T            " + // and extends to position 2, 3
                           string.Concat(Enumerable.Repeat("               ", 11));
            GameData data = new GameData { Board = board };
            return new Game(data);
        }

        protected Game GetGameWithSingleVerticalWordAtBottom()
        {
            string board = string.Concat(Enumerable.Repeat("               ", 11)) +
                           "  T            " + // TEST starts at position 2, 11
                           "  E            " +
                           "  S            " +
                           "  T            ";  // and extends to position 2, 14
            GameData data = new GameData { Board = board };
            return new Game(data);
        }
    }
}