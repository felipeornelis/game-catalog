using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GameCatalog.Entities;
using System.Linq;

namespace GameCatalog.Repositories
{
    public class GameRepository : IGameRepository
    {
        private static Dictionary<Guid, Game> games = new Dictionary<Guid, Game>()
        {
            { Guid.Parse("93dae0a0-ceb3-11eb-b8bc-0242ac130003"), new Game { Id = Guid.Parse("93dae0a0-ceb3-11eb-b8bc-0242ac130003"), Name = "Fifa 21", Producer = "EA", Price = 200 } },
            { Guid.Parse("f8170120-ceb3-11eb-b8bc-0242ac130003"), new Game { Id = Guid.Parse("f8170120-ceb3-11eb-b8bc-0242ac130003"), Name = "Fifa 20", Producer = "EA", Price = 123 } },
            { Guid.Parse("1763fac4-ceb4-11eb-b8bc-0242ac130003"), new Game { Id = Guid.Parse("1763fac4-ceb4-11eb-b8bc-0242ac130003"), Name = "Tekken: Revenge Blood", Producer = "Namco", Price = 325 } },
            { Guid.Parse("2e19fe1c-ceb4-11eb-b8bc-0242ac130003"), new Game { Id = Guid.Parse("2e19fe1c-ceb4-11eb-b8bc-0242ac130003"), Name = "Naruto", Producer = "Naruto Corp", Price = 189 } },
            { Guid.Parse("5df46bf4-ceb3-11eb-b8bc-0242ac130003"), new Game { Id = Guid.Parse("5df46bf4-ceb3-11eb-b8bc-0242ac130003"), Name = "Dragon Ball Z: Budokai Tenkaichi 3", Producer = "Bandai Namco", Price = 222 } },
            { Guid.Parse("67ce8308-ceb3-11eb-b8bc-0242ac130003"), new Game { Id = Guid.Parse("67ce8308-ceb3-11eb-b8bc-0242ac130003"), Name = "PES 21", Producer = "Konami", Price = 1.99 } },
            { Guid.Parse("6ebdad42-ceb3-11eb-b8bc-0242ac130003"), new Game { Id = Guid.Parse("6ebdad42-ceb3-11eb-b8bc-0242ac130003"), Name = "Mortal Kombat 11", Producer = "Shiver Entertainment", Price = 285 } },
            { Guid.Parse("7b52936a-ceb3-11eb-b8bc-0242ac130003"), new Game { Id = Guid.Parse("7b52936a-ceb3-11eb-b8bc-0242ac130003"), Name = "Summer Heat Beach Volleyball", Producer = "Acclaim Entertainment", Price = 253 } },
            { Guid.Parse("84209fb4-ceb3-11eb-b8bc-0242ac130003"), new Game { Id = Guid.Parse("84209fb4-ceb3-11eb-b8bc-0242ac130003"), Name = "Toy Story 2", Producer = "Tiertex", Price = 200 } },
            { Guid.Parse("8b16c0aa-ceb3-11eb-b8bc-0242ac130003"), new Game { Id = Guid.Parse("8b16c0aa-ceb3-11eb-b8bc-0242ac130003"), Name = "Harry Potter and the Philosopher's Stone", Producer = "Argonaut Games", Price = 200 } },
        };
        public Task Create(Game game)
        {
            games.Add(game.Id, game);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Task<Game> GetGame(Guid id)
        {
            if( !games.ContainsKey(id) )
            {
                return null;
            }

            return Task.FromResult(games[id]);
        }

        public Task<List<Game>> GetGames(int page, int length)
        {
            if( page <= 0 )
            {
                page = 1;
            }
            
            return Task.FromResult(games.Values.Skip((page - 1) * length).Take(length).ToList());
        }

        public Task<List<Game>> GetGames(string name, string producer)
        {
            return Task.FromResult(games.Values.Where(game => game.Name.Equals(name) && game.Producer.Equals(producer)).ToList());
        }

        public Task Remove(Guid id)
        {
            if( !games.ContainsKey(id) )
            {
                return null;
            }

            games.Remove(id);

            return Task.CompletedTask;
        }

        public Task Update(Game game)
        {
            games[game.Id] = game;

            return Task.CompletedTask;
        }
    }
}