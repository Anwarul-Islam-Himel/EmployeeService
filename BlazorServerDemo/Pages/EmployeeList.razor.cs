using Domain.Models;
using Microsoft.AspNetCore.Components;
using Service.Services;

namespace BlazorServerDemo.Pages;
public partial class EmployeeListBase: ComponentBase
{
    [Inject]
    protected IEmployeeService _employeeService { get; set; }

    public IEnumerable<Employee> Employees { get; set; } = new List<Employee>();
    protected override async Task OnInitializedAsync()
    {
        await LoadEmployees();
    }

    private async Task LoadEmployees()
    {
        Employees = await _employeeService.GetEmployees();
    }

}
