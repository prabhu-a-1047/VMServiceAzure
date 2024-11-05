using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace FinOpsFunctionApp
{
    public static class Function1
    {
        [FunctionName("Function1")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string name = req.Query["name"];
            string lname = req.Query["lname"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;

            string responseMessage = string.IsNullOrEmpty(name)
                ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
                : $"Hello, {name}. This HTTP triggered function executed successfully.";

            return new OkObjectResult(responseMessage);
        }
    }
}



//using System;
//using System.IO;
//using System.Net;
//using System.Text;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Azure.WebJobs;
//using Microsoft.Azure.WebJobs.Extensions.Http;
//using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
//using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
//using Microsoft.Extensions.Logging;
//using Microsoft.OpenApi.Models;
//using Newtonsoft.Json;

//namespace TurbineRepair
//{
//    public static class Turbine
//    {
//        const double revenuePerkW = 0.12;
//        const double technicianCost = 250;
//        const double turbineCost = 100;

//        [FunctionName("TurbineRepair")]
//        [OpenApiOperation(operationId: "Run")]
//        [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
//        [OpenApiRequestBody("application/json", typeof(string), Description = "JSON request body containing { hours, capacity}")]
//        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(string), Description = "The OK response message containing a JSON result.")]
//        public static async Task<IActionResult> Run(
//            [HttpTrigger(AuthorizationLevel.Function,"get", "post", Route = null)] HttpRequest req,
//            ILogger log)
//        {
//            try
//            {
//                // Get request body data.
//                //req.EnableBuffering();

//                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
//                dynamic data = JsonConvert.DeserializeObject(requestBody);

//                int? capacity = data?.capacity;
//                int? hours = data?.hours;

//                int? capacity1 = int.Parse(req.Query["capacity"]);
//                int? hours1 = int.Parse(req.Query["hours"]);

//                //?name = "Test" & lname = "Arumugam"

//                // Return bad request if capacity or hours are not passed in
//                if (capacity == null || hours == null)
//                {
//                    return new BadRequestObjectResult("Please pass capacity and hours in the request body");
//                }
//                // Formulas to calculate revenue and cost
//                double? revenueOpportunity = capacity * revenuePerkW * 24;
//                double? costToFix = (hours * technicianCost) + turbineCost;
//                string repairTurbine;

//                if (revenueOpportunity > costToFix)
//                {
//                    repairTurbine = "Yes";
//                }
//                else
//                {
//                    repairTurbine = "No";
//                };

//                return (ActionResult)new OkObjectResult(new
//                {
//                    message = repairTurbine,
//                    revenueOpportunity = "$" + revenueOpportunity,
//                    costToFix = "$" + costToFix
//                });

//            }

//            catch (Exception ex)
//            {
//                return (ActionResult)new OkObjectResult(new
//                {
//                    message = ex.Message.ToString()
//                });
//            }



//        }
//    }
//    public class RequestBodyModel
//    {
//        public int Hours { get; set; }
//        public int Capacity { get; set; }
//    }
//}