using api1.Data;

namespace api1.Services
{
    public interface IEmployeeServices
    {
        Task<Employee> AddEmployee(Employee employee);
        Task<IEnumerable<Employee>> GetEmployees();
        Task<Employee> GetEmployeeById(int id);
        Task<bool> DeleteEmployee(int id);
        Task<Employee> UpdateEmployee(int id, Employee employee);
    }
}