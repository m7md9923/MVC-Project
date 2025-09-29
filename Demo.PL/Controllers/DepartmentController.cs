using System.Runtime.InteropServices.JavaScript;
using Demo.BLL.DTOS;
using Demo.BLL.DTOS.DepartmentDTOS;
using Demo.BLL.Services;
using Demo.BLL.Services.Classes;
using Demo.BLL.Services.Interfaces;
using Demo.DAL.Data.Contexts;
using Demo.DAL.Data.Repositories;
using Demo.PL.Models;
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

    #region Edit

    [HttpGet]
    public IActionResult Edit(int? id)
    {
        if (!id.HasValue)
            return BadRequest(); // status code = 400
        var department = _departmentService.GetById(id.Value);
        if (department == null)
            return NotFound(); // status code = 404
        var departmentVM = new DepartmentEditViewModel()
        {
            Code = department.Code,
            Description = department.Description,
            Name = department.Name,
            CreatedOn = department.CreatedOn.HasValue ? department.CreatedOn.Value : default
            // Default ==> 1/1/0001
        };
        return View(departmentVM);
    }

    [HttpPost]
    public IActionResult Edit([FromRoute] int? id, DepartmentEditViewModel departmentVM)
    {
        if (ModelState.IsValid)
        {
            try
            {
                if (!id.HasValue) return BadRequest(); // status code = 400
                var updatedDeptDto = new UpdateDepartmentDto()
                {
                    Id = id.Value,
                    Code = departmentVM.Code,
                    Description = departmentVM.Description,
                    Name = departmentVM.Name,
                    DateOfCreation = departmentVM.CreatedOn
                };
                var res = _departmentService.UpdateDepartment(updatedDeptDto);
                if (res > 0)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Department Update Failed");
                }
            }
            catch (Exception e)
            {
                // Development ==> Action, log error in console, View 
                // Deployment ==> Action, Log Error in file, Db, Return View (Error)
                if (_environment.IsDevelopment())
                {
                    _logger.LogError($"Deprtment update failed due to : {e.Message}"); // log in console
                }
                else
                {
                    _logger.LogError($"Deprtment update failed due to : {e}");
                    return View("ErrorView", e);
                }
            }
        }

        return View(departmentVM);
    }
    #endregion

    #region Delete
    
    // // Get --> render the view
    // [HttpGet]
    // public IActionResult Delete(int? id)
    // {
    //     if (!id.HasValue)
    //         return BadRequest(); // status code = 400
    //     var department = _departmentService.GetById(id.Value);
    //     if (department is null)
    //         return NotFound(); // status code = 404
    //     return View(department);
    // }

    [HttpPost]
    public IActionResult Delete(int id)
    {
        if(id <= 0)
            return BadRequest(); // status code = 400
        try
        {
            bool isDeleted = _departmentService.RemoveDepartment(id);
            if (isDeleted)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Department Deletion Failed");
                return RedirectToAction("Delete", new { id }); // Redirect to Delete
            }
        }
        catch (Exception e)
        {
            // Development ==> Action, log error in console, View 
            // Deployment ==> Action, Log Error in file, Db, Return View (Error)
            if (_environment.IsDevelopment())
            {
                _logger.LogError($"Deprtment Delete failed due to : {e.Message}"); // log in console
            }
            else
            {
                // File, Db
                _logger.LogError($"Deprtment Delete failed due to : {e}");
            }
        }
        return RedirectToAction("Delete", new { id }); 
    }
    #endregion
}