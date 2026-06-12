using AutoMapper;
using CRMWeb.Domain.Entities;
using CRMWeb.Domain.Requests;
using CRMWeb.Domain.Responses;
using CRMWeb.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CRMWeb.Api.Controllers;

[ApiController]
[Route("tarefas")]
public class TarefaController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public TarefaController(
        AppDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpPost("cliente/{clienteId:int}")]
    public async Task<ActionResult<TarefaResponse>> Post(
        int clienteId,
        TarefaRequest request)
    {
        var clienteExiste = await _context.Clientes
            .AsNoTracking()
            .AnyAsync(x => x.Id == clienteId);

        if (!clienteExiste)
        {
            return NotFound("Cliente não encontrado.");
        }

        if (request.ContatoId.HasValue)
        {
            var contatoExiste = await _context.Contatos
                .AsNoTracking()
                .AnyAsync(x =>
                    x.Id == request.ContatoId.Value &&
                    x.ClienteId == clienteId);

            if (!contatoExiste)
            {
                return BadRequest("Contato não encontrado para este cliente.");
            }
        }

        var tarefa = _mapper.Map<Tarefa>(request);

        tarefa.ClienteId = clienteId;
        tarefa.Concluida = false;
        tarefa.Ativo = true;

        _context.Tarefas.Add(tarefa);

        await _context.SaveChangesAsync();

        var response = _mapper.Map<TarefaResponse>(tarefa);

        return CreatedAtAction(
            nameof(GetById),
            new { id = tarefa.Id },
            response);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TarefaResponse>>> Get()
    {
        var tarefas = await _context.Tarefas
            .AsNoTracking()
            .ToListAsync();

        var response = _mapper.Map<List<TarefaResponse>>(tarefas);

        return Ok(response);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<TarefaResponse>> GetById(int id)
    {
        var tarefa = await _context.Tarefas
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);

        if (tarefa is null)
        {
            return NotFound();
        }

        var response = _mapper.Map<TarefaResponse>(tarefa);

        return Ok(response);
    }

    [HttpGet("cliente/{clienteId:int}")]
    public async Task<ActionResult<IEnumerable<TarefaResponse>>> GetByCliente(
        int clienteId)
    {
        var tarefas = await _context.Tarefas
            .AsNoTracking()
            .Where(x => x.ClienteId == clienteId)
            .ToListAsync();

        var response = _mapper.Map<List<TarefaResponse>>(tarefas);

        return Ok(response);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Put(
        int id,
        TarefaRequest request)
    {
        var tarefa = await _context.Tarefas
            .FirstOrDefaultAsync(x => x.Id == id);

        if (tarefa is null)
        {
            return NotFound();
        }

        if (request.ContatoId.HasValue)
        {
            var contatoExiste = await _context.Contatos
                .AsNoTracking()
                .AnyAsync(x =>
                    x.Id == request.ContatoId.Value &&
                    x.ClienteId == tarefa.ClienteId);

            if (!contatoExiste)
            {
                return BadRequest("Contato não encontrado para este cliente.");
            }
        }

        _mapper.Map(request, tarefa);

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpPatch("{id:int}/concluir")]
    public async Task<IActionResult> Concluir(int id)
    {
        var tarefa = await _context.Tarefas
            .FirstOrDefaultAsync(x => x.Id == id);

        if (tarefa is null)
        {
            return NotFound();
        }

        tarefa.Concluida = true;
        tarefa.DataConclusao = DateTime.Now;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpPatch("{id:int}/reabrir")]
    public async Task<IActionResult> Reabrir(int id)
    {
        var tarefa = await _context.Tarefas
            .FirstOrDefaultAsync(x => x.Id == id);

        if (tarefa is null)
        {
            return NotFound();
        }

        tarefa.Concluida = false;
        tarefa.DataConclusao = null;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var tarefa = await _context.Tarefas
            .FirstOrDefaultAsync(x => x.Id == id);

        if (tarefa is null)
        {
            return NotFound();
        }

        _context.Tarefas.Remove(tarefa);

        await _context.SaveChangesAsync();

        return NoContent();
    }
}