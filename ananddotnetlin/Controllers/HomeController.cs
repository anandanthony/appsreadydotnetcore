using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ananddotnetlin.Models;
using System.Threading;
using Microsoft.Extensions.Configuration;

namespace ananddotnetlin.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration Config;

        public HomeController(ILogger<HomeController> logger, IConfiguration config)
        {
            _logger = logger;
            Config = config;
        }

        public IActionResult Index()
        {
            _logger.LogInformation(Config["myappsetting"]);
            printaspnetiispp();
            return View();
        }

        public void printaspnetiispp()
        {
            try
            {
                var sitePhysicalPath = Environment.GetEnvironmentVariable("ASPNETCORE_IIS_PHYSICAL_PATH");
                var siteCurrentPath = Environment.CurrentDirectory;
                ViewBag.rInformation = sitePhysicalPath.ToString();
                ViewBag.cDirectory = siteCurrentPath.ToString();
                _logger.LogInformation(sitePhysicalPath.ToString());
            }
            catch (Exception ex)
            {
                ViewBag.rInformation = ex.StackTrace;
                _logger.LogInformation("Exception: " + ex.StackTrace);
            }
        }

        public IActionResult handledException()
        {
            try
            {
                int i = 0;
                i = 5 / i;
                System.Diagnostics.Trace.WriteLine("Computed Value: " + i);
            }
            catch(DivideByZeroException ex)
            {
                ViewBag.rException = ex.Message;
                System.Diagnostics.Trace.WriteLine("Exception: " + ex.Message);
                System.Diagnostics.Trace.WriteLine("Stack Trace: " + ex.StackTrace);

                _logger.LogInformation("Exception from Logger: " + ex.Message);
                _logger.LogInformation("Stack Trace from Logger: " + ex.StackTrace);
            }
            return View("Index");
        }

        public IActionResult unhandledException()
        {
            _logger.LogInformation("inside unhandled exception");

            int i = 0;
            i = 5 / i;

            _logger.LogInformation("Executed divide by zero");

            System.Diagnostics.Trace.WriteLine("Computed Value: " + i);

            ViewBag.rException = "Can you reach me!";

            return View("Index");
        }
        public IActionResult slowResponse()
        {
            try
            {
                _logger.LogInformation("Request Start: " + DateTime.Now);
                Thread.Sleep(250000);
                _logger.LogInformation("Request End: " + DateTime.Now);

                ViewBag.rException = "Request Executed";
            }
            catch(Exception ex)
            {
                _logger.LogInformation("Error: " + ex.Message);
                _logger.LogInformation("Error StackTrace: " + ex.Message);
            }

            return View("Index");
        }

        public IActionResult crashMethod()
        {
            Recursive(0);
            return View("Index");
        }

        static void Recursive(int value)
        {
            // Write call number and call this method again.
            // ... The stack will eventually overflow.
            //Console.WriteLine(value);
            Recursive(++value);
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
