using ApiFinance.App.Contracts.Services;
using ApiFinance.Domain.ClientReponse;
using ApiFinance.Domain.Entities.DataBase;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ApiFinance.Web.Controllers
{
    /// <summary>
    /// Controller SubCategory
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class SubCategoryController : Controller
    {
        private readonly ISubCategoryService _iSubCategoryService;

        /// <summary>
        /// Construtor da classe SubCategoryController
        /// </summary>
        /// <param name="iSubCategoryService"></param>
        public SubCategoryController(ISubCategoryService iSubCategoryService) => _iSubCategoryService = iSubCategoryService;

        /// <summary>
        /// Deleta uma subcategoria pelo ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("delete/{id}")]
        public IActionResult Delete(int id)
        {
            var result = _iSubCategoryService.Delete(id);
            if (result != 0)
                return Ok(new DefaultResponse { Result = "OK", ResultObject = true});
            return Ok(new DefaultResponse { Result = "OK", ResultObject = false});
        }

        /// <summary>
        /// Retorna todas as subcaterogias cadastradas
        /// </summary>
        /// <remarks>
        /// Exemplo do objeto retornado:
        /// </remarks>
        [HttpGet]
        [Route("getAll")]
        public IActionResult GetAll()
        {
            var result = _iSubCategoryService.GetAll();
            if (result != null && result.Any())
                return Ok(new DefaultResponse { Result = "OK", ResultObject = result });
            return Ok(new DefaultResponse { Result = "OK"});
        }

        /// <summary>
        /// Retona todas as subcategorias cadastradas pelo tipo de categoria informado
        /// </summary>
        /// <param name="categoryId"></param>
        /// <remarks>
        /// Exemplo do objeto retornado:
        /// </remarks>
        [HttpGet]
        [Route("getByCategoryId/{categoryId}")]
        public IActionResult GetByCategoryId(int categoryId)
        {
            var result = _iSubCategoryService.GetByCategoryId(categoryId);
            if (result != null && result.Any())
                return Ok(new DefaultResponse { Result = "OK", ResultObject = result });
            return Ok(new DefaultResponse { Result = "OK" });
        }


        /// <summary>
        /// Retorna uma subcategoria pelo ID
        /// </summary>
        /// <param name="id"></param>
        /// <remarks>
        /// Exemplo do objeto retornado:
        /// </remarks>
        [HttpGet]
        [Route("getById/{id}")]
        public IActionResult GetById(int id)
        {
            var result = _iSubCategoryService.GetById(id);
            if (result != null)
                return Ok(new DefaultResponse { Result = "OK", ResultObject = result });
            return Ok(new DefaultResponse { Result = "OK"});
        }


        /// <summary>
        /// Cadastra uma subcategoria
        /// </summary>
        /// <param name="subcategory"></param>
        /// <remarks>
        /// Exemplo do objeto retornado:
        /// </remarks>
        [HttpPost]
        [Route("insert")]
        public IActionResult Insert(SubCategory subcategory)
        {
            var result = _iSubCategoryService.Insert(subcategory);
            if (result > 0)
                return Ok(new DefaultResponse { Result = "OK", ResultObject = true });
            return Ok(new DefaultResponse { Result = "OK", ResultObject = false });
        }

        /// <summary>
        /// Altera uma subcategoria
        /// </summary>
        /// <param name="subcategory"></param>
        /// <remarks>
        /// Exemplo do objeto retornado:
        /// </remarks>
        [HttpPut]
        [Route("update")]
        public IActionResult Update(SubCategory subcategory)
        {
            var result = _iSubCategoryService.Update(subcategory);
            if (result > 0)
                return Ok(new DefaultResponse { Result = "OK", ResultObject = true });
            return Ok(new DefaultResponse { Result = "OK", ResultObject = false});
        }
    }
}