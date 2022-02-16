using ApiFinance.Data.Contracts;
using ApiFinance.Domain.Entities.DataBase;
using ApiFinance.Domain.Reports;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace ApiFinance.Data.Repositories
{
    public class BudgetRegisterRepository : BaseRepositoy<BudgetRegister>, IBudgetRegisterRepository
    {
        public BudgetRegisterRepository(IDataContext dataContext, IConfiguration configuration) : base(dataContext, configuration)
        {
        }

        public int Delete(int id)
        {
            var query = $@"
                DELETE FROM tb_budget_register
                WHERE ID = {ParamSymbol}Id";

            var parameters = new DynamicParameters();
            parameters.Add(name: "Id", value: id, direction: ParameterDirection.Input);

            var result = DataContext.DataConnection.Execute(
                sql: query,
                param: parameters,
                transaction: DataContext.DbTransaction);
            
            return result;
        }

        public IEnumerable<BudgetRegister> GetAll()
        {
            var query = $@"
                SELECT 
                    BDR.ID AS Id
                    ,BDR.TYPE_ID AS TypeId
			        ,MOVT.NAME AS TypeName
                    ,BDR.CATEGORY_ID AS CategoryId
                    ,CAT.NAME AS CategoryName
                    ,BDR.SUBCATEGORY_ID AS SubCategoryId
                    ,SUBCAT.NAME AS SubCategoryName
                    ,BDR.VALUE AS Value
                    ,BDR.DESCRIPTION AS Description
                    ,BDR.DT_REGISTER AS DtRegister
                    ,BDR.DT_DUE AS DtDue
                    ,BDR.NOTE AS Note
                    ,BDR.PAYMENT_STATUS AS PaymentStatus
                    ,BDR.PAYMENT_ACCOUNT_ID AS PaymentAccountId
                FROM tb_budget_register BDR
                LEFT JOIN tb_movement_type  MOVT
                ON BDR.TYPE_ID = MOVT.ID
                LEFT JOIN tb_category CAT
                ON BDR.CATEGORY_ID = CAT.ID
                LEFT JOIN tb_subcategory SUBCAT
                ON BDR.SUBCATEGORY_ID = SUBCAT.ID
				ORDER BY BDR.DT_REGISTER";

            var result = DataContext.DataConnection.Query<BudgetRegister>(
                sql: query,
                transaction: DataContext.DbTransaction);

            return result;
        }

        public BudgetSummaryReport GetBudgetSummary(string year, string month)
        {
            var query = $@"
            SELECT
	            (SELECT SUM(VALUE) FROM tb_budget_register 
                WHERE TYPE_ID = 1 AND MONTH(DT_REGISTER) = {ParamSymbol}Month AND YEAR(DT_REGISTER) = {ParamSymbol}Year) TotalExpense,
                (SELECT SUM(VALUE) FROM tb_budget_register 
                WHERE TYPE_ID = 2 AND MONTH(DT_REGISTER) = {ParamSymbol}Month AND YEAR(DT_REGISTER) = {ParamSymbol}Year) TotalRevenue,
	            BDR.CATEGORY_ID AS IdCategory,
                CAT.NAME AS NomeCategory,
                BDR.VALUE AS AmountCategory
            FROM tb_budget_register BDR
            LEFT JOIN tb_category CAT
            ON BDR.CATEGORY_ID = CAT.ID
            WHERE MONTH(BDR.DT_REGISTER) = {ParamSymbol}Month
            AND YEAR(BDR.DT_REGISTER) = {ParamSymbol}Year
            GROUP BY BDR.CATEGORY_ID";

            var param = new DynamicParameters();
            param.Add(name: "Month", value: month, direction: ParameterDirection.Input);
            param.Add(name: "Year", value: year, direction: ParameterDirection.Input);

            var result = DataContext.DataConnection.Query<BudgetSummaryReport, SpentCategoryReport, BudgetSummaryReport>(
                sql: query,
                (budgetSummaryReport, spentCategoryReport) => 
                {
                    if (spentCategoryReport?.IdCategory != null)
                        budgetSummaryReport.AmountSpentByCategory.Add(spentCategoryReport);
                    return budgetSummaryReport;
                },
                splitOn: "IdCategory",
                param: param);

            var grouped = result.GroupBy(d => d.FinalBalance).Select(s =>
            {
                var ret = s.First();
                ret.AmountSpentByCategory = ret.AmountSpentByCategory.Any() ? s.Select(d => d?.AmountSpentByCategory?.Single())?.ToList() : ret.AmountSpentByCategory;
                return ret;
            });

            return grouped.FirstOrDefault();
        }

        public IEnumerable<BudgetRegister> GetByDescription(string description, int movementId)
        {
            description = "%" + description + "%";
            var query = $@"
                SELECT 
                    BDR.ID AS Id
                    ,BDR.TYPE_ID AS TypeId
			        ,MOVT.NAME AS TypeName
                    ,BDR.CATEGORY_ID AS CategoryId
                    ,CAT.NAME AS CategoryName
                    ,BDR.SUBCATEGORY_ID AS SubCategoryId
                    ,SUBCAT.NAME AS SubCategoryName
                    ,BDR.VALUE AS Value
                    ,BDR.DESCRIPTION AS Description
                    ,BDR.DT_REGISTER AS DtRegister
                    ,BDR.DT_DUE AS DtDue
                    ,BDR.NOTE AS Note
                    ,BDR.PAYMENT_STATUS AS PaymentStatus
                    ,BDR.PAYMENT_ACCOUNT_ID AS PaymentAccountId
                FROM tb_budget_register BDR
                LEFT JOIN tb_movement_type  MOVT
                ON BDR.TYPE_ID = MOVT.ID
                LEFT JOIN tb_category CAT
                ON BDR.CATEGORY_ID = CAT.ID
                LEFT JOIN tb_subcategory SUBCAT
                ON BDR.SUBCATEGORY_ID = SUBCAT.ID
                WHERE BDR.DESCRIPTION LIKE {ParamSymbol}Description
                AND BDR.TYPE_ID = {ParamSymbol}Movement_Id";

            var parameters = new DynamicParameters();
            parameters.Add(name: "Description", value: description, direction: ParameterDirection.Input);
            parameters.Add(name: "Movement_Id", value: movementId, direction: ParameterDirection.Input);

            var result = DataContext.DataConnection.Query<BudgetRegister>(
                sql: query,
                param: parameters,
                transaction: DataContext.DbTransaction);

            return result;
        }

        public BudgetRegister GetById(int id)
        {
            var query = $@"
                SELECT 
                    BDR.ID AS Id
                    ,BDR.TYPE_ID AS TypeId
			        ,MOVT.NAME AS TypeName
                    ,BDR.CATEGORY_ID AS CategoryId
                    ,CAT.NAME AS CategoryName
                    ,BDR.SUBCATEGORY_ID AS SubCategoryId
                    ,SUBCAT.NAME AS SubCategoryName
                    ,BDR.VALUE AS Value
                    ,BDR.DESCRIPTION AS Description
                    ,BDR.DT_REGISTER AS DtRegister
                    ,BDR.DT_DUE AS DtDue
                    ,BDR.NOTE AS Note
                    ,BDR.PAYMENT_STATUS AS PaymentStatus
                    ,BDR.PAYMENT_ACCOUNT_ID AS PaymentAccountId
                FROM tb_budget_register BDR
                LEFT JOIN tb_movement_type  MOVT
                ON BDR.TYPE_ID = MOVT.ID
                LEFT JOIN tb_category CAT
                ON BDR.CATEGORY_ID = CAT.ID
                LEFT JOIN tb_subcategory SUBCAT
                ON BDR.SUBCATEGORY_ID = SUBCAT.ID
                WHERE BDR.ID = {ParamSymbol}Id";

            var parameters = new DynamicParameters();
            parameters.Add(name: "Id", value: id, direction: ParameterDirection.Input);

            var result = DataContext.DataConnection.QuerySingleOrDefault<BudgetRegister>(
                sql: query,
                param: parameters,
                transaction: DataContext.DbTransaction);

            return result;
        }

        public IEnumerable<BudgetRegister> GetByMonthYear(string year, string month, int movementId)
        {
            var query = $@"
                SELECT 
                    BDR.ID AS Id
                    ,BDR.TYPE_ID AS TypeId
			        ,MOVT.NAME AS TypeName
                    ,BDR.CATEGORY_ID AS CategoryId
                    ,CAT.NAME AS CategoryName
                    ,BDR.SUBCATEGORY_ID AS SubCategoryId
                    ,SUBCAT.NAME AS SubCategoryName
                    ,BDR.VALUE AS Value
                    ,BDR.DESCRIPTION AS Description
                    ,BDR.DT_REGISTER AS DtRegister
                    ,BDR.DT_DUE AS DtDue
                    ,BDR.NOTE AS Note
                    ,BDR.PAYMENT_STATUS AS PaymentStatus
                    ,BDR.PAYMENT_ACCOUNT_ID AS PaymentAccountId
                FROM tb_budget_register BDR
                LEFT JOIN tb_movement_type  MOVT
                ON BDR.TYPE_ID = MOVT.ID
                LEFT JOIN tb_category CAT
                ON BDR.CATEGORY_ID = CAT.ID
                LEFT JOIN tb_subcategory SUBCAT
                ON BDR.SUBCATEGORY_ID = SUBCAT.ID
                WHERE MONTH(BDR.DT_REGISTER) = {ParamSymbol}Month
                AND YEAR(BDR.DT_REGISTER) = {ParamSymbol}Year
                AND BDR.TYPE_ID = {ParamSymbol}Movement_Id";

            var parameters = new DynamicParameters();
            parameters.Add(name: "Month", value: month, direction: ParameterDirection.Input);
            parameters.Add(name: "Year", value: year, direction: ParameterDirection.Input);
            parameters.Add(name: "Movement_Id", value: movementId, direction: ParameterDirection.Input);

            var result = DataContext.DataConnection.Query<BudgetRegister>(
                sql: query,
                param: parameters,
                transaction: DataContext.DbTransaction);

            return result;
        }

        public IEnumerable<BudgetRegister> GetByMovementId(int movementId)
        {
            var query = $@"
                SELECT 
                    BDR.ID AS Id
                    ,BDR.TYPE_ID AS TypeId
			        ,MOVT.NAME AS TypeName
                    ,BDR.CATEGORY_ID AS CategoryId
                    ,CAT.NAME AS CategoryName
                    ,BDR.SUBCATEGORY_ID AS SubCategoryId
                    ,SUBCAT.NAME AS SubCategoryName
                    ,BDR.VALUE AS Value
                    ,BDR.DESCRIPTION AS Description
                    ,BDR.DT_REGISTER AS DtRegister
                    ,BDR.DT_DUE AS DtDue
                    ,BDR.NOTE AS Note
                    ,BDR.PAYMENT_STATUS AS PaymentStatus
                    ,BDR.PAYMENT_ACCOUNT_ID AS PaymentAccountId
                FROM tb_budget_register BDR
                LEFT JOIN tb_movement_type  MOVT
                ON BDR.TYPE_ID = MOVT.ID
                LEFT JOIN tb_category CAT
                ON BDR.CATEGORY_ID = CAT.ID
                LEFT JOIN tb_subcategory SUBCAT
                ON BDR.SUBCATEGORY_ID = SUBCAT.ID
                WHERE BDR.TYPE_ID = {ParamSymbol}Movement_Id";

            var parameters = new DynamicParameters();
            parameters.Add(name: "Movement_Id", value: movementId, direction: ParameterDirection.Input);

            var result = DataContext.DataConnection.Query<BudgetRegister>(
                sql: query,
                param: parameters,
                transaction: DataContext.DbTransaction);

            return result;
        }

        public int Insert(BudgetRegister budgetRegister)
        {
            var query = $@"
                INSERT INTO tb_budget_register
                (
                    CATEGORY_ID,
	                DT_REGISTER,
	                DESCRIPTION,
	                DT_DUE,
	                NOTE,
	                PAYMENT_ACCOUNT_ID,
	                PAYMENT_STATUS,
	                SUBCATEGORY_ID,
	                TYPE_ID,
	                VALUE
                )
                VALUES
                (
	                {ParamSymbol}Category_Id,
                    {ParamSymbol}Dt_Register, 
                    {ParamSymbol}Description,  
                    {ParamSymbol}Dt_Due,
                    {ParamSymbol}Note,
                    {ParamSymbol}Payment_Account_Id,
                    {ParamSymbol}Payment_Status,
                    {ParamSymbol}SubCategory_Id,
                    {ParamSymbol}Type_Id,
                    {ParamSymbol}Value
                )";
            
            var parameters = new DynamicParameters();
            parameters.Add(name: "Category_Id", value: budgetRegister.CategoryId, direction: ParameterDirection.Input);
            parameters.Add(name: "Dt_Register", value: budgetRegister.DtRegister, direction: ParameterDirection.Input);
            parameters.Add(name: "Description", value: budgetRegister.Description, direction: ParameterDirection.Input);
            parameters.Add(name: "Dt_Due", value: budgetRegister.DtDue, direction: ParameterDirection.Input);
            parameters.Add(name: "Note", value: budgetRegister.Note, direction: ParameterDirection.Input);
            parameters.Add(name: "Payment_Account_Id", value: budgetRegister.PaymentAccountId, direction: ParameterDirection.Input);
            parameters.Add(name: "Payment_Status", value: budgetRegister.PaymentStatus, direction: ParameterDirection.Input);
            parameters.Add(name: "SubCategory_Id", value: budgetRegister.SubCategoryId, direction: ParameterDirection.Input);
            parameters.Add(name: "Type_Id", value: budgetRegister.TypeId, direction: ParameterDirection.Input);
            parameters.Add(name: "Value", value: budgetRegister.Value, direction: ParameterDirection.Input);

            var result = DataContext.DataConnection.Execute(
                sql: query,
                param: parameters,
                transaction: DataContext.DbTransaction);

            return result;
        }

        public int Update(BudgetRegister budgetRegister)
        {
            throw new System.NotImplementedException();
        }
    }
}