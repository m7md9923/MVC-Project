using Demo.BLL.DTOS.EmployeeDTOS;
using Demo.BLL.Services.Interfaces;
using Demo.DAL.Models.EmployeeModule;
using Demo.DAL.Models.Shared;
using Demo.PL.Models;
using Microsoft.AspNetCore.Mvc;

namespace Demo.PL.Controllers;

public class EmployeeController(IEmployeeService _employeeService , IWebHostEnvironment _environment , ILogger<EmployeeController> _logger) : Controller
{
    #region Index

    // Master Action 
    // BaseURL/Employee/Index ==> Send data Controller --> View
    [HttpGet]
    public IActionResult Index(string? employeeSearchName)
    {
        var employeeDtos = _employeeService.GetAllEmployees(employeeSearchName);
        return View(employeeDtos);
    }
    #endregion

    #region Create

    [HttpGet]
    public IActionResult Create() // CLR Action Injection 
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(EmployeeViewModel employeeViewModel)
    {
        if (ModelState.IsValid)
        {
            try
            {
                int res = _employeeService.CreateEmployee(new CreateEmployeeDto()
                {
                    Name = employeeViewModel.Name,
                    Age = employeeViewModel.Age,
                    Address = employeeViewModel.Address,
                    Email = employeeViewModel.Email,
                    PhoneNumber = employeeViewModel.PhoneNumber,
                    HiringDate = employeeViewModel.HiringDate,
                    IsActive = employeeViewModel.IsActive,
                    Gender = employeeViewModel.Gender,
                    EmployeeType = employeeViewModel.EmployeeType,
                    Salary = employeeViewModel.Salary,
                    DepartmentId = employeeViewModel.DepartmentId,
                    Image = employeeViewModel.Image
                });
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
        return View(employeeViewModel);
    }
    #endregion

    #region Details

    [HttpGet]
    public IActionResult Details(int? id)
    {
        if(!id.HasValue)
            return BadRequest(); // status code = 400
        var employeeDto = _employeeService.GetById(id.Value);
        if(employeeDto is null)
            return NotFound(); // status code = 404
        return View(employeeDto);
    }
    
    #endregion

    #region Edit

    [HttpGet]
    public IActionResult Edit(int? id)
    {
        if(!id.HasValue)
            return BadRequest(); // status code = 400
        var employee = _employeeService.GetById(id.Value);
        if(employee is null)
            return NotFound(); // status code = 404
        var employeeViewModel = new EmployeeViewModel()  // Department Id
        {
            Name = employee.Name,
            Age = employee.Age,
            Salary = employee.Salary,
            Address = employee.Address,
            Email = employee.Email,
            PhoneNumber = employee.PhoneNumber,
            HiringDate = employee.HiringDate,
            IsActive = employee.IsActive,
            Gender = Enum.Parse<Gender>(employee.Gender),
            EmployeeType = Enum.Parse<EmployeeType>(employee.EmployeeType),
            DepartmentId = employee.DepartmentId
        };
        return View(employeeViewModel);
    }

    [HttpPost]
    public IActionResult Edit([FromRoute] int? id, EmployeeViewModel employeeViewModel)
    {
        if(!id.HasValue)
            return BadRequest(); // status code = 400
        if (ModelState.IsValid)
        {
            try
            {
                int res = _employeeService.UpdateEmployee(new UpdateEmployeeDto()
                {
                    Name = employeeViewModel.Name,
                    Age = employeeViewModel.Age,
                    Address = employeeViewModel.Address,
                    Email = employeeViewModel.Email,
                    PhoneNumber = employeeViewModel.PhoneNumber,
                    HiringDate = employeeViewModel.HiringDate,
                    IsActive = employeeViewModel.IsActive,
                    Gender = employeeViewModel.Gender,
                    EmployeeType = employeeViewModel.EmployeeType,
                    Salary = employeeViewModel.Salary,
                    Id = id.Value,  // Come From Route 
                    DepartmentId = employeeViewModel.DepartmentId
                });
                
                if (res > 0)
                    return RedirectToAction("Index");
                else
                {
                    ModelState.AddModelError("", "Employee Update Failed");
                }
            }
            catch (Exception e)
            {
                if (_environment.IsDevelopment())
                {
                    _logger.LogError($"Employee update failed due to : {e.Message}"); // log in console
                }
                else
                {
                    _logger.LogError($"Employee update failed due to : {e}");
                    return View("ErrorView", e);
                }
            }
        }

        return View(employeeViewModel);
    }

    #endregion
    
    #region Delete

    [HttpPost]
    public IActionResult Delete(int id)
    {
        if (id <= 0)
            return BadRequest(); // status code = 400
        try
        {
            bool isDeleted = _employeeService.RemoveEmployee(id);
            if (isDeleted)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Employee Deletion Failed");
                return RedirectToAction("Delete", new { id }); // Redirect to Delete
            }
        }
        catch (Exception e)
        {
            // Development ==> Action, log error in console, View 
            // Deployment ==> Action, Log Error in file, Db, Return View (Error)
            if (_environment.IsDevelopment())
            {
                _logger.LogError($"Employee Delete failed due to : {e.Message}"); // log in console
            }
            else
            {
                // File, Db
                _logger.LogError($"Employee Delete failed due to : {e}");
            }
        }

        return RedirectToAction("Delete", new { id });
    }

    #endregion
}
