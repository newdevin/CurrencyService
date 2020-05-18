namespace CurrencyService
open FSharp.Data.Sql
open FSharp.Data.Sql.Transactions
open System
open System.Data

[<RequireQualifiedAccess>]
module Database =
    [<Literal>]
    let connectionString = "Server = . ; Database = Currency ; Trusted_Connection = true"
    type CurrencyDb = SqlDataProvider<DatabaseVendor = Common.DatabaseProviderTypes.MSSQLSERVER, ConnectionString = connectionString , UseOptionTypes = true>

    let options = {IsolationLevel = IsolationLevel.DontCreateTransaction ; Timeout = TimeSpan.FromSeconds(15.0)}

    let ctx = CurrencyDb.GetDataContext(options)

    let getCurrencies = 
        query {
            for currency in ctx.Dbo .Currency do
            select currency.Symbol
        }
        |> Seq.toList

    let getCurrenciesForDownload = 
        query {
            for currency in ctx.Dbo.Currency do
            where ( currency.Symbol <> "GBP")
            select currency.Symbol
        }



