// The Service handles the logic

using API_Database.Data;
using API_Database.Models;

namespace API_Database.Services.Students;

// When we inherit from IStudentService, we have to follow the blueprint that it laid out, all of the Methods, their names, return types, and parameters
public class StudentService : IStudentService
{
    private readonly DataContext _db;

    public StudentService(DataContext db) // Injecting our DataContext file, which represents our Database
    {
        _db = db;
    }

    public List<Student> AddStudent(string studentName)
    {
        Student newStudent = new(); // Creating a new Student Class - From our Student Model
        // Setting the Name property of our Student
        // We do not need to set the Id property because EF Core is smart and knows to add +1, to the previous Students Id, for each new Student
        newStudent.Name = studentName;

        _db.Students.Add(newStudent); // Adding the newStudent to the Students table on our Database
        _db.SaveChanges(); // Saving the Database changes

        return _db.Students.ToList(); // Returning our Students table as a List
    }

    public List<Student> DeleteStudent(string studentName)
    {
        // When giving the lecture I had the variable names for foundStudent and student swapped, but
        // I think it makes more sense, readability wise, with their variables like this
        var foundStudent = _db.Students.FirstOrDefault(student => student.Name == studentName);
        if(foundStudent != null) {
            _db.Students.Remove(foundStudent);
            _db.SaveChanges();
        }
        return _db.Students.ToList();
    }

    public List<Student> GetStudents()
    {
        return _db.Students.ToList();
    }

    public List<Student> UpdateStudent(string oldName, string newName)
    {
        // When giving the lecture I had the variable names for foundStudent and student swapped, but
        // I think it makes more sense, readability wise, with their variables like this
        var foundStudent = _db.Students.FirstOrDefault(student => student.Name == oldName);
        if(foundStudent != null) {
            foundStudent.Name = newName;
            _db.SaveChanges();
        }
        return _db.Students.ToList();
    }
}