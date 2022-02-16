using ApiFinance.Data.Contracts;
using ApiFinance.Domain.Entities.DataBase;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;

namespace ApiFinance.Data.Repositories
{
    public class CategoryRepository : BaseRepositoy<Category>, ICategoryRepository
    {
        public CategoryRepository(IDataContext dataContext, IConfiguration configuration) : base(dataContext, configuration)
        {
        }

        public int Delete(int id)
        {
            var query = $@"DELETE FROM tb_category WHERE ID = {ParamSymbol}Id";

            var param = new DynamicParameters();
            param.Add(name: "Id", value: id, direction: ParameterDirection.Input);
            var result = DataContext.DataConnection.Execute(
                sql: query,
                param: param,
                transaction: DataContext.DbTransaction);
            return result;
        }

        public IEnumerable<Category> GetAll()
        {
            var query = $@"
                SELECT
                    CAT.ID AS Id,
                    CAT.TYPE_ID AS TypeId,
                    TYPE.NAME AS TypeName,
                    CAT.NAME AS Name
                FROM tb_category CAT
                INNER JOIN tb_movement_type TYPE
                ON CAT.TYPE_ID = TYPE.ID";

            var result = DataContext.DataConnection.Query<Category>(
                sql: query,
                transaction: DataContext.DbTransaction);
            return result;
        }

        public Category GetById(int id)
        {
            var query = $@"
                SELECT
                    CAT.ID AS Id,
                    CAT.TYPE_ID AS TypeId,
                    TYPE.NAME AS TypeName,
                    CAT.NAME AS Name
                FROM tb_category CAT
                INNER JOIN tb_movement_type TYPE
                ON CAT.TYPE_ID = TYPE.ID
                WHERE CAT.ID = {ParamSymbol}Id";

            var param = new DynamicParameters();
            param.Add(name: "Id", value: id, direction: ParameterDirection.Input);
            var result = DataContext.DataConnection.QuerySingleOrDefault<Category>(
                sql: query,
                param: param,
                transaction: DataContext.DbTransaction);
            return result;
        }

        public IEnumerable<Category> GetByType(int typeId)
        {
            var query = $@"
                SELECT
                    CAT.ID AS Id,
                    CAT.TYPE_ID AS TypeId,
                    TYPE.NAME AS TypeName,
                    CAT.NAME AS Name
                FROM tb_category CAT
                INNER JOIN tb_movement_type TYPE
                ON CAT.TYPE_ID = TYPE.ID
                WHERE CAT.TYPE_ID = {ParamSymbol}Type_Id";

            var param = new DynamicParameters();
            param.Add(name: "Type_Id", value: typeId, direction: ParameterDirection.Input);
            var result = DataContext.DataConnection.Query<Category>(
                sql: query,
                param: param,
                transaction: DataContext.DbTransaction);
            return result;
        }

        public Category GetByTypeIdName(int typeId, string name)
        {
            var query = $@"
                SELECT
                    CAT.ID AS Id,
                    CAT.TYPE_ID AS TypeId,
                    TYPE.NAME AS TypeName,
                    CAT.NAME AS Name
                FROM tb_category CAT
                INNER JOIN tb_movement_type TYPE
                ON CAT.TYPE_ID = TYPE.ID
                WHERE CAT.TYPE_ID = {ParamSymbol}Type_Id
                AND CAT.NAME LIKE {ParamSymbol}Name";

            var param = new DynamicParameters();
            param.Add(name: "Type_Id", value: typeId, direction: ParameterDirection.Input);
            param.Add(name: "Name", value: name, direction: ParameterDirection.Input);

            var result = DataContext.DataConnection.QueryFirstOrDefault<Category>(
                sql: query,
                param: param,
                transaction: DataContext.DbTransaction
                );

            return result;
        }

        public int Insert(Category category)
        {
            var query = $@"
                INSERT INTO tb_category
                (
                    NAME,
                    TYPE_ID                   
                )
                VALUES 
                (
                    {ParamSymbol}Name,
                    {ParamSymbol}Type_Id)";

            var param = new DynamicParameters();
            param.Add(name: "Type_Id", value: category.TypeId, direction: ParameterDirection.Input);
            param.Add(name: "Name", value: category.Name, direction: ParameterDirection.Input);
            var result = DataContext.DataConnection.Execute(
                sql: query,
                param: param,
                transaction: DataContext.DbTransaction);
            return result;
        }

        public int Update(Category category)
        {
            var query = $@"
                UPDATE tb_category SET
                    NAME = {ParamSymbol}Name
                WHERE ID = {ParamSymbol}Id";

            var param = new DynamicParameters();
            param.Add(name: "Id", value: category.Id, direction: ParameterDirection.Input);
            param.Add(name: "Name", value: category.Name, direction: ParameterDirection.Input);
            var result = DataContext.DataConnection.Execute(
                sql: query,
                param: param,
                transaction: DataContext.DbTransaction);
            return result;
        }
    }
}