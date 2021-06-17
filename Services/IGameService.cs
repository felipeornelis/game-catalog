using System;
using System.Threading.Tasks;
using System.Collections.Generic;

using GameCatalog.ViewModel;
using GameCatalog.InputModel;

namespace GameCatalog.Services
{
    public interface IGameService : IDisposable
    {
        Task<List<GameViewModel>> GetGames(int page, int length);
        
        Task<GameViewModel> GetGame(Guid id);

        Task<GameViewModel> Create(GameInputModel game);

        Task Update(Guid id, GameInputModel game);

        Task Update(Guid id, double price);

        Task Delete(Guid id);
    }
}