using GestaoOscAPI.Models.Entities;
using GestaoOscAPI.Models.Requests;
using GestaoOscAPI.Models.Responses;
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
        var oscs = oscService.ListarTodas();
        var oscsResponse = oscs.Select(osc => OscResponse.FromOsc(osc));
        return Ok(oscsResponse);
    }

    [HttpGet("{id}")]
    public IActionResult BuscarPorId(int id)
    {
        Osc? osc = oscService.BuscarPorId(id);

        if (osc == null)
            return NotFound();

        return Ok(OscResponse.FromOsc(osc));
    }

    [HttpGet("emitente/{id}")]
    public IActionResult BuscarPorEmitente(int id)
    {
        var emitente = usuarioService.BuscarPorId(id);

        if (emitente != null)
        {
            return Ok(oscService.BuscarPorEmitente(emitente.Id).Select(o => OscResponse.FromOsc(o)));
        }

        return NotFound();
    }

    [HttpGet("gerente/{id}")]
    public IActionResult BuscarPorGerente(int id)
    {
        var gerente = usuarioService.BuscarPorId(id);

        if (gerente != null)
        {
            return Ok(oscService.BuscarPorGerente(gerente.Id).Select(o => OscResponse.FromOsc(o)));
        }

        return NotFound();
    }

    [HttpPost]
    public IActionResult InserirOsc([FromBody] CriarOscRequest request)
    {
        Usuario? usuarioLogado = usuarioService.BuscarPorId(request.UsuarioLogadoId);

        if (usuarioLogado == null)
            return NotFound();

        Osc osc = oscService.CriarOsc(request.Descricao, request.Equipamento, request.AcaoTomada, request.PrecisaQualidade, request.PrecisaEngenharia, request.PrecisaProducao, usuarioLogado);
        return Ok(OscResponse.FromOsc(osc));

    }

    [HttpPut("{id}")]
    public IActionResult AtualizarOsc(int id, [FromBody] AtualizarOscRequest request)
    {
        Osc? osc = oscService.BuscarPorId(id);

        if (osc == null)
            return NotFound();

        osc.Descricao = request.Descricao;
        osc.Equipamento = request.Equipamento;
        osc.AcaoTomada = request.AcaoTomada;
        osc.PrecisaQualidade = request.PrecisaQualidade;
        osc.PrecisaEngenharia = request.PrecisaEngenharia;
        osc.PrecisaProducao = request.PrecisaProducao;


        oscService.Atualizar(osc);
        return Ok(OscResponse.FromOsc(osc));
    }

    [HttpPost("{id}/assinar")]
    public IActionResult AssinarOsc(int id, [FromBody] int usuarioId)
    {
        bool resultado = oscService.AssinarOSC(id, usuarioId);

        if (!resultado)
            return Unauthorized();

        return Ok();

    }

    [HttpPut("{id}/cancelar")]
    public IActionResult CancelarOsc(int id, [FromBody] int usuarioId)
    {
        if (oscService.Cancelar(id, usuarioId))
            return Ok();

        return Unauthorized();
    }

    [HttpPut("{id}/concluir")]
    public IActionResult ConcluirOsc(int id, [FromBody] int usuarioId)
    {
        if (oscService.Concluir(id, usuarioId))
            return Ok();

        return Unauthorized();
    }

    [HttpDelete("{id}")]
    public IActionResult DeletarOsc(int id)
    {
        Osc? osc = oscService.BuscarPorId(id);

        if (osc == null)
            return NotFound();

        if (oscService.Deletar(id))
            return Ok(OscResponse.FromOsc(osc));

        return NotFound();
    }
}
