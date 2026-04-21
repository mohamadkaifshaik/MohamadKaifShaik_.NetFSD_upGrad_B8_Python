using Microsoft.AspNetCore.Mvc.Rendering;
using Shopee.EmployeeCRM.Shared.Dtos;

namespace Shopee.EmployeeCRM.Mvc.ViewModels;

public class ClientFormViewModel
{
    public ClientDto Client { get; set; } = new();

    public string PageTitle { get; set; } = string.Empty;

    public List<SelectListItem> Employees { get; set; } = [];
}
