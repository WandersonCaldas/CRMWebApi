using AutoMapper;
using CRMWeb.Domain.Entities;
using CRMWeb.Domain.Requests;
using CRMWeb.Domain.Responses;
using CRMWeb.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CRMWeb.Api.Controllers;

[ApiController]
[Route("enderecos")]
public class EnderecoController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public EnderecoController(
        AppDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpPost("cliente/{clienteId:int}")]
    public async Task<ActionResult<EnderecoResponse>> Post(
        int clienteId,
        EnderecoRequest request)
    {
        var cliente = await _context.Clientes
            .AsNoTracking()
            .AnyAsync(x => x.Id == clienteId);

        if (!cliente)
        {
            return NotFound("Cliente não encontrado.");
        }

        if (request.Uf.Length != 2)
        {
            return BadRequest("UF deve possuir 2 caracteres.");
        }

        if (request.Cep.Length != 8)
        {
            return BadRequest("CEP deve possuir 8 dígitos.");
        }

        var endereco = _mapper.Map<Endereco>(request);

        endereco.ClienteId = clienteId;

        _context.Enderecos.Add(endereco);

        await _context.SaveChangesAsync();

        var response = _mapper.Map<EnderecoResponse>(endereco);

        return CreatedAtAction(
            nameof(GetById),
            new { id = endereco.Id },
            response);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<EnderecoResponse>> GetById(int id)
    {
        var endereco = await _context.Enderecos
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);

        if (endereco is null)
        {
            return NotFound();
        }

        var response = _mapper.Map<EnderecoResponse>(endereco);

        return Ok(response);
    }

    [HttpGet("cliente/{clienteId:int}")]
    public async Task<ActionResult<IEnumerable<EnderecoResponse>>> GetByCliente(
        int clienteId)
    {
        var enderecos = await _context.Enderecos
            .AsNoTracking()
            .Where(x => x.ClienteId == clienteId)
            .ToListAsync();

        var response = _mapper.Map<List<EnderecoResponse>>(enderecos);

        return Ok(response);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Put(
        int id,
        EnderecoRequest request)
    {
        var endereco = await _context.Enderecos
            .FirstOrDefaultAsync(x => x.Id == id);

        if (endereco is null)
        {
            return NotFound();
        }

        if (request.Uf.Length != 2)
        {
            return BadRequest("UF deve possuir 2 caracteres.");
        }

        if (request.Cep.Length != 8)
        {
            return BadRequest("CEP deve possuir 8 dígitos.");
        }

        _mapper.Map(request, endereco);

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var endereco = await _context.Enderecos
            .FirstOrDefaultAsync(x => x.Id == id);

        if (endereco is null)
        {
            return NotFound();
        }

        _context.Enderecos.Remove(endereco);

        await _context.SaveChangesAsync();

        return NoContent();
    }
}