namespace SoftUni
{
    using SoftUni.Data;
    using SoftUni.Models;
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Text;

    public class StartUp
    {
        public static void Main(string[] args)
        {
            ////3,4,5,6,7
            using (var context = new SoftUniContext())
            {
                //3
                //var result = GetEmployeesFullInformation(context);

                //4
                //var result = GetEmployeesWithSalaryOver50000(context);

                //5
                //var result = GetEmployeesFromResearchAndDevelopment(context);

                //6
                //var result = AddNewAddressToEmployee(context);

                //7
                //var result = GetEmployeesInPeriod(context);

                //8
                //var result = GetAddressesByTown(context);

                //9
                //var result = GetEmployee147(context);

                //10
                //var result = GetDepartmentsWithMoreThan5Employees(context);

                //11
                //var result = GetLatestProjects(context);

                //12
                //var result = IncreaseSalaries(context);

                //13
                //var result = GetEmployeesByFirstNameStartingWithSa(context);

                //14
                //var result = DeleteProjectById(context);

                //15
                Console.WriteLine(result);
            }
        }
        //3
        //public static string GetEmployeesFullInformation(SoftUniContext context)
        //{
        //    var employees = context.Employees.OrderBy(x => x.EmployeeId).ToList();

        //    StringBuilder sb = new StringBuilder();

        //    foreach (var e in employees)
        //    {
        //        sb.AppendLine($"{e.FirstName} {e.LastName} {e.MiddleName} {e.JobTitle} {e.Salary:f2}");
        //    }

        //    return sb.ToString().TrimEnd();
        //}

        //4
        //public static string GetEmployeesWithSalaryOver50000(SoftUniContext context)
        //{
        //    var employees = context.Employees.
        //        Select(e => new{
        //            e.FirstName,
        //            e.Salary
        //    }).Where(e => e.Salary > 50000).OrderBy(e => e.FirstName).ToList();

        //    StringBuilder sb = new StringBuilder();

        //    foreach (var e in employees)
        //    {
        //        sb.AppendLine($"{e.FirstName} - {e.Salary:f2}");
        //    }

        //    return sb.ToString().TrimEnd();
        //}

        //5
        //public static string GetEmployeesFromResearchAndDevelopment(SoftUniContext context)
        //{
        //    var employees = context.Employees
        //        .OrderBy(x => x.Salary)
        //        .ThenByDescending(x => x.FirstName)
        //        .Where(x => x.Department.Name == "Research and Development")
        //        .ToList();

        //    StringBuilder sb = new StringBuilder();

        //    foreach (var e in employees)
        //    {
        //        sb.AppendLine($"{e.FirstName} {e.LastName} from Research and Development - ${e.Salary:f2}");
        //    }

        //    return sb.ToString().TrimEnd();
        //}

        //6
        //public static string AddNewAddressToEmployee(SoftUniContext context)
        //{
        //    var address = new Address
        //    {
        //        AddressText = "Vitoshka 15",
        //        TownId = 4
        //    };

        //    context.Addresses.Add(address);

        //    var nakov = context.Employees.FirstOrDefault(x => x.LastName == "Nakov");
        //    nakov.Address = address;

        //    context.SaveChanges();

        //    var employeeAddresses = context.Employees
        //        .OrderByDescending(x => x.AddressId)
        //        .Select(a => a.Address.AddressText)
        //        .Take(10)
        //        .ToList();

        //    StringBuilder sb = new StringBuilder();

        //    foreach (var e in employeeAddresses)
        //    {
        //        sb.AppendLine($"{e}");
        //    }

        //    return sb.ToString().TrimEnd();
        //}

