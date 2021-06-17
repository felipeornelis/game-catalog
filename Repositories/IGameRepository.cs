using System;
using System.Threading.Tasks;
using System.Collections.Generic;

using GameCatalog.Entities;

namespace GameCatalog.Repositories
{
    public interface IGameRepository : IDisposable
    {
        Task<List<Game>> GetGames(int page, int length);

        Task<List<Game>> GetGames(string name, string producer);
        
        Task<Game> GetGame(Guid id);

        Task Create(Game game);

        Task Update(Game game);

        Task Remove(Guid id);
    }
}