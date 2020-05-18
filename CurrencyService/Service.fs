namespace CurrencyService
open Domain
open FSharp.Data
module Service =
    let getAllCurrencies = 
        Database.getCurrencies
        |> List.map ( fun symbol -> createCurrency symbol)
      
    type currencyRatesProvider = CsvProvider<"AlphaVantageCurrencyRateSample.csv">
    
    //let toUrl symbol date = 
    //    match date with
    //    | Some d -> 
    //        sprintf "https:www.alphavantage.co/query?function=FX_DAILY&from_symbol=%s&to_symbol=GBP&apikey=demo&datatype=csv"
    //    | None -> 

    //let downloadCurrencyRates = 
    //    Database.getCurrenciesForDownload()
    //    |> List.map toUrl

