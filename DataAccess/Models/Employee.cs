using System;
using System.Collections.Generic;

namespace DataAccess.Models;

public partial class Employee
{
    public int EmployeeId { get; set; }

    public int UserId { get; set; }

    public string FullName { get; set; } = null!;

    public DateOnly BirthDate { get; set; }

    public string Gender { get; set; } = null!;

    public string? Address { get; set; }

    public string PhoneNumber { get; set; } = null!;

    public int? DepartmentId { get; set; }

    public string? Position { get; set; }

    public decimal? Salary { get; set; }

    public DateOnly StartDate { get; set; }

    public byte[]? Avatar { get; set; }

    public virtual ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();

    public virtual Department? Department { get; set; }

    public virtual ICollection<LeaveRequest> LeaveRequests { get; set; } = new List<LeaveRequest>();

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    public virtual ICollection<Salary> Salaries { get; set; } = new List<Salary>();

    public virtual User User { get; set; } = null!;
}
