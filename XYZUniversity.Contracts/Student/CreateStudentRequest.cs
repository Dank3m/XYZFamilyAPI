namespace XYZUniversity.Contracts.Student;

public record CreateStudentRequest(
    string FirstName,
    string LastName,
    DateTime DateOfBirth,
    string Gender,
    string Stream,
    int Id);