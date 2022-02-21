using CPTR319_DB_Example_EF.Models;
using Microsoft.EntityFrameworkCore;


//List student names along with their majors
var context = new UniversityContext();
var students = context.Students.ToList();
foreach (var student in students)
{
    Console.WriteLine($"Name = {student.Name}, Major = {student.DeptName}");
}

//List student names along with their classes ordered nicely by year, semester...
var studentclasses = context.Students
    .Include(s => s.Takes)
        .ThenInclude(t => t.Section)
            .ThenInclude(e => e.Course)
    .OrderBy(s => s.Name);
foreach (var sc in studentclasses)
{
    Console.WriteLine($"STUDENT: {sc.Name}, Major: {sc.DeptName}");
    var takes = sc.Takes.OrderBy(x => x.Year).ThenBy(y => y.Semester);
    foreach (var ta in takes)
    {
        Console.WriteLine($"   {ta.Year} - {ta.Semester}: {ta.Section.Course.Title}");
    }
}
Console.WriteLine("\r\n\r\n----Next Query----\r\n\r\n");

//Let's filter the afore mentioned query for those with 80 or more credits.
var studentclasses2 = context.Students
    .Include(s => s.Takes)
        .ThenInclude(t => t.Section)
            .ThenInclude(e => e.Course)
    .Where(s => s.TotCred >= 80)
    .OrderBy(s => s.Name);
foreach (var sc in studentclasses2)
{
    Console.WriteLine($"STUDENT: {sc.Name}, Major: {sc.DeptName}, Credits: {sc.TotCred}");
    var takes = sc.Takes.OrderBy(x => x.Year).ThenBy(y => y.Semester);
    foreach (var ta in takes)
    {
        Console.WriteLine($"   {ta.Year} - {ta.Semester}: {ta.Section.Course.Title}");
    }
}
Console.WriteLine("\r\n\r\n----Next Query----\r\n\r\n");

//Let's count the number of classes taken by students with 80 or more credits 
var studentclasses3 = context.Students
    .Include(s => s.Takes);
foreach (var sc in studentclasses3)
{
    Console.WriteLine($"Name: {sc.Id}->{sc.Name}, Major: {sc.DeptName}, Hours: {sc.TotCred}, Classes: {sc.Takes.Count()}");
}
Console.WriteLine("\r\n\r\n----Next Query----\r\n\r\n");

//Althernative ways of doing this using Link to SQL 
var list = from o in context.Students
           where o.TotCred >= 80
           from t in o.Takes
           let foo = new
           {
               Name = o.Name,
               Id = o.Id,
               Major = o.DeptName,
               Hours = o.TotCred,
               Classes = o.Takes.Count()
           }
           orderby foo.Name, foo.Classes descending
           select foo;

foreach (var l in list)
{
    Console.WriteLine($"Name: {l.Id}->{l.Name}, Major: {l.Major}, Hours: {l.Hours}, Classes: {l.Classes}");
}
