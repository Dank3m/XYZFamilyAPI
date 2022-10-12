using XYZUniversity.Models;
using ErrorOr;

namespace XYZUniversity.Repositories.Students;

public interface IStudentRepository
{
    ErrorOr<Created> CreateStudent(Student student);
    ErrorOr<Student> GetStudent(//Guid id,
                                    int id);
    ErrorOr<UpsertedStudent> UpsertStudent(Student student);
    ErrorOr<Deleted> DeleteStudent(//Guid id
                                        int id);
    ErrorOr<Created> CreateStudentDB(Student student);
    ErrorOr<Student> GetStudentDB(//Guid id,
                                    int id);
    ErrorOr<UpsertedStudent> UpsertStudentDB(Student student);
    ErrorOr<Deleted> DeleteStudentDB(//Guid id
                                        int id);
}
