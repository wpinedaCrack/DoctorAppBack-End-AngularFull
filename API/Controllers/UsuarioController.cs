﻿using Data;
using Data.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.DTOs;
using Models.Entidades;
using System.Security.Cryptography;
using System.Text;

namespace API.Controllers
{
    public class UsuarioController : BaseApiController
    {
        public readonly AplicationDbContext _db;
        public readonly ITokenServicio _tokenServicio;

        public UsuarioController(AplicationDbContext db, ITokenServicio tokenServicio)
        {
            _db = db;
            _tokenServicio = tokenServicio;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarios()
        {
            var usuarios = await _db.Usuarios.ToListAsync();
            return Ok(usuarios);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> GetUsuario(int id)
        {
            var usuarios = await _db.Usuarios.FindAsync(id);
            return Ok(usuarios);
        }

        [HttpPost("registro")]
        public async Task<ActionResult<UsuarioDto>> Registro(RegistroDto registroDto)
        {
            if (await UsuarioExiste(registroDto.Username))
            {
                return BadRequest("Usuario ya esta Registrado.");
            }
            using var hmac = new HMACSHA512();

            var usuario = new Usuario
            {
                Username = registroDto.Username.ToLower(),
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registroDto.Password)),
                PasswordSalt = hmac.Key
            };
            _db.Usuarios.Add(usuario);
            await _db.SaveChangesAsync();

            return new UsuarioDto
            {
                Username = registroDto.Username.ToLower(),
                Token= _tokenServicio.crearToken(usuario)
            };
        }

        [HttpPost("login")]
        public async Task<ActionResult<UsuarioDto>> Login(LoginDto loginDto)
        {
            var usuario = await _db.Usuarios.SingleOrDefaultAsync(x=>x.Username==loginDto.Username);

            if(usuario == null)
            {
                return Unauthorized("Usuario no Valido");
            }
            using var hmac = new HMACSHA512(usuario.PasswordSalt);

            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

            for(int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != usuario.PasswordHash[i])
                {
                    return Unauthorized("Passworld no Valido");
                }
            }

            return new UsuarioDto
            {
                Username = usuario.Username.ToLower(),
                Token = _tokenServicio.crearToken(usuario)
            };
        }

        [AllowAnonymous]
        private async Task<bool> UsuarioExiste(string username)
        {
            return await _db.Usuarios.AnyAsync(x => x.Username.Equals(username.ToLower()));
        }
    }
}
