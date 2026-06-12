using AutoMapper;
using CRMWeb.Domain.Entities;
using CRMWeb.Domain.Requests;
using CRMWeb.Domain.Responses;
using CRMWeb.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CRMWeb.Api.Controllers;

[ApiController]
[Route("agendas")]
public class AgendaController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public AgendaController(
        AppDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<ActionResult<AgendaResponse>> Post(
    AgendaRequest request)
    {
        if (request.DataFim < request.DataInicio)
        {
            return BadRequest("A data final não pode ser menor que a data inicial.");
        }

        var clienteExiste = await _context.Clientes
            .AnyAsync(x => x.Id == request.ClienteId);

        if (!clienteExiste)
        {
            return BadRequest("Cliente não encontrado.");
        }

        if (request.ContatoId.HasValue)
        {
            var contatoExiste = await _context.Contatos
                .AnyAsync(x => x.Id == request.ContatoId.Value);

            if (!contatoExiste)
            {
                return BadRequest("Contato não encontrado.");
            }
        }

        var agenda = _mapper.Map<Agenda>(request);

        agenda.Ativo = true;

        _context.Agendas.Add(agenda);

        await _context.SaveChangesAsync();

        var response = _mapper.Map<AgendaResponse>(agenda);

        return CreatedAtAction(
            nameof(GetById),
            new { id = agenda.Id },
            response);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AgendaResponse>>> Get()
    {
        var agendas = await _context.Agendas
            .AsNoTracking()
            .ToListAsync();

        var response = _mapper.Map<List<AgendaResponse>>(agendas);

        return Ok(response);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<AgendaResponse>> GetById(int id)
    {
        var agenda = await _context.Agendas
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);

        if (agenda is null)
        {
            return NotFound();
        }

        var response = _mapper.Map<AgendaResponse>(agenda);

        return Ok(response);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Put(
    int id,
    AgendaRequest request)
    {
        if (request.DataFim < request.DataInicio)
        {
            return BadRequest("A data final não pode ser menor que a data inicial.");
        }

        var agenda = await _context.Agendas
            .FirstOrDefaultAsync(x => x.Id == id);

        if (agenda is null)
        {
            return NotFound();
        }

        var clienteExiste = await _context.Clientes
            .AnyAsync(x => x.Id == request.ClienteId);

        if (!clienteExiste)
        {
            return BadRequest("Cliente não encontrado.");
        }

        if (request.ContatoId.HasValue)
        {
            var contatoExiste = await _context.Contatos
                .AnyAsync(x => x.Id == request.ContatoId.Value);

            if (!contatoExiste)
            {
                return BadRequest("Contato não encontrado.");
            }
        }

        _mapper.Map(request, agenda);

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var agenda = await _context.Agendas
            .FirstOrDefaultAsync(x => x.Id == id);

        if (agenda is null)
        {
            return NotFound();
        }

        _context.Agendas.Remove(agenda);

        await _context.SaveChangesAsync();

        return NoContent();
    }
}