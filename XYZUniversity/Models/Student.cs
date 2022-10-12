using XYZUniversity.Contracts.Student;
using XYZUniversity.ServiceErrors;
using ErrorOr;

namespace XYZUniversity.Models;

public class Student
{
    public const int MinFirstNameLength = 3;
    public const int MaxFirstNameLength = 50;

    public const int MinLastNameLength = 3;
    public const int MaxLastNameLength = 50;



    // public Guid Id { get; }

    public int Id {get; }
    public string FirstName { get; }
    public string LastName { get; }
    public DateTime DateOfBirth { get; }
    public string Gender { get; }
    public string Stream { get; }

    public virtual List<Payment> Payments {get; }

    public Student() {

    }

    private Student(
        // Guid id,
        int id,
        string firstName,
        string lastName,
        DateTime dateOfBirth,
        string gender,
        string stream)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        DateOfBirth = dateOfBirth;
        Gender = gender;
        Stream = stream;
    }

    public static ErrorOr<Student> Create(
        string firstName,
        string lastName,
        DateTime dateOfBirth,
        string gender,
        string stream,
        // Guid? id = null
        int id)
    {
        List<Error> errors = new();

        if (firstName.Length is < MinFirstNameLength or > MaxFirstNameLength)
        {
            errors.Add(Errors.Student.InvalidFirstName);
        }

        if (lastName.Length is < MinLastNameLength or > MaxLastNameLength)
        {
            errors.Add(Errors.Student.InvalidLastName);
        }

        if (errors.Count > 0)
        {
            return errors;
        }

        return new Student(
            // id ?? Guid.NewGuid(),
            id,
            firstName,
            lastName,
            dateOfBirth,
            gender,
            stream);
    }

    public static ErrorOr<Student> From(CreateStudentRequest request)
    {
        return Create(
            request.FirstName,
            request.LastName,
            request.DateOfBirth,
            request.Gender,
            request.Stream,
            request.Id);
    }

    public static ErrorOr<Student> From(//Guid id, 
                                        int id,         
                                        UpsertStudentRequest request)
    {
        return Create(
            request.FirstName,
            request.LastName,
            request.DateOfBirth,
            request.Gender,
            request.Stream,
            id);
    }
}