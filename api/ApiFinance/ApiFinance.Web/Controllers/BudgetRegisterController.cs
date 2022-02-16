using ApiFinance.App.Contracts.Services;
using ApiFinance.Domain.ClientReponse;
using ApiFinance.Domain.Entities.DataBase;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ApiFinance.Web.Controllers
{
    /// <summary>
    /// Controler BudgetRegister
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BudgetRegisterController : Controller
    {
        private readonly IBudgetRegisterService _iBudgetRegisterService;

        /// <summary>
        /// Construtor da classe BudgetRegisterController
        /// </summary>
        /// <param name="iBudgetRegisterService"></param>
        public BudgetRegisterController(IBudgetRegisterService iBudgetRegisterService) => 
            _iBudgetRegisterService = iBudgetRegisterService;

        /// <summary>
        /// Remove um registro
        /// </summary>
        /// <param name="id">ID do registro</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("delete/{id}")]
        public IActionResult Delete(int id)
        {
            var result = _iBudgetRegisterService.Delete(id);
            if (result != 0)
                return Ok(new DefaultResponse { Result = "OK", ResultObject = true });
            return Ok(new DefaultResponse { Result = "OK", ResultObject = false });
        }

        /// <summary>
        /// Retorna todos os registros cadastrados
        /// </summary>
        /// <remarks>
        /// Exemplo do objeto retornado:
        ///
        ///{
        ///    "exception": null,
        ///    "message": null,
        ///    "result": "OK",
        ///    "resultObject": [
        ///        {
        ///            "categoryId": 1,
        ///            "categoryName": "string",
        ///            "dtRegister": "2022-01-22T15:43:29",
        ///            "description": "string",
        ///            "dtDue": "2022-03-20T00:00:00",
        ///            "note": "string",
        ///            "paymentAccountId": 0,
        ///            "paymentStatus": 1,
        ///            "subCategoryId": 1,
        ///            "subCategoryName": "string",
        ///            "typeId": 1,
        ///            "typeName": "string",
        ///            "value": 0,
        ///            "id": 1
        ///        }
        ///    ],
        ///    "status": null
        ///}
        /// </remarks>
        [HttpGet]
        [Route("getAll")]
        public IActionResult GetAll()
        {
            var result = _iBudgetRegisterService.GetAll();
            if(result != null && result.Any())
                return Ok(new DefaultResponse { Result = "OK", ResultObject = result });
            return Ok(new DefaultResponse { Result = "OK"});
        }

        /// <summary>
        /// Consulta um registro pelo ID
        /// </summary>
        /// <param name="id">ID do registro</param>
        /// <remarks>
        /// Exemplo do objeto retornado:
        ///
        ///{
        ///    "exception": null,
        ///    "message": null,
        ///    "result": "OK",
        ///    "resultObject": [
        ///        {
        ///            "categoryId": 1,
        ///            "categoryName": "string",
        ///            "dtRegister": "2022-01-22T15:43:29",
        ///            "description": "string",
        ///            "dtDue": "2022-03-20T00:00:00",
        ///            "note": "string",
        ///            "paymentAccountId": 0,
        ///            "paymentStatus": 1,
        ///            "subCategoryId": 1,
        ///            "subCategoryName": "string",
        ///            "typeId": 1,
        ///            "typeName": "string",
        ///            "value": 0,
        ///            "id": 1
        ///        }
        ///    ],
        ///    "status": null
        ///}
        /// </remarks>
        [HttpGet]
        [Route("getBYId/{id}")]
        public IActionResult GetById(int id)
        {
            var result = _iBudgetRegisterService.GetById(id);
            if (result != null)
                return Ok(new DefaultResponse { Result = "OK", ResultObject = result });
            return Ok(new DefaultResponse { Result = "OK" });
        }

        /// <summary>
        /// Retorna os registros por Mês e por tipo de Movimento
        /// </summary>
        /// <param name="year">Ano de registro</param>
        /// <param name="month">Mê de registro</param>
        /// <param name="movementId">Código do tipo de Movimento Ex:(1-Despesa / 2-Receita / 3-Transfência entre contas)  
        /// </param>
        /// <remarks>
        /// Exemplo do objeto retornado:
        ///
        ///{
        ///    "exception": null,
        ///    "message": null,
        ///    "result": "OK",
        ///    "resultObject": [
        ///        {
        ///            "categoryId": 1,
        ///            "categoryName": "string",
        ///            "dtRegister": "2022-01-22T15:43:29",
        ///            "description": "string",
        ///            "dtDue": "2022-03-20T00:00:00",
        ///            "note": "string",
        ///            "paymentAccountId": 0,
        ///            "paymentStatus": 1,
        ///            "subCategoryId": 1,
        ///            "subCategoryName": "string",
        ///            "typeId": 1,
        ///            "typeName": "string",
        ///            "value": 0,
        ///            "id": 1
        ///        }
        ///    ],
        ///    "status": null
        ///}
        /// </remarks>
        [HttpGet]
        [Route("getByMonthYear/{year}/{month}/{movementId}")]
        public IActionResult GetByMonthYear(string year, string month, int movementId)
        {
            var result = _iBudgetRegisterService.GetByMonthYear(year, month, movementId);
            if (result.Any())
                return Ok(new DefaultResponse { Result = "OK", ResultObject = result });
            return Ok(new DefaultResponse { Result = "OK"});
        }

        /// <summary>
        /// Retorna todos os regitros de um tipo de movimento
        /// </summary>
        /// <param name="movementId">Código do tipo de Movimento
        ///  Exemplo:
        ///     1 - Despesa
        ///     2 - Receita
        ///     3 - Transfência entre contas
        /// </param>
        /// <remarks>
        /// Exemplo do objeto retornado:
        ///
        ///{
        ///    "exception": null,
        ///    "message": null,
        ///    "result": "OK",
        ///    "resultObject": [
        ///        {
        ///            "categoryId": 1,
        ///            "categoryName": "string",
        ///            "dtRegister": "2022-01-22T15:43:29",
        ///            "description": "string",
        ///            "dtDue": "2022-03-20T00:00:00",
        ///            "note": "string",
        ///            "paymentAccountId": 0,
        ///            "paymentStatus": 1,
        ///            "subCategoryId": 1,
        ///            "subCategoryName": "string",
        ///            "typeId": 1,
        ///            "typeName": "string",
        ///            "value": 0,
        ///            "id": 1
        ///        }
        ///    ],
        ///    "status": null
        ///}
        /// </remarks>
        [HttpGet]
        [Route("getByMovementId/{movementId}")]
        public IActionResult GetByMovementId(int movementId)
        {
            var result = _iBudgetRegisterService.GetByMovementId(movementId);
            if (result.Any())
                return Ok(new DefaultResponse { Result = "OK", ResultObject = result });
            return Ok(new DefaultResponse { Result = "OK"});
        }

        /// <summary>
        /// Retorna os registros pela dercrição
        /// </summary>
        /// <param name="description">Descrição do registro</param>
        /// <param name="movementId">Código do tipo de movimento</param>
        /// <remarks>
        /// Exemplo do objeto retornado:
        ///
        ///{
        ///    "exception": null,
        ///    "message": null,
        ///    "result": "OK",
        ///    "resultObject": [
        ///        {
        ///            "categoryId": 1,
        ///            "categoryName": "string",
        ///            "dtRegister": "2022-01-22T15:43:29",
        ///            "description": "string",
        ///            "dtDue": "2022-03-20T00:00:00",
        ///            "note": "string",
        ///            "paymentAccountId": 0,
        ///            "paymentStatus": 1,
        ///            "subCategoryId": 1,
        ///            "subCategoryName": "string",
        ///            "typeId": 1,
        ///            "typeName": "string",
        ///            "value": 0,
        ///            "id": 1
        ///        }
        ///    ],
        ///    "status": null
        ///}
        /// </remarks>
        [HttpGet]
        [Route("getByDescription/{description}/{movementId}")]
        public IActionResult GetByDescription(string description, int movementId)
        {
            var result = _iBudgetRegisterService.GetByDescription(description, movementId);
            if (result.Any())
                return Ok(new DefaultResponse { Result = "OK", ResultObject = result });
            return Ok(new DefaultResponse { Result = "OK"});
        }

        /// <summary>
        /// Retorna o relatório referente ao resumo mensal
        /// </summary>
        /// <param name="year">Ano do relatório</param>
        /// <param name="month">Mês do relatório</param>
        /// <remarks>
        /// Exemplo do objeto retornado:
        /// {
        ///     "exception": null,
        ///      "message": null,
        ///      "result": "OK",
        ///      "resultObject": {
        ///        "totalRevenue": 0,
        ///        "totalExpense": 0,
        ///        "finalBalance": 0,
        ///        "amountSpentByCategory": [
        ///          {
        ///            "idCategory": 0,
        ///            "nomeCategory": "string",
        ///            "amountCategory": 0
        ///          },
        ///          {
        ///            "idCategory": 0,
        ///            "nomeCategory": "string",
        ///            "amountCategory": 0
        ///          }
        ///        ]
        ///      },
        ///      "status": null
        ///}
        /// </remarks>
        [HttpGet]
        [Route("getBudgetSummary/{year}/{month}")]
        public IActionResult GetBudgetSummary(string year, string month)
        {
            var result = _iBudgetRegisterService.GetBudgetSummary(year, month);
            if (result != null)
                return Ok(new DefaultResponse { Result = "OK", ResultObject = result });
            return Ok(new DefaultResponse { Result = "OK" });
        }

        /// <summary>
        /// Insere um registro
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("insert")]
        public IActionResult Insert(BudgetRegister request)
        {
            var result = _iBudgetRegisterService.Insert(request);
            if (result != 0)
                return Ok(new DefaultResponse { Result = "OK", ResultObject = true});
            return Ok(new DefaultResponse { Result = "OK", ResultObject = false });
        }
    }
}