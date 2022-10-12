using XYZUniversity.Models;
using XYZUniversity.Repositories.Students;
using ErrorOr;

namespace XYZUniversity.Services.Students;

public class StudentService : IStudentService
{
    private readonly IStudentRepository _studentRepo;

    public StudentService (IStudentRepository studentRepo)
    {
        _studentRepo = studentRepo;
    }

    public ErrorOr<Created> CreateStudent(Student student)
    {
        //In Memory
        // return _studentRepo.CreateStudent(student);
        //For Database
        return _studentRepo.CreateStudentDB(student);
    }

    public ErrorOr<Deleted> DeleteStudent(//Guid id
                                            int id)
    {
        //In Memory
        // return _studentRepo.DeleteStudent(id);
        //For Database
        return _studentRepo.DeleteStudentDB(id);
    }

    public ErrorOr<Student> GetStudent(//Guid id
                                            int id)
    {
        //In Memory
        // return _studentRepo.GetStudent(id);
        //For Database
        return _studentRepo.GetStudentDB(id);
    }

    public ErrorOr<UpsertedStudent> UpsertStudent(Student student)
    {
        //In Memory
        // return _studentRepo.UpsertStudent(student);
        //For Database
        return _studentRepo.UpsertStudentDB(student);
    }
}