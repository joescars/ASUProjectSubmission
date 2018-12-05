using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ASUProjectSubmission.Services
{

    public class StringTable
    {
        public string[] ColumnNames { get; set; }
        public string[,] Values { get; set; }
    }
    public class ResponseService
    {
        public class Result
        {
            public bool IsValid { get; set; }
            public string ResponseContent { get; set; }
            public Result()
            {
                this.IsValid = false;
            }
        }

        public async Task<Result> InvokeRequestResponseService(string apiKey, string apiUrl, string requestBody)
        {
            using (var client = new HttpClient())
            {

                Result myResult = new Result();


                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

                client.BaseAddress = new Uri(apiUrl);
                
                var content = new StringContent(requestBody, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync("", content);

                if (response.IsSuccessStatusCode)
                {
                    myResult.IsValid = true;
                    myResult.ResponseContent = await response.Content.ReadAsStringAsync();                
                }
                else
                {
                    myResult.ResponseContent = string.Format("The request failed with status code: {0}", response.StatusCode);
                    //string responseContent = await response.Content.ReadAsStringAsync();
                }

                // Return the result object
                return myResult;
            }
        }
    }
}
