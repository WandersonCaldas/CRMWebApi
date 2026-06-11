using CRMWeb.Domain.Enums;
using CRMWeb.Domain.Responses;
using Microsoft.AspNetCore.Mvc;

namespace CRMWeb.Api.Controllers;

[ApiController]
[Route("tipos-pessoa")]
public class TipoPessoaController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        var tipos = Enum.GetValues<TipoPessoa>()
            .Select(x => new TipoPessoaResponse
            {
                Id = (int)x,
                Descricao = x.ToString()
            });

        return Ok(tipos);
    }
}