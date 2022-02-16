using ApiFinance.Data.Contracts;
using ApiFinance.Domain.Entities.DataBase;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;

namespace ApiFinance.Data.Repositories
{
    public class CreditCardRepository : BaseRepositoy<CreditCard>, ICreditCardRepository
    {
        public CreditCardRepository(IDataContext dataContext, IConfiguration configuration) : base(dataContext, configuration)
        {
        }

        public int Delete(int id)
        {
            var query = $@"
                DELETE FROM tb_credit_card
                WHERE ID = {ParamSymbol}Id";

            var param = new DynamicParameters();
            param.Add(name: "Id", value: id, direction: ParameterDirection.Input);
            var result = DataContext.DataConnection.Execute(
                sql: query,
                param: param,
                transaction: DataContext.DbTransaction);
            return result;
        }

        public IEnumerable<CreditCard> GetAll()
        {
            var query = $@"
                  SELECT
                    CC.ID AS Id,
                    CC.ACCOUNT_PAYMENT_ID AS AccountPaymentId,  
                    ACC.NAME AS AccountPaymentName,
                    CC.AVAILABLE_LIMIT AS AvailableLimit,
                    CC.CARD_LIMIT AS CardLimit,
                    CC.CLOSING_DAY AS ClosingDay,
                    CC.DUE_DAY AS DueDay,
                    CC.FINANCIAL_INSTITUTION_ID AS FinancialInstitutionId,
                    FI.SHORT_NAME AS FinancialInstitutionName,
                    CC.NAME AS Name
                FROM tb_credit_card CC
                LEFT JOIN tb_account ACC
                ON CC.ACCOUNT_PAYMENT_ID = ACC.ID
                INNER JOIN tb_financial_institution FI
                ON CC.FINANCIAL_INSTITUTION_ID = FI.ID";

            var result = DataContext.DataConnection.Query<CreditCard>(
                sql: query,
                transaction: DataContext.DbTransaction);
            return result;
        }

        public CreditCard GetById(int id)
        {
            var query = $@"
                SELECT
                    CC.ID AS Id,
                    CC.ACCOUNT_PAYMENT_ID AS AccountPaymentId,  
                    ACC.NAME AS AccountPaymentName,
                    CC.AVAILABLE_LIMIT AS AvailableLimit,
                    CC.CARD_LIMIT AS CardLimit,
                    CC.CLOSING_DAY AS ClosingDay,
                    CC.DUE_DAY AS DueDay,
                    CC.FINANCIAL_INSTITUTION_ID AS FinancialInstitutionId,
                    FI.SHORT_NAME AS FinancialInstitutionName,
                    CC.NAME AS Name
                FROM tb_credit_card CC
                LEFT JOIN tb_account ACC
                ON CC.ACCOUNT_PAYMENT_ID = ACC.ID
                INNER JOIN tb_financial_institution FI
                ON CC.FINANCIAL_INSTITUTION_ID = FI.ID
                WHERE CC.ID = {ParamSymbol}Id";

            var param = new DynamicParameters();
            param.Add(name: "Id", value: id, direction: ParameterDirection.Input);
            var result = DataContext.DataConnection.QueryFirstOrDefault<CreditCard>(
                sql: query,
                param: param,
                transaction: DataContext.DbTransaction);
            return result;
        }

        public int Insert(CreditCard creditCard)
        {
            var query = $@"
                INSERT INTO tb_credit_card
                (
                    ACCOUNT_PAYMENT_ID,
	                AVAILABLE_LIMIT,
	                CARD_LIMIT,
	                CLOSING_DAY,
	                DUE_DAY,
                    FINANCIAL_INSTITUTION_ID,
	                NAME
                )
                VALUES
                (
	                {ParamSymbol}Account_Payment_Id,
                    {ParamSymbol}Available_Limit, 
                    {ParamSymbol}Card_Limit,  
                    {ParamSymbol}Closing_Day,
                    {ParamSymbol}Due_Day,
                    {ParamSymbol}Financial_Institution_Id,
                    {ParamSymbol}Name
                )";

            var parameters = new DynamicParameters();
            parameters.Add(name: "Account_Payment_Id", value: creditCard.AccountPaymentId, direction: ParameterDirection.Input);
            parameters.Add(name: "Available_Limit", value: creditCard.AvailableLimit, direction: ParameterDirection.Input);
            parameters.Add(name: "Card_Limit", value: creditCard.CardLimit, direction: ParameterDirection.Input);
            parameters.Add(name: "Closing_Day", value: creditCard.ClosingDay, direction: ParameterDirection.Input);
            parameters.Add(name: "Due_Day", value: creditCard.DueDay, direction: ParameterDirection.Input);
            parameters.Add(name: "Financial_Institution_Id", value: creditCard.FinancialInstitutionId, direction: ParameterDirection.Input);
            parameters.Add(name: "Name", value: creditCard.Name, direction: ParameterDirection.Input);

            var result = DataContext.DataConnection.Execute(
                sql: query,
                param: parameters,
                transaction: DataContext.DbTransaction);

            return result;
        }

        public int Update(CreditCard creditCard)
        {
            var query = $@"
                UPDATE tb_credit_card SET
                    ACCOUNT_PAYMENT_ID = {ParamSymbol}Account_Payment_Id,
                    AVAILABLE_LIMIT = {ParamSymbol}Available_Limit,
                    CARD_LIMIT = {ParamSymbol}Card_Limit,
                    CLOSING_DAY = {ParamSymbol}Closing_Day,
                    DUE_DAY = {ParamSymbol}Due_Day,
                    FINANCIAL_INSTITUTION_ID = {ParamSymbol}Financial_Institution_Id,
                    NAME = {ParamSymbol}Name
                WHERE ID = {ParamSymbol}Id";

            var param = new DynamicParameters();
            param.Add(name: "Account_Payment_Id", value: creditCard.AccountPaymentId, direction: ParameterDirection.Input);
            param.Add(name: "Available_Limit", value: creditCard.AvailableLimit, direction: ParameterDirection.Input);
            param.Add(name: "Card_Limit", value: creditCard.CardLimit, direction: ParameterDirection.Input);
            param.Add(name: "Closing_Day", value: creditCard.ClosingDay, direction: ParameterDirection.Input);
            param.Add(name: "Due_Day", value: creditCard.DueDay, direction: ParameterDirection.Input);
            param.Add(name: "Financial_Institution_Id", value: creditCard.FinancialInstitutionId, direction: ParameterDirection.Input);
            param.Add(name: "Name", value: creditCard.Name, direction: ParameterDirection.Input);
            param.Add(name: "Id", value: creditCard.Id, direction: ParameterDirection.Input);

            var result = DataContext.DataConnection.Execute(
                sql: query,
                param: param,
                transaction: DataContext.DbTransaction);
            return result;
        }
    }
}