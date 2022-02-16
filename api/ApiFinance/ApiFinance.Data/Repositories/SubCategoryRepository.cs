using ApiFinance.Data.Contracts;
using ApiFinance.Domain.Entities.DataBase;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;

namespace ApiFinance.Data.Repositories
{
    public class SubCategoryRepository : BaseRepositoy<SubCategory>, ISubCategoryRepository
    {
        public SubCategoryRepository(IDataContext dataContext, IConfiguration configuration) : base(dataContext, configuration)
        {
        }

        public int Delete(int id)
        {
            var query = $@"DELETE FROM tb_subcategory WHERE ID = {ParamSymbol}Id";

            var param = new DynamicParameters();
            param.Add(name: "Id", value: id, direction: ParameterDirection.Input);
            var result = DataContext.DataConnection.Execute(
                sql: query,
                param: param,
                transaction: DataContext.DbTransaction);
            return result;
        }

        public IEnumerable<SubCategory> GetAll()
        {
            var query = $@"
                SELECT
                    SUBCAT.ID AS Id,
                    SUBCAT.NAME AS Name,
                    SUBCAT.CATEGORY_ID AS CategoryId,                   
                    CAT.NAME AS CategoryName
                FROM tb_subcategory SUBCAT
                INNER JOIN tb_category CAT
                ON SUBCAT.CATEGORY_ID = CAT.ID";

            var result = DataContext.DataConnection.Query<SubCategory>(
                sql: query,
                transaction: DataContext.DbTransaction);
            return result;
        }

        public IEnumerable<SubCategory> GetByCategoryId(int categoryId)
        {
            var query = $@"
                SELECT
                    SUBCAT.ID AS Id,
                    SUBCAT.NAME AS Name,
                    SUBCAT.CATEGORY_ID AS CategoryId,                   
                    CAT.NAME AS CategoryName
                FROM tb_subcategory SUBCAT
                INNER JOIN tb_category CAT
                ON SUBCAT.CATEGORY_ID = CAT.ID
                WHERE SUBCAT.CATEGORY_ID = {ParamSymbol}Category_Id";

            var param = new DynamicParameters();
            param.Add(name: "Category_Id", value: categoryId, direction: ParameterDirection.Input);
            var result = DataContext.DataConnection.Query<SubCategory>(
                sql: query,
                param: param,
                transaction: DataContext.DbTransaction);
            return result;
        }

        public SubCategory GetById(int id)
        {
            var query = $@"
                SELECT
                    SUBCAT.ID AS Id,
                    SUBCAT.NAME AS Name,
                    SUBCAT.CATEGORY_ID AS CategoryId,                   
                    CAT.NAME AS CategoryName
                FROM tb_subcategory SUBCAT
                INNER JOIN tb_category CAT
                ON SUBCAT.CATEGORY_ID = CAT.ID
                WHERE SUBCAT.ID = {ParamSymbol}Id";

            var param = new DynamicParameters();
            param.Add(name: "Id", value: id, direction: ParameterDirection.Input);
            var result = DataContext.DataConnection.QueryFirstOrDefault<SubCategory>(
                sql: query,
                param: param,
                transaction: DataContext.DbTransaction);
            return result;
        }

        public int Insert(SubCategory subCategory)
        {
            var query = $@"
                INSERT INTO tb_subcategory
                (
                    NAME,
                    CATEGORY_ID                   
                )
                VALUES 
                (
                    {ParamSymbol}Name,
                    {ParamSymbol}Category_Id)";

            var param = new DynamicParameters();
            param.Add(name: "Name", value: subCategory.Name, direction: ParameterDirection.Input);
            param.Add(name: "Category_Id", value: subCategory.CategoryId, direction: ParameterDirection.Input);
            var result = DataContext.DataConnection.Execute(
                sql: query,
                param: param,
                transaction: DataContext.DbTransaction);
            return result;
        }

        public int Update(SubCategory subCategory)
        {
            var query = $@"
                UPDATE tb_subcategory SET
                    NAME = {ParamSymbol}Name
                WHERE ID = {ParamSymbol}Id";

            var param = new DynamicParameters();
            param.Add(name: "Name", value: subCategory.Name, direction: ParameterDirection.Input);
            param.Add(name: "Id", value: subCategory.Id, direction: ParameterDirection.Input);
            var result = DataContext.DataConnection.Execute(
                sql: query,
                param: param,
                transaction: DataContext.DbTransaction);
            return result;
        }
    }
}