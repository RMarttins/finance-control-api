using ApiFinance.Data.Contracts;
using ApiFinance.Domain.Entities.DataBase;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;

namespace ApiFinance.Data.Repositories
{
    public class AccountRepository : BaseRepositoy<Account>, IAccountRepository
    {
        public AccountRepository(IDataContext dataContext, IConfiguration configuration) : base(dataContext, configuration)
        {
        }

        public int Delete(int id)
        {
            var query = $@"
                DELETE FROM tb_account
                WHERE ID = {ParamSymbol}Id";

            var param = new DynamicParameters();
            param.Add(name: "Id", value: id, direction: ParameterDirection.Input);
            var result = DataContext.DataConnection.Execute(
                sql: query,
                param: param,
                transaction: DataContext.DbTransaction);
            return result;
        }

        public IEnumerable<Account> GetAll()
        {
            var query = $@"
                  SELECT
                    ACC.ID AS Id,
                    ACC.ACCOUNT_LIMIT AS AccountLimit,
                    ACC.ACCOUNT_TYPE_ID AS AccountTypeId,  
                    TPACC.NAME AS AccountTypeName,
                    ACC.BALANCE_AVAILABLE AS BalanceAvailable,
                    ACC.FINANCIAL_INSTITUTION_ID AS FinancialInstitutionId,
                    FI.SHORT_NAME AS FinancialInstitutionName,
                    ACC.OPENING_BALANCE AS OpeningBalance,
                    ACC.NAME AS Name
                FROM tb_account ACC
                INNER JOIN tb_account_type TPACC
                ON ACC.ACCOUNT_TYPE_ID = TPACC.ID
                INNER JOIN tb_financial_institution FI
                ON ACC.FINANCIAL_INSTITUTION_ID = FI.ID";

            var result = DataContext.DataConnection.Query<Account>(
                sql: query,
                transaction: DataContext.DbTransaction);
            return result;
        }

        public Account GetById(int id)
        {
            var query = $@"
                SELECT
                    ACC.ID AS Id,
                    ACC.ACCOUNT_LIMIT AS AccountLimit,
                    ACC.ACCOUNT_TYPE_ID AS AccountTypeId,  
                    TPACC.NAME AS AccountTypeName,
                    ACC.BALANCE_AVAILABLE AS BalanceAvailable,
                    ACC.FINANCIAL_INSTITUTION_ID AS FinancialInstitutionId,
                    FI.SHORT_NAME AS FinancialInstitutionName,
                    ACC.OPENING_BALANCE AS OpeningBalance,
                    ACC.NAME AS Name
                FROM tb_account ACC
                INNER JOIN tb_account_type TPACC
                ON ACC.ACCOUNT_TYPE_ID = TPACC.ID
                INNER JOIN tb_financial_institution FI
                ON ACC.FINANCIAL_INSTITUTION_ID = FI.ID
                WHERE ACC.ID = {ParamSymbol}Id";

            var param = new DynamicParameters();
            param.Add(name: "Id", value: id, direction: ParameterDirection.Input);
            var result = DataContext.DataConnection.QueryFirstOrDefault<Account>(
                sql: query,
                param: param,
                transaction: DataContext.DbTransaction);
            return result;
        }

        public int Insert(Account account)
        {
            var query = $@"
                INSERT INTO tb_account
                (
                    ACCOUNT_LIMIT,
	                ACCOUNT_TYPE_ID,
	                BALANCE_AVAILABLE,
	                FINANCIAL_INSTITUTION_ID,
	                OPENING_BALANCE,
	                NAME
                )
                VALUES
                (
	                {ParamSymbol}Account_Limit,
                    {ParamSymbol}Account_Type_Id, 
                    {ParamSymbol}Balance_Available,  
                    {ParamSymbol}Financial_Institution_Id,
                    {ParamSymbol}Opening_Balance,
                    {ParamSymbol}Name
                )";

            var parameters = new DynamicParameters();
            parameters.Add(name: "Account_Limit", value: account.AccountLimit, direction: ParameterDirection.Input);
            parameters.Add(name: "Account_Type_Id", value: account.AccountTypeId, direction: ParameterDirection.Input);
            parameters.Add(name: "Balance_Available", value: account.BalanceAvailable, direction: ParameterDirection.Input);
            parameters.Add(name: "Financial_Institution_Id", value: account.FinancialInstitutionId, direction: ParameterDirection.Input);
            parameters.Add(name: "Name", value: account.Name, direction: ParameterDirection.Input);
            parameters.Add(name: "Opening_Balance", value: account.OpeningBalance, direction: ParameterDirection.Input);

            var result = DataContext.DataConnection.Execute(
                sql: query,
                param: parameters,
                transaction: DataContext.DbTransaction);

            return result;
        }

        public int Update(Account account)
        {
            var query = $@"
                UPDATE tb_account SET
                    ACCOUNT_LIMIT = {ParamSymbol}Account_Limit,
                    FINANCIAL_INSTITUTION_ID = {ParamSymbol}Financial_Institution_Id,
                    NAME = {ParamSymbol}Name,
                    OPENING_BALANCE = {ParamSymbol}Opening_Balance
                WHERE ID = {ParamSymbol}Id";

            var param = new DynamicParameters();
            param.Add(name: "Account_Limit", value: account.AccountLimit, direction: ParameterDirection.Input);
            param.Add(name: "Financial_Institution_Id", value: account.FinancialInstitutionId, direction: ParameterDirection.Input);
            param.Add(name: "Name", value: account.Name, direction: ParameterDirection.Input);
            param.Add(name: "Opening_Balance", value: account.OpeningBalance, direction: ParameterDirection.Input);
            param.Add(name: "Id", value: account.Id, direction: ParameterDirection.Input);

            var result = DataContext.DataConnection.Execute(
                sql: query,
                param: param,
                transaction: DataContext.DbTransaction);
            return result;
        }
    }
}