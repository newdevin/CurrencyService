namespace CurrencyService

open System

module Domain =
    type Currency = {Symbol : string}
    type CurrencyRate = {From : Currency ; To : Currency ; Rate : decimal ; Day : DateTime}

    let createCurrency symbol = 
        { Symbol =  symbol }

    let createCurrencyRate from ``to`` rate day =
        {From = from ; To = ``to`` ; Rate = rate ; Day = day}




