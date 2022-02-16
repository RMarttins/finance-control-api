using ApiFinance.App.Contracts.Services;
using ApiFinance.Domain.ClientReponse;
using ApiFinance.Domain.Entities.DataBase;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ApiFinance.Web.Controllers
{
    /// <summary>
    /// Controller CreditCard
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CreditCardController : Controller
    {
        private readonly ICreditCardService _iCreditCardService;

        /// <summary>
        /// Construtor da classe CreditCardController
        /// </summary>
        /// <param name="iCreditCardService"></param>
        public CreditCardController(ICreditCardService iCreditCardService) => _iCreditCardService = iCreditCardService;

        /// <summary>
        /// Deleta uma cartão pelo ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("delete/{id}")]
        public IActionResult Delete(int id)
        {
            var result = _iCreditCardService.Delete(id);
            if (result != 0)
                return Ok(new DefaultResponse { Result = "OK", ResultObject = true });
            return Ok(new DefaultResponse { Result = "OK", ResultObject = false });
        }

        /// <summary>
        /// Retorna todos os cartões cadastrados
        /// </summary>
        /// <remarks>
        /// Exemplo do objeto retornado:
        /// </remarks>
        [HttpGet]
        [Route("getAll")]
        public IActionResult GetAll()
        {
            var result = _iCreditCardService.GetAll();
            if (result != null && result.Any())
                return Ok(new DefaultResponse { Result = "OK", ResultObject = result });
            return Ok(new DefaultResponse { Result = "OK" });
        }

        /// <summary>
        /// Retorna um cartão pelo ID
        /// </summary>
        /// <param name="id"></param>
        /// <remarks>
        /// Exemplo do objeto retornado:
        /// </remarks>
        [HttpGet]
        [Route("getById/{id}")]
        public IActionResult GetById(int id)
        {
            var result = _iCreditCardService.GetById(id);
            if (result != null)
                return Ok(new DefaultResponse { Result = "OK", ResultObject = result });
            return Ok(new DefaultResponse { Result = "OK" });
        }

        /// <summary>
        /// Cadastra um cartão
        /// </summary>
        /// <param name="creditCard"></param>
        /// <remarks>
        /// Exemplo do objeto retornado:
        /// </remarks>
        [HttpPost]
        [Route("insert")]
        public IActionResult Insert(CreditCard creditCard)
        {
            var result = _iCreditCardService.Insert(creditCard);
            if (result > 0)
                return Ok(new DefaultResponse { Result = "OK", ResultObject = true });
            return Ok(new DefaultResponse { Result = "OK", ResultObject = false });
        }

        /// <summary>
        /// Altera um cartão cadastrado
        /// </summary>
        /// <param name="creditCard"></param>
        /// <remarks>
        /// Exemplo do objeto retornado:
        /// </remarks>
        [HttpPut]
        [Route("update")]
        public IActionResult Update(CreditCard creditCard)
        {
            var result = _iCreditCardService.Update(creditCard);
            if (result > 0)
                return Ok(new DefaultResponse { Result = "OK", ResultObject = true });
            return Ok(new DefaultResponse { Result = "OK", ResultObject = false });
        }
    }
}