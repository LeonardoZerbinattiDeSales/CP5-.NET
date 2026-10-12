using CP5_.NET.Models;
using CP5_.NET.Services;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace CP5_.NET.Controllers;

[ApiController]
[Route("api/[controller]")]
public class JogosController : ControllerBase
{
    private readonly JogoService _service;

    public JogosController(JogoService service)
    {
        _service = service;
    }

    // GET: api/jogos
    [HttpGet]
    public async Task<ActionResult<List<Jogo>>> ListarTodos()
    {
        var jogos = await _service.ListarTodosAsync();

        return Ok(jogos);
    }

    // GET: api/jogos/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<Jogo>> BuscarPorId(string id)
    {
        if (!ObjectId.TryParse(id, out _))
        {
            return BadRequest(new
            {
                mensagem = "O identificador informado é inválido."
            });
        }

        var jogo = await _service.BuscarPorIdAsync(id);

        if (jogo is null)
        {
            return NotFound(new
            {
                mensagem = "Jogo não encontrado."
            });
        }

        return Ok(jogo);
    }

    // POST: api/jogos
    [HttpPost]
    public async Task<ActionResult<Jogo>> Criar([FromBody] Jogo jogo)
    {
        var jogoCriado = await _service.CriarAsync(jogo);

        return CreatedAtAction(
            nameof(BuscarPorId),
            new { id = jogoCriado.Id },
            jogoCriado
        );
    }

    // PUT: api/jogos/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> Atualizar(
        string id,
        [FromBody] Jogo jogo)
    {
        if (!ObjectId.TryParse(id, out _))
        {
            return BadRequest(new
            {
                mensagem = "O identificador informado é inválido."
            });
        }

        var atualizado = await _service.AtualizarAsync(id, jogo);

        if (!atualizado)
        {
            return NotFound(new
            {
                mensagem = "Jogo não encontrado para atualização."
            });
        }

        return NoContent();
    }

    // DELETE: api/jogos/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> Excluir(string id)
    {
        if (!ObjectId.TryParse(id, out _))
        {
            return BadRequest(new
            {
                mensagem = "O identificador informado é inválido."
            });
        }

        var excluido = await _service.ExcluirAsync(id);

        if (!excluido)
        {
            return NotFound(new
            {
                mensagem = "Jogo não encontrado para exclusão."
            });
        }

        return NoContent();
    }

    // GET: api/jogos/busca?plataforma=PlayStation%205&precoMaximo=300
    [HttpGet("busca")]
    public async Task<ActionResult<List<Jogo>>> BuscarComFiltros(
        [FromQuery] string? plataforma,
        [FromQuery] decimal? precoMaximo)
    {
        if (precoMaximo.HasValue && precoMaximo.Value < 0)
        {
            return BadRequest(new
            {
                mensagem = "O preço máximo não pode ser negativo."
            });
        }

        var jogos = await _service.BuscarComFiltrosAsync(
            plataforma,
            precoMaximo
        );

        return Ok(jogos);
    }

    // GET: api/jogos/relatorio-estoque
    [HttpGet("relatorio-estoque")]
    public async Task<ActionResult<List<RelatorioEstoque>>> RelatorioEstoque()
    {
        var relatorio = await _service.GerarRelatorioEstoqueAsync();

        return Ok(relatorio);
    }
}