using Contatos.CreateProducer.DTO;
using Microsoft.AspNetCore.Mvc;
using MassTransit;
using Serilog;
using Contatos.DataContracts.Commands;

namespace Contatos.CreateProducer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CadastroContatoController : ControllerBase
    {
        private readonly IBus _bus;

        public CadastroContatoController(IBus bus)
        {
            _bus = bus;
        }

        /// <summary>
        /// Endpoint para verificar a disponibilidade do servi�o.
        /// </summary>
        [HttpGet]
        public ActionResult HealthCheck()
        {
            return Ok("Servi�o online.");
        }

        /// <summary>
        /// Endpoint para cadastrar um novo contato.
        /// </summary>
        /// <param name="input">Forne�a o nome completo, DDD, nr. de telefone e opcionalmente um endere�o de e-mail do contato.</param>
        [HttpPost]
        public async Task<IActionResult> CadastrarContato([FromBody] NovoContato input)
        {
            try
            {
                await _bus.Send(new CadastrarContato
                {
                    CommandId = Guid.NewGuid(),
                    TimeStamp = DateTime.Now,
                    NomeCompleto = input.NomeCompleto,
                    DDD = input.DDD,
                    Telefone = input.Telefone,
                    Email = input.Email
                });
                return Ok("Contato recepcionado com �xito.");
            }
            catch (Exception ex)
            {
                Log.Error($"POST para cadastro de contato falhou. Exception: {ex.GetType()}. Message: {ex.Message}.");
                return BadRequest(ex.Message);
            }
        }
    }
}
