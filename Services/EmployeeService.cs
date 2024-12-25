using Microsoft.EntityFrameworkCore;
using pps.Data;
using pps.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.IO;
using System.Xml.Linq;

namespace pps.Services
{
    public interface IEmployeeService
    {
        Employee GetEmployeeById(uint id);
        List<Employee> GetEmployeeByCriteria(string? fullName = null, DateTime? minBirthday = null, DateTime? maxBirthday = null, string? jobTitle = null,
                                            bool? isRemoteWork = null, bool? isGiveCredit = null, decimal? minSalary = null, decimal? maxSalary = null);
        Employee CreateEmployee(EmployeeCreate employeeCreate);
        EmployeeResponse MapToEmployeeResponse(Employee employee);
        void UpdateEmployee(Employee employee, EmployeeUpdate employeeUpdate);
        void DeleteEmployee(Employee employee);
    }

    public class EmployeeService : IEmployeeService
    {
        private AppDbContext _context;
        //private IBankService _bankService;
        private IBankOfficeService _bankOfficeService;

        public EmployeeService(AppDbContext context, IBankOfficeService bankOfficeService)
        {
            _context = context;
            _bankOfficeService = bankOfficeService;
            //_bankService = bankService;
        }

        public Employee GetEmployeeById(uint id)
        {
            var query = _context.Employees.Include(e => e.BankAtms).Include(e => e.BankOffice).Include(e => e.CreditAccounts).AsQueryable();
            return query.FirstOrDefault(bo => bo.Id == id);
        }

        public List<Employee> GetEmployeeByCriteria(string? fullName = null, DateTime? minBirthday = null, DateTime? maxBirthday = null, string? jobTitle = null,
                                                    bool? isRemoteWork = null, bool? isGiveCredit = null, decimal? minSalary = null, decimal? maxSalary = null)
        {
            var query = _context.Employees.Include(e => e.BankAtms).Include(e => e.BankOffice).Include(e => e.CreditAccounts).AsQueryable();

            if (!string.IsNullOrEmpty(fullName))
                query = query.Where(e => e.FullName.Contains(fullName));
            if (minBirthday.HasValue)
                query = query.Where(e => e.Birthday >= minBirthday);
            if (maxBirthday.HasValue)
                query = query.Where(e => e.Birthday <= maxBirthday);
            if (!string.IsNullOrEmpty(jobTitle))
                query = query.Where(e => e.JobTitle.Contains(jobTitle));
            if (isRemoteWork.HasValue)
                query = query.Where(e => e.IsRemoteWork == isRemoteWork);
            if (isGiveCredit.HasValue)
                query = query.Where(e => e.IsGiveCredit == isGiveCredit);
            if (minSalary.HasValue)
                query = query.Where(e => e.Salary >= minSalary);
            if (maxSalary.HasValue)
                query = query.Where(e => e.Salary <= maxSalary);

            return query.ToList();
        }

        public Employee CreateEmployee(EmployeeCreate employeeCreate)
        {
            Employee employee = new Employee(employeeCreate.FullName, employeeCreate.Birthday, employeeCreate.JobTitle, employeeCreate.IsRemoteWork,
                                             employeeCreate.IsGiveCredit, employeeCreate.Salary, _bankOfficeService.GetBankOfficeById(employeeCreate.bankOfficeId));
            _context.Employees.Add(employee);
            _context.SaveChanges();
            return employee;
        }

        public EmployeeResponse MapToEmployeeResponse(Employee employee)
        {
            return new EmployeeResponse
            {
                Id = employee.Id,
                FullName = employee.FullName,
                Birthday = employee.Birthday,
                JobTitle = employee.JobTitle,
                IsRemoteWork = employee.IsRemoteWork,
                IsGiveCredit = employee.IsGiveCredit,
                Salary = employee.Salary,
                BankOffice = new BankOfficeResponseShort
                {
                    Id = employee.BankOffice.Id,
                    Name = employee.BankOffice.Name
                },
                Atms = employee.BankAtms.Select(ba => new BankAtmResponseShort
                {
                    Id = ba.Id
                }).ToList(),
                CreditAccounts = employee.CreditAccounts.Select(ca => new CreditAccountResponseShort
                {
                    Id = ca.Id
                }).ToList()
            };
        }

        public void UpdateEmployee(Employee employee, EmployeeUpdate employeeUpdate)
        {
            if (!string.IsNullOrEmpty(employeeUpdate.FullName))
                employee.FullName = employeeUpdate.FullName;
            if (employeeUpdate.Birthday.HasValue)
                employee.Birthday = employeeUpdate.Birthday.Value;
            if (!string.IsNullOrEmpty(employeeUpdate.JobTitle))
                employee.JobTitle = employeeUpdate.JobTitle;
            if (employeeUpdate.IsRemoteWork.HasValue)
                employee.IsRemoteWork = employeeUpdate.IsRemoteWork.Value;
            if (employeeUpdate.IsGiveCredit.HasValue)
                employee.IsGiveCredit = employeeUpdate.IsGiveCredit.Value;
            if (employeeUpdate.Salary.HasValue)
                employee.Salary = employeeUpdate.Salary.Value;
            if (employeeUpdate.bankOfficeId.HasValue)
                employee.BankOffice = _bankOfficeService.GetBankOfficeById(employeeUpdate.bankOfficeId.Value);

            _context.SaveChanges();
        }

        public void DeleteEmployee(Employee employee)
        {
            _context.Employees.Remove(employee);
            _context.SaveChanges();
        }
    }
}