        //7
        //public static string GetEmployeesInPeriod(SoftUniContext context)
        //{
        //    var employees = context.Employees
        //        .Where(x => x.EmployeesProjects
        //        .Any(s => s.Project.StartDate.Year >= 2001 && s.Project.StartDate.Year <= 2003))
        //        .Select(x => new
        //        {
        //            EmployeeFullName = x.FirstName + " " + x.LastName,
        //            ManagerFullName = x.Manager.FirstName + " " + x.Manager.LastName,
        //            Projects = x.EmployeesProjects.Select(p => new
        //            {
        //                ProjectName = p.Project.Name,
        //                StartDate = p.Project.StartDate,
        //                EndDate = p.Project.EndDate
        //            })
        //        })
        //        .Take(10)
        //        .ToList();

        //    StringBuilder sb = new StringBuilder();

        //    foreach (var e in employees)
        //    {
        //        sb.AppendLine($"{e.EmployeeFullName} - Manager: {e.ManagerFullName}");

        //        foreach (var p in e.Projects)
        //        {
        //            var startDate = p.StartDate.ToString("M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture);

        //            var endDate = p.EndDate.HasValue ? p.EndDate.Value.ToString("M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture) : "not finished";

        //            sb.AppendLine($"--{p.ProjectName} - {startDate} - {endDate}");
        //        }
        //    }

        //    return sb.ToString().TrimEnd();
        //}

        //8
        //public static string GetAddressesByTown(SoftUniContext context)
        //{
        //    //Find all addresses, ordered by the number of employees who live there(descending), then by town name(ascending), and finally by address text(ascending).Take only the first 10 addresses.For each address return it in the format "<AddressText>, <TownName> - <EmployeeCount> employees"

        //    var addresses = context.Addresses
        //        .OrderByDescending(x => x.Employees.Count)
        //        .ThenBy(x => x.Town.Name)
        //        .ThenBy(x => x.AddressText)
        //        .Select(a => new
        //        {
        //            AddressText = a.AddressText,
        //            TownName = a.Town.Name,
        //            EmployeesCount = a.Employees.Count
        //        })
        //    .Take(10)
        //    .ToList();

        //    StringBuilder sb = new StringBuilder();

        //    foreach (var a in addresses)
        //    {
        //        sb.AppendLine($"{a.AddressText}, {a.TownName} - {a.EmployeesCount} employees");
        //    }

        //    return sb.ToString().TrimEnd();
        //}


        //9
        //public static string GetEmployee147(SoftUniContext context)
        //{
        //    var employee147 = context.Employees
        //        .Where(x => x.EmployeeId == 147)
        //        .Select(x => new
        //        {
        //            FullName = x.FirstName + " " + x.LastName,
        //            JobTitle = x.JobTitle,
        //            Projects = x.EmployeesProjects.OrderBy(p => p.Project.Name).Select(p => p.Project.Name)
        //        }).ToList();

        //    StringBuilder sb = new StringBuilder();

        //    foreach (var e in employee147)
        //    {
        //        sb.AppendLine($"{e.FullName} - {e.JobTitle}");

        //        foreach (var p in e.Projects)
        //        {
        //            sb.AppendLine($"{p}");
        //        }
        //    }

        //    return sb.ToString().TrimEnd();
        //}

        //10
        //public static string GetDepartmentsWithMoreThan5Employees(SoftUniContext context)
        //{
        //    var departments = context.Departments
        //        .Where(x => x.Employees.Count > 5)
        //        .OrderBy(x => x.Employees.Count)
        //        .ThenBy(x => x.Name)
        //        .Select(x => new
        //        {
        //            ManagerFullName = x.Manager.FirstName + " " + x.Manager.LastName,
        //            DepartmentName = x.Name,
        //            emp = x.Employees.Select(e => new
        //            {
        //                firstname = e.FirstName,
        //                lastname = e.LastName,
        //                jobtitle = e.JobTitle
        //            }).OrderBy(e => e.firstname).ThenBy(e => e.lastname)
        //        })
        //        .ToList();

        //    StringBuilder sb = new StringBuilder();

        //    foreach (var d in departments)
        //    {
        //        sb.AppendLine($"{d.DepartmentName} - {d.ManagerFullName}");

        //        foreach (var e in d.emp)
        //        {
        //            sb.AppendLine($"{e.firstname} {e.lastname} - {e.jobtitle}");
        //        }
        //    }

