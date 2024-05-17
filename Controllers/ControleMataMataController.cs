using Codefast.Models;
using Codefast.Models.DTOs.ControleMataMata;
using Codefast.Repository;
using Codefast.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;
using System.Linq;

namespace Codefast.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ControleMataMataController : ControllerBase
    {
        private readonly IControleMataMataRepository _repository;
        private readonly ISementeRodadaRepository _sementeRodadaRepository;

        public ControleMataMataController(IControleMataMataRepository repository, ISementeRodadaRepository sementeRodadaRepository)
        {
            _repository = repository;
            _sementeRodadaRepository = sementeRodadaRepository;
        }

        [HttpGet("{idTorneio}")]
        public async Task<ActionResult<IEnumerable<ControleMataMataDTO>>> Get(int idTorneio)
        {

            IEnumerable<ControleMataMataDTO> controleMataMata = await _repository.GetControleMataMataAsync(idTorneio);

            if (controleMataMata == null)
                return NotFound("Nenhuma equipe foi encontrada para a etapa mata-mata");

            return Ok(controleMataMata);
        }

        [HttpGet("{id}/equipes")]
        public async Task<ActionResult<ControleMataMata>> GetById(int id)
        {
            ControleMataMata controleMataMata = await _repository.GetControleMataMataByIdAsync(id);

            if (controleMataMata == null)
                return NotFound("Nenhuma equipe foi encontrada para essa etapa mata-mata");

            return Ok(controleMataMata);
        }

        [HttpGet("{idTorneio}/equipes/validando")]
        public async Task<ActionResult<IEnumerable<ControleMataMataDTO>>> SelecionaEquipesEmValidacao(int idTorneio)
        {
            IEnumerable<ControleMataMataDTO> controleMataMata = await _repository.GetControleMataMataEmValidacaoAsync(idTorneio);

            if (controleMataMata == null)
                return NotFound("Nenhuma equipe foi encontrada para essa etapa mata-mata");

            return Ok(controleMataMata);
        }

        [HttpPut("{id}/alterar-status-validacao")]
        public async Task<ActionResult<ControleMataMata>> AlteraStatusValidacao(int id, AtualizaControleMataMataDTO request)
        {
            if (request == null)
                return BadRequest("Dados inválidos");

            ControleMataMata controleExistente = await _repository.GetControleMataMataByIdAsync(id);

            if (controleExistente == null)
                return NotFound("Controle não encontrado");

            if (request.StatusValidacao != null)
                controleExistente.StatusValidacao = request.StatusValidacao;

            await _repository.UpdateAsync(controleExistente);

            return Ok(controleExistente.StatusValidacao);
        }

        [HttpPut("{id}/desclassificar-equipe")]
        public async Task<ActionResult<ControleMataMata>> AlteraStatusValidacao(int id)
        {
            ControleMataMata controleExistente = await _repository.GetControleMataMataByIdAsync(id);

            if (controleExistente == null)
                return NotFound("Controle da equipe não encontrado");

            controleExistente.Equipe.IsDesclassificado = true;

            await _repository.UpdateAsync(controleExistente);

            return Ok(controleExistente);
        }

        [HttpPut("{id}/disputar-terceiro-lugar")]
        public async Task<ActionResult<ControleMataMata>> DisputaTerceiroLugar(int id)
        {
            ControleMataMata controleExistente = await _repository.GetControleMataMataByIdAsync(id);

            if (controleExistente == null)
                return NotFound("Controle da equipe não encontrado");

            controleExistente.DisputaTerceiroLugar = true;
            controleExistente.Equipe.IsDesclassificado = false;

            await _repository.UpdateAsync(controleExistente);

            return Ok(controleExistente);
        }


        [HttpPost("{idTorneio}/preparar-etapa-mata-mata")]
        public async Task<ActionResult<List<ControleMataMataDTO>>> PreparaEtapaMataMata(int idTorneio)
        {
            IEnumerable<ControleMataMataDTO> controleMataMata = await _repository.GetControleMataMataAsync(idTorneio);

            var grupos = controleMataMata.Select((equipe, index) => new { Index = index, Equipe = equipe })
                        .GroupBy(x => x.Index / 2)
                        .Select(grupo => grupo.Select(x => x.Equipe).ToList())
                        .ToList();

            await _repository.ZeraStatusValidacao(controleMataMata);

            var sementeRodadas = await _sementeRodadaRepository.GetAllSementeRodadaAsync();

            if (grupos.Count() == 1)
            {

                Rodada rodada = new Rodada
                {
                    Titulo = "Disputa terceiro lugar",
                    TorneioId = idTorneio,
                    SementeRodadas = new List<SementeRodada>(),
                };

                rodada.SementeRodadas.Add(new SementeRodada
                {
                    Equipes = []
                });

                await _repository.AddAsync(rodada);
            }

            foreach (var item in controleMataMata)
            {
                var gruposPorRodadaId = sementeRodadas
                        .GroupBy(sementeRodada => sementeRodada.RodadaId)
                        .Where(grupo => grupo.Count() >= 1);

                if (grupos.Count() == 4)
                {

                    gruposPorRodadaId = gruposPorRodadaId.Take(1);

                }
                else if (grupos.Count() == 2)
                {
                    gruposPorRodadaId = gruposPorRodadaId.Skip(1).Take(1);
                }
                else if (grupos.Count() == 1)
                {
                    gruposPorRodadaId = gruposPorRodadaId.Skip(2).Take(1);
                }

                // selecionar individualmente todas as etapas e fazer verificação pra processar.
                foreach (var sementeRodada in gruposPorRodadaId)
                {
                    int currentIndex = 0;

                    foreach (var semente in sementeRodada)
                    {
                        var sementeId = semente.Id;

                        for (int i = currentIndex; i < grupos.Count; i++)
                        {
                            var grupo = grupos[i];
                            var primeiroElemento = grupo[0].Equipe.Id;
                            var segundoElemento = grupo[1].Equipe.Id;

                            await _repository.PreparaEtapaMataMata(primeiroElemento, semente.Id);
                            await _repository.PreparaEtapaMataMata(segundoElemento, semente.Id);

                            currentIndex = i + 1;
                            break;
                        }
                    }
                }
            }

            return Ok(sementeRodadas);
        }

        [HttpPost("{idTorneio}/preparar-disputa-terceiro-lugar")]
        public async Task<ActionResult<List<ControleMataMataDTO>>> PreparaDisputaTerceiroLugar(int idTorneio)
        {
            var novoTesteSementeRodadas = await _sementeRodadaRepository.GetAllSementeRodadaAsync();

            var disputaPorTerceiro = novoTesteSementeRodadas.Last();

            IEnumerable<ControleMataMata> DisputaTerceiroLugar = await _repository.GetDisputaTerceiroLugarAsync(idTorneio);
            var disputaTerceiroLugarList = DisputaTerceiroLugar.ToList();

            var selecionaPrimeiro = disputaTerceiroLugarList[0].Equipe.Id;
            var selecionaSegundo = disputaTerceiroLugarList[1].Equipe.Id;

            await _repository.ReclassificaEquipeTerceiroLugar(DisputaTerceiroLugar);

            await _repository.PreparaEtapaMataMata(selecionaPrimeiro, disputaPorTerceiro.Id);
            await _repository.PreparaEtapaMataMata(selecionaSegundo, disputaPorTerceiro.Id);


            return Ok(DisputaTerceiroLugar);
        }



    }
}

