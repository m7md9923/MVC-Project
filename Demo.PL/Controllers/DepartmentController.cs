using Demo.BLL.Services;
using Demo.DAL.Data.Contexts;
using Demo.DAL.Data.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Demo.PL.Controllers;

public class DepartmentController : Controller
{
    private readonly DepartmentService _departmentService;

    public DepartmentController(DepartmentService departmentService)
    {
        _departmentService = departmentService;
    }

    private IActionResult Index()
    {
        return View();
    }
}