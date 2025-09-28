using Demo.BLL.DTOS.EmployeeDTOS;
using Demo.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Demo.PL.Controllers;

public class EmployeeController(IEmployeeService _employeeService , IWebHostEnvironment _environment , ILogger<EmployeeController> _logger) : Controller
{
    #region Index

    // Master Action 
    // BaseURL/Employee/Index ==> Send data Controller --> View
    [HttpGet]
    public IActionResult Index()
    {
        var employeeDtos = _employeeService.GetAllEmployees();
        return View(employeeDtos);
    }
    #endregion

    #region Create

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(CreateEmployeeDto employeeDto)
    {
        if (ModelState.IsValid)
        {
            try
            {
                int res = _employeeService.CreateEmployee(employeeDto);
                if (res > 0) // Success
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Employee Creation Failed");
                }
            }
            catch (Exception e)
            {
                if (_environment.IsDevelopment())
                {
                    _logger.LogError($"Employee creation failed due to : {e.Message}"); // log in console
                }else {
                    _logger.LogError($"Employee creation failed due to : {e}");
                    return View("ErrorView" , e);
                }
            }
        }
        return View(employeeDto);
    }
    #endregion
}
