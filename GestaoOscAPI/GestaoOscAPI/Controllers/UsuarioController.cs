using GestaoOscAPI.Models.Entities;
using GestaoOscAPI.Models.Enums;
using GestaoOscAPI.Models.Requests;
using GestaoOscAPI.Models.Responses;
using GestaoOscAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace GestaoOscAPI.Controllers;

[ApiController]
[Route("/auth")]

public class UsuarioController : ControllerBase
{
    private readonly UsuarioService usuarioService;

    public UsuarioController(UsuarioService usuarioService)
    {
        this.usuarioService = usuarioService;
    }


    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        Usuario? user = usuarioService.ValidarLogin(request.Email, request.Senha);
        if (user == null)
            return Unauthorized();
        return Ok(UsuarioResponse.FromUsuario(user));
    }

    [HttpGet("/usuarios")]
    public IActionResult ListarUsuarios()
    {
        var usuarios = usuarioService.ListarTodos();
        var usuariosResponse = usuarios.Select(u => UsuarioResponse.FromUsuario(u)).ToList();

        return Ok(usuariosResponse);
    }

    [HttpGet("/usuarios/{id}")]
    public IActionResult BuscarUsuarioPorId(int id)
    {
        Usuario? usuario = usuarioService.BuscarPorId(id);

        if (usuario == null)
            return NotFound();

        return Ok(UsuarioResponse.FromUsuario(usuario));
    }

    [HttpGet("/usuarios/email")]
    public IActionResult BuscarUsuarioPorEmail([FromQuery] string email)
    {
        Usuario? usuario = usuarioService.BuscarPorEmail(email);

        if (usuario == null)
            return NotFound();

        return Ok(UsuarioResponse.FromUsuario(usuario));
    }

    [HttpGet("/usuarios/gerentes/{setor}")]
    public IActionResult BuscarGerentesPorSetor(Setor setor)
    {
        var gerentes = usuarioService.BuscarPorSetor(setor);

        var response = gerentes.Select(u => UsuarioResponse.FromUsuario(u)).ToList();

        return Ok(response);
    }

    [HttpPost("/usuarios")]
    public IActionResult InserirUsuario([FromBody] CriarUsuarioRequest request)
    {
        Usuario usuario = usuarioService.CriarUsuario(request.Nome, request.Email, request.Senha, request.Perfil, request.Setor);

        return Ok(UsuarioResponse.FromUsuario(usuario));

    }

    [HttpPut("/usuarios/{id}")]
    public IActionResult AtualizarUsuario(int id, [FromBody] AtualizarUsuarioRequest request)
    {
        Usuario? usuario = usuarioService.BuscarPorId(id);

        if (usuario == null)
            return NotFound();

        usuario.Nome = request.Nome;
        usuario.Email = request.Email;
        usuario.Senha = request.Senha;
        usuario.Perfil = request.Perfil;
        usuario.Setor = request.Setor;

        usuarioService.Atualizar(usuario);
        return Ok(UsuarioResponse.FromUsuario(usuario));
    }

    [HttpDelete("/usuarios/{id}")]
    public IActionResult DeletarUsuario (int id)
    {
        Usuario? usuario = usuarioService.BuscarPorId(id);

        if (usuario == null) 
            return NotFound();

        return Ok(usuarioService.Deletar(id));

    }
}

