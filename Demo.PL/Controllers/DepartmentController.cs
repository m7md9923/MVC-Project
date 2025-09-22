using System.Runtime.InteropServices.JavaScript;
using Demo.BLL.DTOS;
using Demo.BLL.Services;
using Demo.BLL.Services.Classes;
using Demo.BLL.Services.Interfaces;
using Demo.DAL.Data.Contexts;
using Demo.DAL.Data.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Demo.PL.Controllers;

public class DepartmentController(IDepartmentService _departmentService , IWebHostEnvironment _environment , ILogger<DepartmentController> _logger) : Controller
{

    #region Index

    // BaseURL/ Department/Index

    // Constructor injection
    public IActionResult Index()
    {
        var departments = _departmentService.GetAllDepartments() ?? new List<DepartmentDto>();
        return View(departments);
    }
    #endregion

    #region Create

    [HttpGet]
    public IActionResult Create()
    {
        return View(); // Same name as Action 
    }

    [HttpPost]
    public IActionResult Create(CreateDepartmentDto departmentDto)
    {
        if (ModelState.IsValid) // Server Side Validation
        {
            // Department ==> ManagerId [Employees 1-5]
            // Attribute ManagerId [range(1-100)]
            // MagaerId ==> 6
            try
            {
                int res = _departmentService.AddDpartment(departmentDto);
                if (res > 0) // Success
                {
                    return RedirectToAction("Index");
                }
                else 
                {
                    ModelState.AddModelError("", "Department Creation Failed");
                }
            }
            catch (Exception e)
            {   
                // Development ==> Action, log error in console, View 
                // Deployment ==> Action, Log Error in file, Db, Return View (Error)
                if (_environment.IsDevelopment())
                {
                        _logger.LogError($"Deprtment creation failed due to : {e.Message}"); // log in console
                }else {
                    _logger.LogError($"Deprtment creation failed due to : {e}"); 
                    return View("ErrorView");
                }
                
            }
        }
        return View(departmentDto);
    }
    
    #endregion

    #region Details

    [HttpGet]
    public IActionResult Details(int? id)
    {
        if (!id.HasValue)
            return BadRequest(); // status code = 400
        var department = _departmentService.GetById(id.Value);
        if (department == null)
            return NotFound(); // status code = 404
        return View(department);
    }

    #endregion
    
}