using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using GameCatalog.InputModel;
using GameCatalog.Repositories;
using GameCatalog.ViewModel;
using GameCatalog.Entities;
using GameCatalog.Exceptions;

namespace GameCatalog.Services
{
    public class GameService : IGameService
    {
        private readonly IGameRepository _gameRepository;

        public GameService(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }

       public async Task<List<GameViewModel>> GetGames(int page, int length)
       {
           List<Game> games = await _gameRepository.GetGames(page, length);

           return games.Select(game => new GameViewModel
           {
               Id = game.Id,
               Name = game.Name,
               Producer = game.Producer,
               Price = game.Price
           }).ToList();
       }

       public async Task<GameViewModel> GetGame(Guid id)
       {
           Game game = await _gameRepository.GetGame(id);

           if( game == null )
           {
               return null;
           }

           return new GameViewModel
           {
               Id = game.Id,
               Name = game.Name,
               Producer = game.Producer,
               Price = game.Price
           };
       }

       public async Task<GameViewModel> Create(GameInputModel game)
       {
           List<Game> queryGame = await _gameRepository.GetGames(game.Name, game.Producer);

           if( queryGame.Count > 0 )
           {
               throw new GameRegisteredException();
           }

           Game createGame = new Game
           {
               Id = Guid.NewGuid(),
               Name = game.Name,
               Producer = game.Producer,
               Price = game.Price
           };

           await _gameRepository.Create(createGame);

           return new GameViewModel
           {
               Id = createGame.Id,
               Name = createGame.Name,
               Producer = createGame.Producer,
               Price = createGame.Price
           };
       }

       public async Task Update(Guid id, GameInputModel game)
       {
           Game queryGame = await _gameRepository.GetGame(id);

           if( queryGame == null )
           {
               throw new GameNotFoundException();
           }

           queryGame.Name = game.Name;
           queryGame.Producer = game.Producer;
           queryGame.Price = game.Price;

           await _gameRepository.Update(queryGame);
       }

       public async Task Update(Guid id, double price)
       {
           Game queryGame = await _gameRepository.GetGame(id);

           if( queryGame == null )
           {
               throw new GameNotFoundException();
           }

           queryGame.Price = price;

           await _gameRepository.Update(queryGame);
       }

       public async Task Delete(Guid id)
       {
           Game queryGame = await _gameRepository.GetGame(id);

           if( queryGame == null )
           {
               throw new GameNotFoundException();
           }

           await _gameRepository.Remove(id);
       }

       public void Dispose()
       {
           _gameRepository?.Dispose();
       }

    }
}