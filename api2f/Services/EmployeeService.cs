using api1.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api1.Services
{
    public class EmployeeServices : IEmployeeServices
    {
        private readonly AppDbContext _context;
        public EmployeeServices(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Employee> AddEmployee(Employee employee)
        {
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
            return employee;
        }

        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            return await _context.Employees.ToListAsync();
        }

        public async Task<Employee> GetEmployeeById(int id)
        {
            var emp = await _context.Employees.FindAsync(id);
            if (emp == null)
                return null;
            else
                return emp;
        }

        public async Task<bool> DeleteEmployee(int id)
        {
            var emp = await _context.Employees.FindAsync(id);
            if (emp != null)
            {
                _context.Employees.Remove(emp);
                await _context.SaveChangesAsync();
                return true;
            }
            else
                return false;
        }

        public async Task<Employee> UpdateEmployee(int id, Employee employee)
        {
            var emp = await _context.Employees.FindAsync(id);
            if (emp != null)
            {
                emp.Name = employee.Name;
                emp.PhoneNumber = employee.PhoneNumber;
                emp.Email = employee.Email;
                _context.Employees.Update(emp);
                await _context.SaveChangesAsync();
                return emp;
            }
            else
                return null;
        }

    }
}