using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
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

        public async Task<Result> InvokeRequestResponseService(string apiKey, string apiUrl)
        {
            using (var client = new HttpClient())
            {

                Result myResult = new Result();

                // We are setting this the same for all students being they used the same model
                var scoreRequest = new
                {

                    Inputs = new Dictionary<string, StringTable>() {
                        {
                            "input1",
                            new StringTable()
                            {
                                ColumnNames = new string[] {"PassengerId", "Survived", "Pclass", "Name", "Sex", "Age", "SibSp", "Parch", "Ticket", "Fare", "Cabin", "Embarked"},
                                Values = new string[,] {  { "0", "0", "0", "value", "value", "0", "0", "0", "value", "0", "value", "value" },  { "0", "0", "0", "value", "value", "0", "0", "0", "value", "0", "value", "value" },  }
                            }
                        },
                    },
                    GlobalParameters = new Dictionary<string, string>()
                    {
                    }
                };

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

                client.BaseAddress = new Uri(apiUrl);

                HttpResponseMessage response = await client.PostAsJsonAsync("", scoreRequest);

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
