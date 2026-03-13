using GestaoOscAPI.Models.Entities;
using GestaoOscAPI.Models.Requests;
using GestaoOscAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System;

namespace GestaoOscAPI.Controllers;

[ApiController]
[Route("/osc")]
public class OscController : ControllerBase
{
    private readonly OscService oscService;
    private readonly UsuarioService usuarioService;

    public OscController(OscService oscService, UsuarioService usuarioService)
    {
        this.oscService = oscService;
        this.usuarioService = usuarioService;
    }

    [HttpGet]
    public IActionResult ListarTodas()
    {
        return Ok(oscService.ListarTodas());
    }

    [HttpGet("{id}")]
    public IActionResult BuscarPorId(int id)
    {
        Osc? osc = oscService.BuscarPorId(id);

        if (osc == null)
            return NotFound();

        return Ok(osc);
    }

    [HttpPost]
    public IActionResult InserirOsc([FromBody] CriarOscRequest request)
    {
        Usuario? gerenteQualidade = usuarioService.BuscarPorId(request.GerenteQualidadeId);
        Usuario? gerenteEngenharia = usuarioService.BuscarPorId(request.GerenteEngenhariaId);
        Usuario? gerenteProducao = usuarioService.BuscarPorId(request.GerenteProducaoId);
        Usuario? usuarioLogado = usuarioService.BuscarPorId(request.UsuarioLogadoId);

        if (gerenteQualidade == null || gerenteEngenharia == null
            || gerenteProducao == null || usuarioLogado == null)
            return NotFound("Um ou mais usuários não foram encontrados.");

        Osc osc = oscService.CriarOsc(request.descricao, request.Equipamento, gerenteQualidade, gerenteEngenharia, gerenteProducao, usuarioLogado);
        return Ok(osc);

    }

    [HttpPut("{id}")]
    public IActionResult AtualizarOsc(int id, [FromBody] AtualizarOscRequest request)
    {
        Osc? osc = oscService.BuscarPorId(id);

        if (osc == null)
            return NotFound();

        osc.Descricao = request.descricao;
        osc.Equipamento = request.Equipamento;

        if (request.GerenteQualidadeId > 0)
            osc.GerenteQualidade = usuarioService.BuscarPorId(request.GerenteQualidadeId);  
        if (request.GerenteEngenhariaId > 0)
            osc.GerenteEngenharia = usuarioService.BuscarPorId(request.GerenteEngenhariaId);  
        if (request.GerenteProducaoId > 0)
            osc.GerenteProducao = usuarioService.BuscarPorId(request.GerenteProducaoId);  


        oscService.Atualizar(osc);
        return Ok(osc);
    }

    [HttpPost("{id}/assinar")]
    public IActionResult AssinarOsc(int id, [FromBody] int usuarioId)
    {
        bool resultado = oscService.AssinarOSC(id, usuarioId);

        if (!resultado)
            return Forbid();

        return Ok();

    }

    [HttpPut("{id}/cancelar")]
    public IActionResult CancelarOsc(int id)
    {
        Osc? osc = oscService.BuscarPorId(id);

        if (osc == null)
            return NotFound();

        if (oscService.Cancelar(id))
            return Ok(osc);

        return NotFound();
    }

    [HttpDelete("{id}")]
    public IActionResult DeletarOsc(int id)
    {
        Osc? osc = oscService.BuscarPorId(id);

        if (osc == null)
            return NotFound();

        if (oscService.Deletar(id))
            return Ok(osc);

        return NotFound();
    }
}
