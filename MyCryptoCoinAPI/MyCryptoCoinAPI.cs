
// MyCryptoCoinApi //

/*
MyCryptoCoin C#  

  - MyCryptoCoinAPI/
    - Controllers/
      - BlockchainController.cs
      - TransactionController.cs
    - Middleware/
      - ExceptionHandlerMiddleware.cs
    - Startup.cs
    - appsettings.json
    - MyCryptoCoinAPI.csproj
*/

// BlockchainController.cs 
using MyCryptoCoinCore.Services;
using MyCryptoCoinCore.Models;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;

namespace MyCryptoCoinApi.Controllers
{
    public class BlockchainController : ApiController
    {
        private readonly IBlockchainService _blockchainService;

        public BlockchainController(IBlockchainService blockchainService)
        {
            _blockchainService = blockchainService;
        }

        [HttpGet]
        public IHttpActionResult GetBlockchain()
        {
            var blockchain = _blockchainService.GetBlockchain();
            return Ok(blockchain);
        }

        [HttpGet]
        public IHttpActionResult GetBlock(string blockId)
        {
            var block = _blockchainService.GetBlock(blockId);
            if (block == null)
                return NotFound();

            return Ok(block);
        }

        [HttpPost]
        public IHttpActionResult AddBlock(BlockDto blockDto)
        {
            var newBlock = _blockchainService.AddBlock(blockDto.Data);
            return Content(HttpStatusCode.Created, newBlock);
        }

        [HttpPost]
        public IHttpActionResult MineBlock()
        {
            var minedBlock = _blockchainService.MineBlock();
            return Content(HttpStatusCode.Created, minedBlock);
        }

        [HttpPost]
        public IHttpActionResult ValidateBlockchain()
        {
            var isValid = _blockchainService.ValidateBlockchain();
            return Ok(isValid);
        }

        [HttpPost]
        public IHttpActionResult AddTransaction(TransactionDto transactionDto)
        {
            var addedTransaction = _blockchainService.AddTransaction(transactionDto);
            return Content(HttpStatusCode.Created, addedTransaction);
        }

        [HttpGet]
        public IHttpActionResult GetTransaction(string transactionId)
        {
            var transaction = _blockchainService.GetTransaction(transactionId);
            if (transaction == null)
                return NotFound();

            return Ok(transaction);
        }

        // Other blockchain-related endpoints

        [HttpGet]
        public IHttpActionResult GetBalance(string walletAddress)
        {
            var balance = _blockchainService.GetBalance(walletAddress);
            return Ok(balance);
        }

        [HttpGet]
        public IHttpActionResult GetWallet(string walletAddress)
        {
            var wallet = _blockchainService.GetWallet(walletAddress);
            if (wallet == null)
                return NotFound();

            return Ok(wallet);
        }

        [HttpPost]
        public IHttpActionResult CreateWallet()
        {
            var newWallet = _blockchainService.CreateWallet();
            return Content(HttpStatusCode.Created, newWallet);
        }
    }
}

// TransactionController.cs 
using MyCryptoCoinCore.Services;
using MyCryptoCoinCore.Models;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MyCryptoCoinApi.Controllers
{
    public class TransactionController : ApiController
    {
        private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpGet]
        public IHttpActionResult GetTransaction(string transactionId)
        {
            var transaction = _transactionService.GetTransaction(transactionId);
            if (transaction == null)
                return NotFound();

            return Ok(transaction);
        }

        [HttpPost]
        public IHttpActionResult CreateTransaction(TransactionDto transactionDto)
        {
            var newTransaction = _transactionService.CreateTransaction(transactionDto);
            return Content(HttpStatusCode.Created, newTransaction);
        }

        [HttpGet]
        public IHttpActionResult GetTransactionHistory(string walletAddress)
        {
            var transactionHistory = _transactionService.GetTransactionHistory(walletAddress);
            return Ok(transactionHistory);
        }

        [HttpGet]
        public IHttpActionResult GetPendingTransactions()
        {
            var pendingTransactions = _transactionService.GetPendingTransactions();
            return Ok(pendingTransactions);
        }

        [HttpPost]
        public IHttpActionResult ProcessPendingTransactions()
        {
            var processedTransactions = _transactionService.ProcessPendingTransactions();
            return Ok(processedTransactions);
        }
    }
}

// ExceptionHandlerMiddleware.cs 
using System;
using System.Net;
using System.Threading.Tasks;
using MyCryptoCoinApi.Models;
using Newtonsoft.Json;
using System.Web.Http;

namespace MyCryptoCoinApi.Middleware
{
    public class ExceptionHandlerMiddleware
    {
        private readonly Func<Task> _next;

        public ExceptionHandlerMiddleware(Func<Task> next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpActionContext actionContext)
        {
            try
            {
                await _next.Invoke();
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(actionContext, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpActionContext actionContext, Exception ex)
        {
            // Log the exception or perform any other necessary actions

            var errorResponse = new ErrorResponse
            {
                Message = "An error occurred while processing the request.",
                Error = ex.Message
            };

            var response = actionContext.Request.CreateResponse(HttpStatusCode.InternalServerError, errorResponse);
            response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            await actionContext.Response.WriteAsync(JsonConvert.SerializeObject(errorResponse));
        }
    }
}

// Startup.cs 
using System;
using MyCryptoCoinApi.Middleware;
using MyCryptoCoinCore.Services;
using MyCryptoCoinCore.Repositories;
using MyCryptoCoinApi.Controllers; // Import the namespace containing your controllers

namespace MyCryptoCoin.Api
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            // Configure services
            services.AddSingleton<IBlockchainService, BlockchainService>();
            services.AddSingleton<ITransactionService, TransactionService>();
            services.AddSingleton<IBlockRepository, BlockRepository>();
            services.AddSingleton<ITransactionRepository, TransactionRepository>();

            // Add other required services

            services.AddControllers().AddControllersAsServices(); // Manually register controllers
            services.AddTransient<BlockchainController>(); // Register additional controllers if needed
            services.AddTransient<TransactionController>();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.Use(async (context, next) =>
            {
                try
                {
                    await next.Invoke();
                }
                catch (Exception ex)
                {
                    // Handle exceptions globally
                    await ExceptionHandlerMiddleware.HandleExceptionAsync(context, ex);
                }
            });

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

// appsettings.json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning",
      "Microsoft.Hosting.Lifetime": "Information"
    }
  },
  "MyCryptoCoinAPIConfig": {
    "Setting1": "Value1",
    "Setting2": "Value2"
  }
}

// MyCryptoCoinAPI.csproj
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace MyCryptoCoinAPI
{
    public class MyCryptoCoinAPI
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.AddConsole();
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}


  
