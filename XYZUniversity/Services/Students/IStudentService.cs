using XYZUniversity.Models;
using XYZUniversity.Repositories.Students;
using ErrorOr;

namespace XYZUniversity.Services.Students;

public interface IStudentService
{
    ErrorOr<Created> CreateStudent(Student student);
    ErrorOr<Student> GetStudent(//Guid id
                                    int id);
    ErrorOr<UpsertedStudent> UpsertStudent(Student student);
    ErrorOr<Deleted> DeleteStudent(//Guid id
                                        int id);
}

