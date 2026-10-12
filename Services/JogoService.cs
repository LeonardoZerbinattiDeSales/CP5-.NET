using CP5_.NET.Models;
using CP5_.NET.Repositories;

namespace CP5_.NET.Services;

public class JogoService
{
    private readonly JogoRepository _repository;

    public JogoService(JogoRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<Jogo>> ListarTodosAsync()
    {
        return await _repository.ListarTodosAsync();
    }

    public async Task<Jogo?> BuscarPorIdAsync(string id)
    {
        return await _repository.BuscarPorIdAsync(id);
    }

    public async Task<Jogo> CriarAsync(Jogo jogo)
    {
        jogo.Id = null;

        return await _repository.CriarAsync(jogo);
    }

    public async Task<bool> AtualizarAsync(string id, Jogo jogo)
    {
        return await _repository.AtualizarAsync(id, jogo);
    }

    public async Task<bool> ExcluirAsync(string id)
    {
        return await _repository.ExcluirAsync(id);
    }

    public async Task<List<Jogo>> BuscarComFiltrosAsync(
        string? plataforma,
        decimal? precoMaximo)
    {
        return await _repository.BuscarComFiltrosAsync(
            plataforma,
            precoMaximo
        );
    }

    public async Task<List<RelatorioEstoque>> GerarRelatorioEstoqueAsync()
    {
        return await _repository.GerarRelatorioEstoqueAsync();
    }
}