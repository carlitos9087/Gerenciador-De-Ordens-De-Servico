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

        var response = new UsuarioResponse 
        {
            Id = user.Id,
            Nome = user.Nome,
            Email = user.Email,
            Perfil = user.Perfil,
            Setor = user.Setor
        };

        return Ok(response);
    }

    [HttpGet("/usuarios")]
    public IActionResult ListarUsuarios()
    {
        var usuarios = usuarioService.ListarTodos();
        var usuariosResponse = usuarios.Select( u => new UsuarioResponse
                {
                    Id = u.Id,
                    Nome = u.Nome,
                    Email = u.Email,
                    Perfil = u.Perfil,
                    Setor = u.Setor
                }  
            ).ToList();

        return Ok(usuariosResponse);
    }

    [HttpGet("/usuarios/{id}")]
    public IActionResult BuscarUsuarioPorId(int id)
    {
        Usuario? usuario = usuarioService.BuscarPorId(id);

        if (usuario == null)
            return NotFound();

        UsuarioResponse usuarioResponse = new UsuarioResponse
        {
            Id = usuario.Id,
            Nome = usuario.Nome,
            Email = usuario.Email,
            Perfil = usuario.Perfil,
            Setor = usuario.Setor
        };
        return Ok(usuarioResponse);
    }

    [HttpGet("/usuarios/email")]
    public IActionResult BuscarUsuarioPorEmail([FromQuery] string email)
    {
        Usuario? usuario = usuarioService.BuscarPorEmail(email);

        if (usuario == null)
            return NotFound();

        UsuarioResponse usuarioResponse = new UsuarioResponse
        {
            Id = usuario.Id,
            Nome = usuario.Nome,
            Email = usuario.Email,
            Perfil = usuario.Perfil,
            Setor = usuario.Setor
        };
        return Ok(usuarioResponse);
    }

    [HttpGet("/usuarios/gerentes/{setor}")]
    public IActionResult BuscarGerentesPorSetor(Setor setor)
    {
        var gerentes = usuarioService.BuscarPorSetor(setor);

        var response = gerentes.Select(u => new UsuarioResponse
        {
            Id = u.Id,
            Nome = u.Nome,
            Email = u.Email,
            Perfil = u.Perfil,
            Setor = u.Setor
        }).ToList();

        return Ok(response);
    }

}

