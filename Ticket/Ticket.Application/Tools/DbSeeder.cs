using Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticket.Domain.Context;
using Ticket.Domain.Models;

using Microsoft.AspNetCore.Identity;
using Ticket.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

public static class DbSeeder
{
    public static async Task SeedAsync(
        IUserService _userService,
        IPasswordHasher<User> passwordHasher)
    {
        if  (await  _userService.GetQuery().AnyAsync())
            return;

        var admin = new User
        {
            Id = Guid.NewGuid().ToString(),
            FullName = "System Admin",
            Email = "admin@test.com",
            Role = UserRole.Admin.ToString()
        };

        admin.PasswordHash = passwordHasher.HashPassword(admin, "123456");

        var employee = new User
        {
            Id = Guid.NewGuid().ToString(),
            FullName = "Test Employee",
            Email = "employee@test.com",
            Role = UserRole.Employee.ToString()
        };

        employee.PasswordHash = passwordHasher.HashPassword(employee, "123456");
        await _userService.Add(admin);
        await _userService.Add(employee);
    }


}