        //    return sb.ToString().TrimEnd();
        //}

        //11
        //public static string GetLatestProjects(SoftUniContext context)
        //{ 
        //    var latestProjects = context.Projects
        //        .OrderByDescending(d => d.StartDate)
        //        .Take(10)
        //        .OrderBy(p => p.Name)
        //        .ToList();

        //    StringBuilder sb = new StringBuilder();

        //    foreach (var lp in latestProjects)
        //    {
        //        var startDate = lp.StartDate.ToString("M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture);

        //        sb.AppendLine($"{lp.Name}");
        //        sb.AppendLine($"{lp.Description}");
        //        sb.AppendLine($"{startDate}");
        //    }

        //    return sb.ToString().TrimEnd();
        //}

        //12
        //public static string IncreaseSalaries(SoftUniContext context)
        //{
        //    var salariesForUpdate = context.Employees
        //        .Where(x => x.Department.Name == "Engineering" || x.Department.Name == "Tool Design" || x.Department.Name == "Marketing" || x.Department.Name == "Information Services")
        //        .OrderBy(x => x.FirstName)
        //        .ThenBy(x => x.LastName)
        //        .ToList();

        //    StringBuilder sb = new StringBuilder();

        //    foreach (var s in salariesForUpdate)
        //    {
        //        s.Salary = s.Salary * 1.12m;
        //        sb.AppendLine($"{s.FirstName} {s.LastName} (${s.Salary:f2})");
        //    }

        //    context.SaveChanges();

        //    return sb.ToString().TrimEnd();
        //}

        //13
        //public static string GetEmployeesByFirstNameStartingWithSa(SoftUniContext context)
        //{
        //    //Write a program that finds all employees whose first name starts with "Sa".Return their first, last name, their job title and salary, rounded to 2 symbols after the decimal separator in the format given in the example below.Order them by first name, then by last name(ascending).

        //    var employees = context.Employees
        //        .Where(x => x.FirstName.StartsWith("Sa"))
        //        .OrderBy(x => x.FirstName)
        //        .ThenBy(x => x.LastName)
        //        .Select(x =>
        //        new {
        //            FullName = x.FirstName + " " + x.LastName,
        //            JobTitle = x.JobTitle,
        //            Salary = x.Salary
        //        })
        //        .ToList();

        //    StringBuilder sb = new StringBuilder();

        //    foreach (var e in employees)
        //    {
        //        sb.AppendLine($"{e.FullName} - {e.JobTitle} - (${e.Salary:f2})");
        //    }

        //    return sb.ToString().TrimEnd();
        //}

        //14
        //public static string DeleteProjectById(SoftUniContext context)
        //{
        //    //Let's delete the project with id 2. Then, take 10 projects and return their names, each on a new line. Remember to restore your database after this task.

        //    StringBuilder sb = new StringBuilder();

        //    var project = context.Projects.FirstOrDefault(x => x.ProjectId == 2);

        //    var employeeProjects = context.EmployeesProjects.Where(x => x.ProjectId == 2).ToList();

        //    context.EmployeesProjects.RemoveRange(employeeProjects);

        //    context.Projects.Remove(project);

        //    context.SaveChanges();

        //    var display = context.Projects.Select(p => p.Name).Take(10).ToList();

        //    foreach (var ep in display)
        //    {
        //        sb.AppendLine($"{ep}");
        //    }

        //    return sb.ToString().TrimEnd();
        //}

        //15
        public static string RemoveTown(SoftUniContext context)
        {
            //Write a program that deletes a town with name „Seattle”. Also, delete all addresses that are in those towns. Return the number of addresses that were deleted in format “{ count}
            //addresses in Seattle were deleted”. There will be employees living at those addresses, which will be a problem when trying to delete the addresses. So, start by setting the AddressId of each employee for the given address to null.After all of them are set to null, you may safely remove all the addresses from the context.Addresses and finally remove the given town.



        }
    }
}
