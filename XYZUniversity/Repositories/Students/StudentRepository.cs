using Microsoft.EntityFrameworkCore;
using XYZUniversity.Data;
using XYZUniversity.Models;
using XYZUniversity.ServiceErrors;
using ErrorOr;

namespace XYZUniversity.Repositories.Students;

public class StudentRepository : IStudentRepository
{
    //In memory retrieval
    // private static readonly Dictionary<Guid, Student> _students = new();
    private static readonly Dictionary<int, Student> _students = new();

    public ErrorOr<Created> CreateStudent(Student student)
    {
        // _students.Add(student.Id, student);

        return Result.Created;
    }

    public ErrorOr<Deleted> DeleteStudent(//Guid id
                                            int id)
    {
        _students.Remove(id);

        return Result.Deleted;
    }

    public ErrorOr<Student> GetStudent(//Guid id
                                            int id)
    {
        if (_students.TryGetValue(id, out var student))
        {
            return student;
        }

        return Errors.Student.NotFound;
    }

    public ErrorOr<UpsertedStudent> UpsertStudent(Student student)
    {
        var isNewlyCreated = !_students.ContainsKey(student.Id);
        _students[student.Id] = student;

        return new UpsertedStudent(isNewlyCreated);
    }

    //Database Retrieval

    private readonly DataContext _context;

    public StudentRepository(DataContext context)
    {
        _context = context;
    }

    public ErrorOr<Created> CreateStudentDB(Student student)
    {
        Console.WriteLine(student.FirstName);
        _context.Students.Add(student);
        _context.SaveChanges();

        return Result.Created;
    }

    public ErrorOr<Deleted> DeleteStudentDB(//Guid id
                                            int id)
    {
       try{
            ErrorOr<Student> student = (from s in _context.Students where s.Id 
                == id select s).First<Student>();

            _context.Students.Remove(student.Value);
            _context.SaveChanges();

        }  

        catch (Exception ex)
        {
            ex.GetBaseException();
            return Errors.Student.NotFound;
        }

        return Result.Deleted;
    }

    public ErrorOr<Student> GetStudentDB(//Guid id
    int id)
    {
        try{
            ErrorOr<Student> student = (from s in _context.Students where s.Id 
                == id select s).First<Student>();
            return student;
        }  

        catch (Exception ex)
        {
            ex.GetBaseException();
            return Errors.Student.NotFound;
        }
            
    }

    public ErrorOr<UpsertedStudent> UpsertStudentDB(Student student)
    {
        var isNewlyCreated = false;

        try{     
            _context.Students.Update(student);
            _context.SaveChanges();

        return new UpsertedStudent(isNewlyCreated);
            
        }  

        catch (Exception ex)
        {
            ex.GetBaseException();
            isNewlyCreated =  true;
            _context.Entry(student).State = EntityState.Detached;
            _context.Students.Add(Student.Create(student.FirstName, student.LastName,student.DateOfBirth,
                    student.Gender, student.Stream, student.Id).Value);
            _context.SaveChanges();
            return new UpsertedStudent(isNewlyCreated);
        }
    }

}