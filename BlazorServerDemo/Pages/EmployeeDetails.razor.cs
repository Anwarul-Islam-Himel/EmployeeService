using Domain.Models;
using Microsoft.AspNetCore.Components;
using Service.Services;

namespace BlazorServerDemo.Pages
{
    public partial class EmployeeDetailsBase: ComponentBase
    {
        [Inject]
        protected IEmployeeService _employeeService { get; set; }
        public Employee Employee { get; set; } = new Employee();
        [Parameter]
        public String Id { get; set; }
        protected  async override Task OnInitializedAsync()
        {
            Id = Id ?? "1";
            Employee = await _employeeService.GetEmployee(int.Parse(Id));
        }
    }
}
