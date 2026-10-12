using System.Text.RegularExpressions;
using CP5_.NET.Models;
using CP5_.NET.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace CP5_.NET.Repositories;

public class JogoRepository
{
    private readonly IMongoCollection<Jogo> _jogos;

    public JogoRepository(IOptions<MongoDbSettings> settings)
    {
        var configuracoes = settings.Value;

        var cliente = new MongoClient(configuracoes.ConnectionString);
        var banco = cliente.GetDatabase(configuracoes.DatabaseName);

        _jogos = banco.GetCollection<Jogo>(
            configuracoes.CollectionName
        );
    }

    public async Task<List<Jogo>> ListarTodosAsync()
    {
        return await _jogos
            .Find(Builders<Jogo>.Filter.Empty)
            .ToListAsync();
    }

    public async Task<Jogo?> BuscarPorIdAsync(string id)
    {
        return await _jogos
            .Find(jogo => jogo.Id == id)
            .FirstOrDefaultAsync();
    }

    public async Task<Jogo> CriarAsync(Jogo jogo)
    {
        await _jogos.InsertOneAsync(jogo);

        return jogo;
    }

    public async Task<bool> AtualizarAsync(string id, Jogo jogo)
    {
        jogo.Id = id;

        var resultado = await _jogos.ReplaceOneAsync(
            item => item.Id == id,
            jogo
        );

        return resultado.MatchedCount > 0;
    }

    public async Task<bool> ExcluirAsync(string id)
    {
        var resultado = await _jogos.DeleteOneAsync(
            jogo => jogo.Id == id
        );

        return resultado.DeletedCount > 0;
    }

    public async Task<List<Jogo>> BuscarComFiltrosAsync(
        string? plataforma,
        decimal? precoMaximo)
    {
        var filtro = Builders<Jogo>.Filter.Empty;

        if (!string.IsNullOrWhiteSpace(plataforma))
        {
            var regex = new BsonRegularExpression(
                Regex.Escape(plataforma.Trim()),
                "i"
            );

            filtro &= Builders<Jogo>.Filter.Regex(
                jogo => jogo.Plataforma,
                regex
            );
        }

        if (precoMaximo.HasValue)
        {
            filtro &= Builders<Jogo>.Filter.Lte(
                jogo => jogo.Preco,
                precoMaximo.Value
            );
        }

        return await _jogos
            .Find(filtro)
            .ToListAsync();
    }

    public async Task<List<RelatorioEstoque>> GerarRelatorioEstoqueAsync()
    {
        var pipeline = new[]
        {
            new BsonDocument("$group", new BsonDocument
            {
                {
                    "_id",
                    "$plataforma"
                },
                {
                    "quantidadeTitulos",
                    new BsonDocument("$sum", 1)
                },
                {
                    "quantidadeUnidades",
                    new BsonDocument("$sum", "$estoque")
                },
                {
                    "valorTotalEstoque",
                    new BsonDocument(
                        "$sum",
                        new BsonDocument(
                            "$multiply",
                            new BsonArray
                            {
                                "$preco",
                                "$estoque"
                            }
                        )
                    )
                }
            }),

            new BsonDocument("$project", new BsonDocument
            {
                {
                    "_id",
                    0
                },
                {
                    "plataforma",
                    "$_id"
                },
                {
                    "quantidadeTitulos",
                    1
                },
                {
                    "quantidadeUnidades",
                    1
                },
                {
                    "valorTotalEstoque",
                    1
                }
            }),

            new BsonDocument("$sort", new BsonDocument
            {
                {
                    "plataforma",
                    1
                }
            })
        };

        return await _jogos
            .Aggregate<RelatorioEstoque>(pipeline)
            .ToListAsync();
    }
}