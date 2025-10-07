using System.Reflection.Metadata;
using Demo.BLL.Mappings;
using Demo.BLL.Services;
using Demo.BLL.Services.AttachmentService;
using Demo.BLL.Services.Classes;
using Demo.BLL.Services.Interfaces;
using Demo.DAL.Data.Contexts;
using Demo.DAL.Data.Repositories;
using Demo.DAL.Data.Repositories.Classes;
using Demo.DAL.Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

internal class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        #region DI Container

        builder.Services.AddControllersWithViews(opt =>
        {
            opt.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
        });
        
        builder.Services.AddControllersWithViews();
        // LifeTime [Objects] ==>  AddScoped , AddSingleton , Addtranisent
        builder.Services.AddScoped<ApplicationDbContext>(); 
        // AddDbcontext ==> Allow DI DbContext  
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
        {
            // options.UseSqlServer("connection string");
            // options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString"));
            // options.UseSqlServer(builder.Configuration.GetSection("ConnectionString")["DefaultConnectionString"]);
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString"));
            options.UseSqlServer("Server=.;Database=Demo;Trusted_Connection=True;TrustServerCertificate=True;");
            options.UseLazyLoadingProxies();
        });
        builder.Services.AddScoped<IDepartmentRepository , DepartmentRepository>();
        builder.Services.AddScoped<IDepartmentService , DepartmentService>();
        builder.Services.AddScoped<IEmployeeRepository , EmployeeRepository>();
        builder.Services.AddScoped<IEmployeeService , EmployeeService>();
        builder.Services.AddScoped<IUnitOfWork , UnitOfWork>();
        builder.Services.AddScoped<IAttachmentService , AttachmentService>();
        // ask u to create obj from class imp IDepartmentRepository  ==> new instance from Department Repository 
        // builder.Services.AddAutoMapper(cfg => {} ,typeof(AssemblyReference).Assembly);
        builder.Services.AddAutoMapper(Mapping => Mapping.AddProfile(new MappingProfile()));  // Singleton 
        #endregion

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error"); 
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();  // middleware that may sure all requests secured in the deployment https
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles(); 

        app.UseRouting();

        app.UseAuthentication();  // Login 
        app.UseAuthorization();  // Roles

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
}

/*
 * Department module
 * -- Model 
 * ==> Name , Code, Description [Props, Department]
 * ==> Id, Created By, Created On, Modified By, Modifies On, Is Deleted {Soft Delete}  [All Models] [Parent Class]
 * 
*/