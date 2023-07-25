using DataAccess.DTO;
using Microsoft.AspNetCore.Mvc.Formatters;
using System.Text;

namespace WebAPI.Formatter
{
    public class CsvInputFormatter : TextInputFormatter
    {
        public CsvInputFormatter()
        {
            SupportedMediaTypes.Add("text/csv");
            SupportedEncodings.Add(Encoding.UTF8);
            SupportedEncodings.Add(Encoding.Unicode);
        }

        protected override bool CanReadType(Type type)
        {
            return type == typeof(AccountDTO);
        }

        public async override Task<InputFormatterResult> ReadRequestBodyAsync
            (InputFormatterContext context, Encoding encoding)
        {
            var httpContext = context.HttpContext;
            using var reader = new StreamReader(httpContext.Request.Body, encoding);
            string? dataLine = null;
            try
            {
                await ReadLineAsync($"Name,Code,Email", reader, context);
                List<AccountDTO> list = new List<AccountDTO>();
                while((dataLine = await ReadLineAsync(null, reader, context))!=null)
                {
                    var data = dataLine.Split(',');
                    var product = new AccountDTO()
                    {
                        Name = data[0],
                        Code = data[1],
                        Email = data[2]
                    };
                    list.Add(product);
                }
                return await InputFormatterResult.SuccessAsync(list);

            }
            catch (Exception)
            {
                return await InputFormatterResult.FailureAsync();
            }
        }

        private static async Task<string> ReadLineAsync(
     string expectedText, StreamReader reader, InputFormatterContext context)
        {
            var line = await reader.ReadLineAsync();

            if (expectedText != null)
            {
                if (line is null || !line.StartsWith(expectedText))
                {
                    var errorMessage = $"Looked for '{expectedText}' and got '{line}'";

                    context.ModelState.TryAddModelError(context.ModelName, errorMessage);


                    throw new Exception(errorMessage);
                }
            }
     

            return line;
        }
    }
}
