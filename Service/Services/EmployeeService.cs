using Domain;
using Domain.Models;
using Microsoft.EntityFrameworkCore;


namespace Service.Services
{
    public interface IEmployeeService
    {
        Task<IEnumerable<Employee>> GetEmployees();
        Task<Employee> GetEmployee(int Id);
        Task<Employee> AddEmployee(Employee employee);
        Task<Employee> UpdateEmployee(Employee employee);
        void DeleteEmployee(int employeeId);
    }
    public class EmployeeService:IEmployeeService
    {
        private readonly AppDbContext _context;
        public EmployeeService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Employee> AddEmployee(Employee employee)
        {
            var result = await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async void DeleteEmployee(int employeeId)
        {
            var result = await _context.Employees
                .FirstOrDefaultAsync(e => e.EmployeeId == employeeId);
            if(result != null)
            {
                _context.Employees.Remove(result);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Employee> GetEmployee(int Id)
        {
            return await _context.Employees
                .Include(e => e.Department)
                .Where(e => e.EmployeeId == Id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            return await _context.Employees.ToListAsync();
        }

        public async Task<Employee> UpdateEmployee(Employee employee)
        {
            var result = await _context.Employees.FirstOrDefaultAsync(e => e.EmployeeId == employee.EmployeeId);
            if( result != null)
            {
                result.FirstName = employee.FirstName;
                result.LastName = employee.LastName;
                result.DateOfBirth = employee.DateOfBirth;
                result.Email = employee.Email;
                result.PhotoPath = employee.PhotoPath;
                result.Department = employee.Department;
                result.Gender = employee.Gender;

                await _context.SaveChangesAsync();
                
                return result;
            }
            return null;
        }
    }
}
