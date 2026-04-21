using Microsoft.AspNetCore.Mvc.Rendering;
using Shopee.EmployeeCRM.Shared.Dtos;

namespace Shopee.EmployeeCRM.Mvc.ViewModels;

public class TaskFormViewModel
{
    public WorkTaskDto Task { get; set; } = new();

    public string PageTitle { get; set; } = string.Empty;

    public List<SelectListItem> Employees { get; set; } = [];

    public List<SelectListItem> StatusOptions { get; set; } = [];
}
