﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ScrabbleData;
using ScrabbleGame;
using ScrabbleMoveChecker;
using ScrabbleWeb.Server.Data;
using ScrabbleWeb.Shared;

namespace ScrabbleWeb.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IWordCheckerFactory wordCheckerFactory;
        public GameController(ApplicationDbContext context, IWordCheckerFactory wordCheckerFactory)
        {
            this.context = context;
            this.wordCheckerFactory = wordCheckerFactory;
            game = game ?? new Game(wordCheckerFactory.GetWordChecker());
        }

        static Game game;

        [HttpGet("{id}")]
        public GameDto Get(int id)
        {
            string board = game.Board;
            return new GameDto
            {
                Board = board,
                PlayerTiles = game.Player1Tiles,
                OtherPlayerName = "Test Player"
            };
        }

        [HttpPost]
        public IEnumerable<string> Post(List<TilePlacement> placements)
        {
            var move = new Move(game);
            foreach (var placement in placements)
            {
                move.AddPlacement(placement);
            }

            var badWords = move.InvalidWords().ToList();

            if (badWords.Count() == 0)
            {
                move.Play();
            }
            else
            {
                Response.StatusCode = (int)HttpStatusCode.UnprocessableEntity;
            }

            return badWords;
        }
    }
}
