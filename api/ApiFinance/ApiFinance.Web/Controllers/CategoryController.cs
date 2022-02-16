using ApiFinance.App.Contracts.Services;
using ApiFinance.Domain.ClientReponse;
using ApiFinance.Domain.Entities.DataBase;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ApiFinance.Web.Controllers
{
    /// <summary>
    /// Controller Category
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _iCategoryService;

        /// <summary>
        /// Construtor da classe CategoryController
        /// </summary>
        /// <param name="iCategoryService"></param>
        public CategoryController(ICategoryService iCategoryService) => _iCategoryService = iCategoryService;

        /// <summary>
        /// Deleta uma categoria pelo ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("delete/{id}")]
        public IActionResult Delete(int id)
        {
            var result = _iCategoryService.Delete(id);
            if (result != 0)
                return Ok(new DefaultResponse { Result = "OK", ResultObject = true });      
            return Ok(new DefaultResponse { Result = "OK", ResultObject = false });
        }

        /// <summary>
        /// Retorna todas as caterogias cadastradas
        /// </summary>
        /// <remarks>
        /// Exemplo do objeto retornado:
        /// </remarks>
        [HttpGet]
        [Route("getAll")]
        public IActionResult GetAll()
        {
            var result = _iCategoryService.GetAll();
            if (result != null && result.Any())
                return Ok(new DefaultResponse { Result = "OK", ResultObject = result });
            return Ok(new DefaultResponse { Result = "OK" });
        }

        /// <summary>
        /// Retorna uma categoria pelo ID
        /// </summary>
        /// <param name="id"></param>
        /// <remarks>
        /// Exemplo do objeto retornado:
        /// </remarks>
        [HttpGet]
        [Route("getById/{id}")]
        public IActionResult GetById(int id)
        {
            var result = _iCategoryService.GetById(id);
            if (result != null)
                return Ok(new DefaultResponse { Result = "OK", ResultObject = result });
            return Ok(new DefaultResponse { Result = "OK"});
        }

        /// <summary>
        /// Retonas todas as categorias cadastradas pelo tipo informado
        /// </summary>
        /// <param name="typeId"></param>
        /// <remarks>
        /// Exemplo do objeto retornado:
        /// </remarks>
        [HttpGet]
        [Route("getByType/{typeId}")]
        public IActionResult GetByType(int typeId)
        {
            var result = _iCategoryService.GetByType(typeId);
            if (result != null && result.Any())
                return Ok(new DefaultResponse { Result = "OK", ResultObject = result });
            return Ok(new DefaultResponse { Result = "OK" });
        }

        /// <summary>
        /// Cadastra uma categoria
        /// </summary>
        /// <param name="category"></param>
        /// <remarks>
        /// Exemplo do objeto retornado:
        /// </remarks>
        [HttpPost]
        [Route("insert")]
        public IActionResult Insert(Category category)
        {
            var result = _iCategoryService.Insert(category);
            if(result > 0)
                return Ok(new DefaultResponse { Result = "OK", ResultObject = true});
            return Ok(new DefaultResponse { Result = "OK" , ResultObject = false});
        }

        /// <summary>
        /// Altera uma categoria
        /// </summary>
        /// <param name="category"></param>
        /// <remarks>
        /// Exemplo do objeto retornado:
        /// </remarks>
        [HttpPut]
        [Route("update")]
        public IActionResult Update(Category category)
        {
            var result = _iCategoryService.Update(category);
            if (result > 0)
                return Ok(new DefaultResponse { Result = "OK", ResultObject = true});
            return Ok(new DefaultResponse { Result = "OK", ResultObject = false});
        }
    }
}