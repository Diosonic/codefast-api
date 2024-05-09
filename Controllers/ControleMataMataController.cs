﻿using Codefast.Models;
using Codefast.Models.DTOs.ControleMataMata;
using Codefast.Models.DTOs.Equipe;
using Codefast.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Codefast.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ControleMataMataController : ControllerBase
    {
        private readonly IControleMataMataRepository _repository;

        public ControleMataMataController(IControleMataMataRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("{idTorneio}")]
        public async Task<ActionResult<IEnumerable<ControleMataMataDTO>>> Get(int idTorneio)
        {

            IEnumerable<ControleMataMataDTO> controleMataMata = await _repository.GetControleMataMataAsync(idTorneio);

            if (controleMataMata == null)
                return NotFound("Nenhuma equipe foi encontrada para a etapa mata-mata");

            if (controleMataMata.Count() != 8)
            {
                return BadRequest("Ops! Quantidade das equipes tem que ser igual a 8 para para começar a etapa de mata-mata.");
            }

            return Ok(controleMataMata);
        }

        [HttpPost("{idTorneio}/adicionar-controles")]
        public async Task<ActionResult<IEnumerable<ControleMataMataDTO>>> Post(int idTorneio)
        {
            IEnumerable<EquipeDTO> equipes = await _repository.GetEquipesParaFaseMataMata(idTorneio);

            if (equipes is null)
                return BadRequest("Nenhuma equipe encontrada");

            if (equipes.Count() != 8)
                return BadRequest("Ops! Quantidade das equipes tem que ser igual a 8 para para começar a etapa de mata-mata.");

            foreach (var equipe in equipes)
            {
                ControleMataMata controleMata = new ControleMataMata
                {
                    StatusValidacao = "Em espera",
                    EquipeId = equipe.Id
                };

                await _repository.AddAsync(controleMata);
            }

            return Ok(equipes);
        }


    }
}