using AutoMapper;
using CRMWeb.Domain.Entities;
using CRMWeb.Domain.Requests;
using CRMWeb.Domain.Responses;
using CRMWeb.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CRMWeb.Api.Controllers;

[ApiController]
[Route("contatos")]
public class ContatoController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public ContatoController(
        AppDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpPost("cliente/{clienteId:int}")]
    public async Task<ActionResult<ContatoResponse>> Post(
        int clienteId,
        ContatoRequest request)
    {
        var clienteExiste = await _context.Clientes
            .AsNoTracking()
            .AnyAsync(x => x.Id == clienteId);

        if (!clienteExiste)
        {
            return NotFound("Cliente não encontrado.");
        }

        var contato = _mapper.Map<Contato>(request);

        contato.ClienteId = clienteId;
        contato.Ativo = true;

        _context.Contatos.Add(contato);

        await _context.SaveChangesAsync();

        var response = _mapper.Map<ContatoResponse>(contato);

        return CreatedAtAction(
            nameof(GetById),
            new { id = contato.Id },
            response);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ContatoResponse>>> Get()
    {
        var contatos = await _context.Contatos
            .AsNoTracking()
            .ToListAsync();

        var response = _mapper.Map<List<ContatoResponse>>(contatos);

        return Ok(response);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ContatoResponse>> GetById(int id)
    {
        var contato = await _context.Contatos
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);

        if (contato is null)
        {
            return NotFound();
        }

        var response = _mapper.Map<ContatoResponse>(contato);

        return Ok(response);
    }

    [HttpGet("cliente/{clienteId:int}")]
    public async Task<ActionResult<IEnumerable<ContatoResponse>>> GetByCliente(
        int clienteId)
    {
        var contatos = await _context.Contatos
            .AsNoTracking()
            .Where(x => x.ClienteId == clienteId)
            .ToListAsync();

        var response = _mapper.Map<List<ContatoResponse>>(contatos);

        return Ok(response);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Put(
        int id,
        ContatoRequest request)
    {
        var contato = await _context.Contatos
            .FirstOrDefaultAsync(x => x.Id == id);

        if (contato is null)
        {
            return NotFound();
        }

        _mapper.Map(request, contato);

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var contato = await _context.Contatos
            .FirstOrDefaultAsync(x => x.Id == id);

        if (contato is null)
        {
            return NotFound();
        }

        _context.Contatos.Remove(contato);

        await _context.SaveChangesAsync();

        return NoContent();
    }
}