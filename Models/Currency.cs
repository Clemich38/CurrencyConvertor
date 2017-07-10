using System.Collections.Generic;
using System;

namespace CurrencyConvertor.ViewModels
{
  public class CurrencyModel
  {
    public Dictionary<string, Currency> currencyDic { get; set; }
    public decimal exchangeRate { get; set; }
    public string countryCode1 { get; set; }
    public string countryCode2 { get; set; }
    public decimal value1 { get; set; }
    public decimal value2 { get; set; }
    
  }
  public class Currency
  {
    public string symbol { get; set; }
    public string name { get; set; }
    public string symbol_native { get; set; }
    public int decimal_digits { get; set; }
    public int introunding { get; set; }
    public string code { get; set; }
    public string name_plural { get; set; }
  }
}