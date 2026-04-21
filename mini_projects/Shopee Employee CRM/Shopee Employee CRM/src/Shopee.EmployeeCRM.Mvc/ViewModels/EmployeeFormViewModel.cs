using Microsoft.AspNetCore.Mvc.Rendering;
using Shopee.EmployeeCRM.Shared.Dtos;

namespace Shopee.EmployeeCRM.Mvc.ViewModels;

public class EmployeeFormViewModel
{
    public EmployeeDto Employee { get; set; } = new();

    public string PageTitle { get; set; } = string.Empty;

    public List<SelectListItem> RoleOptions { get; set; } = [];
}
