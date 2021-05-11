using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using mvc.Models;
using mvc.Models.Data;
using mvc.Utils.Extensions;

namespace mvc.Controllers
{
    [Route("/")]
    public class EmployeeController : Controller
    {
        private const int ITEMS_PER_PAGE = 50;

        private readonly ILogger<EmployeeController> mLogger;
        private readonly SynelDbContext mContext;

        public EmployeeController(ILogger<EmployeeController> logger, SynelDbContext context)
        {
            mLogger = logger;
            mContext = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] string search = "", [FromQuery] int page = 1, [FromQuery] int totalLoadedItems = 0)
        {
            EmployeesViewModel viewModel = new EmployeesViewModel();

            if (page < 1)
            {
                mLogger.LogInformation($"<strong>page</strong> parameter cannot be less than 1.");
                viewModel.HasError = true;
                viewModel.Message = $"* page parameter cannot be less than 1.";
                return View(viewModel);
            }

            if (!await mContext.Employees.AnyAsync())
            {
                mLogger.LogInformation("There are no items in the Database");
                viewModel.HasError = true;
                viewModel.Message = "There are no items to show. Try loading them from *.csv file.";
                return View(viewModel);
            }

            var employeeDtos = await mContext.Employees
                .Where(e => string.IsNullOrWhiteSpace(search) || EmployeeMatchesSearch(e, search))
                .Skip((page - 1) * ITEMS_PER_PAGE)
                .Take(ITEMS_PER_PAGE)
                .Select(e => e.ToEmployeeDto())
                .ToListAsync();

            int totalItems = await mContext.Employees.CountAsync();
            int totalPages = (int)Math.Ceiling((double)totalItems / ITEMS_PER_PAGE);

            viewModel.Employees = employeeDtos;
            viewModel.TotalPages = totalPages;
            viewModel.CurrentPage = page;

            if (totalLoadedItems > 0)
            {
                viewModel.Message = $"You have successfully loaded {totalLoadedItems} employees.";
            }

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Import(EmployeesViewModel model)
        {
            var file = model.EmployeesFile;

            if (file == null || file.Length <= 0)
            {
                model.HasError = true;
                model.Message = "The file cannot be empty.";

                return View(model);
            }

            var rows = await file.ReadLinesAsList();

            if (!rows.Any())
            {
                model.HasError = true;
                model.Message = "The file cannot be empty.";

                return View(model);
            }

            var employees = rows.Skip(1)
                .Where(r => IsRowValid(r))
                .Select(r =>
                {
                    var columns = r.Split(",");
                    DateTime.TryParse(columns[3], out DateTime birthdate);
                    DateTime.TryParse(columns[10], out DateTime startDate);

                    return new Employee(
                        columns[0],
                        columns[1],
                        columns[2],
                        birthdate,
                        columns[4],
                        columns[5],
                        columns[6],
                        columns[7],
                        columns[8],
                        columns[9],
                        startDate);
                });

            try
            {
                await mContext.Employees.AddRangeAsync(employees);
                await mContext.SaveChangesAsync();
            }
            catch(Exception e)
            {
                model.HasError = true;
                model.Message = e.Message;

                return View(model);
            }


            return RedirectToAction(nameof(Index), new { TotalLoadedItems = employees.Count() });
        }

        [HttpPost]
        [Route("edit")]
        public IActionResult Edit(EmployeeDto model)
        {
            return BadRequest(model);
        }

        private bool IsRowValid(string row)
        {
            var columns = row.Split(",");

            if (columns.Count() < 11)
            {
                return false;
            }

            return !columns.Any(col => string.IsNullOrWhiteSpace(col))
                && DateTime.TryParse(columns[3], out DateTime birthdate)
                && DateTime.TryParse(columns[10], out DateTime startDate);
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

        private bool EmployeeMatchesSearch(Employee employee, string search)
        {
            return employee.Forename.Contains(search, StringComparison.InvariantCultureIgnoreCase)
                || employee.Surname.Contains(search, StringComparison.InvariantCultureIgnoreCase)
                || employee.HomeEmail.Equals(search);
        }
    }
}
