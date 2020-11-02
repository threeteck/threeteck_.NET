using System;
using System.Net.Http;

namespace ExpressionCalculatorProxy
{
    public class CalculationProxyResult
    {
        private string _responseMessage;
        
        public bool HasResult;
        public int StatusCode;
        public double Result;

        public CalculationProxyResult(HttpResponseMessage response) 
            : this((int)response.StatusCode, response.Content.ReadAsStringAsync().Result,
                response.IsSuccessStatusCode)
        {
        }

        public CalculationProxyResult(int statusCode, string responseMessage, bool isSuccessful = true)
        {
            StatusCode = statusCode;
            _responseMessage = responseMessage;

            if (isSuccessful && Double.TryParse(_responseMessage, out var result))
            {
                Result = result;
                HasResult = true;
            }
        }
        
        private string MapStatusCode(int statusCode) =>
            statusCode switch
            {
                200 => "",
                400 => "Bad Request: ",
                500 => "Server Error",
                _ => "Unknown Error"
            };

        public override string ToString()
        {
            return $"{MapStatusCode(StatusCode)}{_responseMessage}";
        }
    }
}