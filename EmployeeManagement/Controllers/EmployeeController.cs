using EmployeeManagement.Data;
using EmployeeManagement.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        
        public EmployeeController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(Employees empModel)
        {
            var employeemodel = new Employees
            {
                Name= empModel.Name,
                Email= empModel.Email,
                Department= empModel.Department,
                DateOfJoining = DateTime.Now

            };
            await dbContext.AddAsync(employeemodel);
            await dbContext.SaveChangesAsync();

            return View();
        }
        [HttpGet]
        public async Task<IActionResult> List()
        {
            var employee= await dbContext.Employees.ToListAsync();
            return View(employee);
           
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var employee = await dbContext.Employees.FindAsync(id);
            return View(employee);

        }
        [HttpPost]
        public async Task<IActionResult> Edit(Employees empModel)
        {
            var employee = await dbContext.Employees.FindAsync(empModel.EmployeeId);
            if (employee is not null)
            {
                employee.Name = empModel.Name;
                employee.Email = empModel.Email;
                employee.Department = empModel.Department;
                employee.DateOfJoining = DateTime.Now;

                await dbContext.SaveChangesAsync();
            }
            return RedirectToAction("List","Employee");

        }
        [HttpPost]
        public async Task<IActionResult> Delete(Employees empModel)
        {
            var employee = await dbContext.Employees.AsNoTracking().FirstOrDefaultAsync( x=>x.EmployeeId == empModel.EmployeeId);
            if (employee is not null)
            {

                dbContext.Remove(empModel);
                await dbContext.SaveChangesAsync();
            }
            return RedirectToAction("List", "Employee");

        }


    }
}
