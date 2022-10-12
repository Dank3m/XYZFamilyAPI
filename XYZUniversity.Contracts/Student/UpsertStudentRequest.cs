namespace XYZUniversity.Contracts.Student;

public record UpsertStudentRequest(
    string FirstName,
    string LastName,
    DateTime DateOfBirth,
    string Gender,
    string Stream,
    int Id);