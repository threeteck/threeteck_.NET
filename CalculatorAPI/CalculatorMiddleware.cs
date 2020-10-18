using System;
using System.Globalization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using threeteck_Calculator;

namespace CalculatorAPI
{
    public class CalculatorMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly Calculator _calculator;

        public CalculatorMiddleware(RequestDelegate next, Calculator calculator = null)
        {
            _next = next;
            if (calculator == null)
                _calculator = Calculator.GetStandartCalculator();
            else _calculator = calculator;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var request = context.Request;
            var response = context.Response;
            var result = double.NaN;
            try
            {
                if (request.Query.ContainsKey("query"))
                    result = _calculator.MakeCalculation(request.Query["query"]);
                else
                {
                    if (!request.Query.ContainsKey("firstNumber"))
                        throw new ArgumentException(null, "firstNumber");
                    var firstNumber = double.Parse(request.Query["firstNumber"]);
                    
                    if (!request.Query.ContainsKey("op"))
                        throw new ArgumentException(null, "op");
                    var op = char.Parse(request.Query["op"]);
                    
                    if (!request.Query.ContainsKey("secondNumber"))
                        throw new ArgumentException(null, "secondNumber");
                    var secondNumber = double.Parse(request.Query["secondNumber"]);

                    result = _calculator.MakeCalculation(firstNumber, op, secondNumber);
                }

                response.StatusCode = 200;
                await response.WriteAsync(result.ToString(CultureInfo.CurrentCulture));
            }
            catch (DivideByZeroException e)
            {
                response.StatusCode = 400;
                await response.WriteAsync("Cannot divide by zero");
            }
            catch (FormatException e)
            {
                response.StatusCode = 400;
                await response.WriteAsync("Invalid format");
            }
            catch (ArgumentException e)
            {
                response.StatusCode = 400;
                if (e.ParamName == "operation")
                    await response.WriteAsync("This operation is not supported");
                else
                    await response.WriteAsync($"Parameter '{e.ParamName}' should be presented in query");
            }
            catch
            {
                response.StatusCode = 500;
            }
        }
    }
}