using ApiFinance.App.Contracts.Services;
using ApiFinance.Domain.ClientReponse;
using ApiFinance.Domain.Entities.DataBase;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ApiFinance.Web.Controllers
{
    /// <summary>
    /// Controller Account
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AccountController : Controller
    {
        private readonly IAccountService _iAccountService;

        /// <summary>
        /// Construtor da classe AccountController
        /// </summary>
        /// <param name="iAccountService"></param>
        public AccountController(IAccountService iAccountService) => _iAccountService = iAccountService;

        /// <summary>
        /// Deleta uma conta pelo ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("delete/{id}")]
        public IActionResult Delete(int id)
        {
            var result = _iAccountService.Delete(id);
            if (result != 0)
                return Ok(new DefaultResponse { Result = "OK", ResultObject = true });
            return Ok(new DefaultResponse { Result = "OK", ResultObject = false });
        }

        /// <summary>
        /// Retorna todas as contas cadastradas
        /// </summary>
        /// <remarks>
        /// Exemplo do objeto retornado:
        /// </remarks>
        [HttpGet]
        [Route("getAll")]
        public IActionResult GetAll()
        {
            var result = _iAccountService.GetAll();
            if (result != null && result.Any())
                return Ok(new DefaultResponse { Result = "OK", ResultObject = result });
            return Ok(new DefaultResponse { Result = "OK" });
        }

        /// <summary>
        /// Retorna uma conta pelo ID
        /// </summary>
        /// <param name="id"></param>
        /// <remarks>
        /// Exemplo do objeto retornado:
        /// </remarks>
        [HttpGet]
        [Route("getById/{id}")]
        public IActionResult GetById(int id)
        {
            var result = _iAccountService.GetById(id);
            if (result != null)
                return Ok(new DefaultResponse { Result = "OK", ResultObject = result });
            return Ok(new DefaultResponse { Result = "OK" });
        }

        /// <summary>
        /// Cadastra uma conta
        /// </summary>
        /// <param name="account"></param>
        /// <remarks>
        /// Exemplo do objeto retornado:
        /// </remarks>
        [HttpPost]
        [Route("insert")]
        public IActionResult Insert(Account account)
        {
            var result = _iAccountService.Insert(account);
            if (result > 0)
                return Ok(new DefaultResponse { Result = "OK", ResultObject = true });
            return Ok(new DefaultResponse { Result = "OK" , ResultObject = false});
        }

        /// <summary>
        /// Altera uma conta
        /// </summary>
        /// <param name="account"></param>
        /// <remarks>
        /// Exemplo do objeto retornado:
        /// </remarks>
        [HttpPut]
        [Route("update")]
        public IActionResult Update(Account account)
        {
            var result = _iAccountService.Update(account);
            if (result > 0)
                return Ok(new DefaultResponse { Result = "OK", ResultObject = true });
            return Ok(new DefaultResponse { Result = "OK", ResultObject = false });
        }
    }
}