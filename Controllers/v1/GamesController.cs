using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Mvc;

using Swashbuckle.AspNetCore.SwaggerUI;

using GameCatalog.Services;
using GameCatalog.ViewModel;
using GameCatalog.InputModel;
using GameCatalog.Exceptions;

namespace GameCatalog.Controllers.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {

        private readonly IGameService _gameService;

        public GamesController(IGameService gameService)
        {
            _gameService = gameService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GameViewModel>>> GetGames([FromQuery, Range(1, int.MaxValue)] int page = 1, [FromQuery, Range(1, 50)] int length = 5)
        {
            List<GameViewModel> games = await _gameService.GetGames(page, length);

            if( games.Count == 0 )
            {
                return NoContent();
            }


            return Ok(games);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<GameViewModel>> GetGame([FromRoute] Guid id)
        {
            GameViewModel game = await _gameService.GetGame(id);

            if( game == null ) 
            {
                return NoContent();
            }

            return Ok(game);
        }

        [HttpPost]
        public async Task<ActionResult<GameViewModel>> CreateGame([FromBody] GameInputModel game)
        {
            try
            {
                GameViewModel createdGame = await _gameService.Create(game);
                
                return Ok(createdGame);

            }
            catch(GameRegisteredException error)
            {
                return UnprocessableEntity($"This game already exists: {error}");
            }

        }

        // It updates the whole resource
        [HttpPut("{id:guid}")]
        public async Task<ActionResult> UpdateGame([FromRoute] Guid id, [FromBody] GameInputModel game)
        {
            try
            {
                
                await _gameService.Update(id, game);
                return Ok();
            }
            catch(GameNotFoundException error)
            {
                return NotFound($"Game not found {error}");
            }
        }


        // Updates a specific element of the resource
        [HttpPatch("{id:guid}/price/{price:double}")]
        public async Task<ActionResult> UpdateGame([FromRoute] Guid id, [FromRoute] double price)
        {
            try
            {
                await _gameService.Update(id, price);

                return Ok();
            }
            catch(GameNotFoundException error)
            {
                return NotFound($"Game Not found {error}");
            }
        }


        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> DeleteGame([FromRoute] Guid id)
        {
            try
            {
                await _gameService.Delete(id);

                return Ok();
            }
            catch(GameNotFoundException error)
            {
                return NotFound($"Game not found {error}");
            }
            
        }


    }
}