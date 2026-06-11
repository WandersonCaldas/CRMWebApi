using AutoMapper;
using CRMWeb.Domain.Entities;
using CRMWeb.Domain.Enums;
using CRMWeb.Domain.Requests;
using CRMWeb.Domain.Responses;
using CRMWeb.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CRMWeb.Api.Controllers;

[ApiController]
[Route("clientes")]
public class ClienteController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public ClienteController(
        AppDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<ActionResult<ClienteResponse>> Post(
        ClienteRequest request)
    {
        if (!Enum.IsDefined(typeof(TipoPessoa), request.TipoPessoa))
        {
            return BadRequest("TipoPessoa inválido.");
        }

        var cliente = _mapper.Map<Cliente>(request);

        cliente.DataCadastro = DateTime.Now;
        cliente.Ativo = true;

        _context.Clientes.Add(cliente);

        await _context.SaveChangesAsync();

        var response = _mapper.Map<ClienteResponse>(cliente);

        return CreatedAtAction(
            nameof(GetById),
            new { id = cliente.Id },
            response);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ClienteResponse>>> Get()
    {
        var clientes = await _context.Clientes
            .AsNoTracking()
            .ToListAsync();

        var response = _mapper.Map<List<ClienteResponse>>(clientes);

        return Ok(response);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ClienteResponse>> GetById(int id)
    {
        var cliente = await _context.Clientes
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);

        if (cliente is null)
        {
            return NotFound();
        }

        var response = _mapper.Map<ClienteResponse>(cliente);

        return Ok(response);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Put(
        int id,
        ClienteRequest request)
    {
        if (!Enum.IsDefined(typeof(TipoPessoa), request.TipoPessoa))
        {
            return BadRequest("TipoPessoa inválido.");
        }

        var cliente = await _context.Clientes
            .FirstOrDefaultAsync(x => x.Id == id);

        if (cliente is null)
        {
            return NotFound();
        }

        _mapper.Map(request, cliente);

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var cliente = await _context.Clientes
            .FirstOrDefaultAsync(x => x.Id == id);

        if (cliente is null)
        {
            return NotFound();
        }

        _context.Clientes.Remove(cliente);

        await _context.SaveChangesAsync();

        return NoContent();
    }
}