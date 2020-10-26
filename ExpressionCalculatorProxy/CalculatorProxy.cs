using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Extensions;

namespace ExpressionCalculatorProxy
{
    public class CalculatorProxy
    {
        private HttpClient _httpClient;
        private string _url;

        public CalculatorProxy(string url)
        {
            _url = url;
            _httpClient = new HttpClient();
        }
        
        public async Task<CalculationProxyResult> GetCalculationResultAsync(double firstNumber, char op, double secondNumber)
        {
            var queryBuilder = new QueryBuilder();
            
            queryBuilder.Add("firstNumber", firstNumber.ToString());
            queryBuilder.Add("op", op.ToString());
            queryBuilder.Add("secondNumber", secondNumber.ToString());
            
            var uri = new UriBuilder(_url)
            {
                Query = queryBuilder.ToString()
            }.ToString();
            
            var response = await _httpClient.GetAsync(uri);

            return new CalculationProxyResult(response);
        }
    }
}