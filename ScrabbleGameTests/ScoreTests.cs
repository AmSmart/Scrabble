﻿using ScrabbleGame;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ScrabbleGameTests
{
    public class ScoreTests : TestBase
    {
        [Fact]
        public void BasicScoreTest()
        {
            Game game = GetGameWithSingleVerticalWord();
            Move move = new Move(game, new List<TilePlacement>
            {
                new TilePlacement(0, 4, 'C'),
                new TilePlacement(1, 4, 'L'),
                new TilePlacement(3, 4, 'F')
            });

            int actual = move.GetScore(out string _);
            int expected = 9; // CLEF

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void AdjacentScoreTest()
        {
            Game game = GetGameWithSingleVerticalWord();
            Move move = new Move(game, new List<TilePlacement>
            {
                new TilePlacement(1, 2, 'L'),
                new TilePlacement(1, 3, 'A'),
                new TilePlacement(1, 4, 'P')
            });

            int actual = move.GetScore(out string _);
            int expected = 5  // LAP
                         + 2  // AT
                         + 4; // PE

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void WordMultiplerTest()
        {
            Game game = GetGameWithSingleVerticalWord();
            Move move = new Move(game, new List<TilePlacement>
            {
                new TilePlacement(1, 1, 'F'),
                new TilePlacement(1, 2, 'L'),
                new TilePlacement(1, 3, 'A'),
                new TilePlacement(1, 4, 'P')
            });

            int actual = move.GetScore(out string _);
            int expected = 18 // FLAP with double word under the F
                         + 2  // AT
                         + 4; // PE

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void LetterMultiplerTest()
        {
            Game game = GetGameWithSingleVerticalWord();
            Move move = new Move(game, new List<TilePlacement>
            {
                new TilePlacement(1, 1, 'F'),
                new TilePlacement(1, 2, 'L'),
                new TilePlacement(1, 3, 'A'),
                new TilePlacement(1, 4, 'P'),
                new TilePlacement(1, 5, 'S')
            });

            int actual = move.GetScore(out string _);
            int expected = 24 // FLAPS with double word under the F, tripple letter under the S
                         + 2  // AT
                         + 4  // PE
                         + 4; // SS with tripple letter under the first S

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void WordMultiplierMultipleWordsTest()
        {
            Game game = GetGameWithSingleVerticalWord();
            Move move = new Move(game, new List<TilePlacement>
            {
                new TilePlacement(1, 2, 'L'),
                new TilePlacement(2, 2, 'A'),
                new TilePlacement(3, 2, 'P')
            });

            int actual = move.GetScore(out string _);
            int expected = 10  // LAPS with double word under the A
                         + 10; // ATEST with double word under the A

            Assert.Equal(expected, actual);
        }
    }
}