namespace XYZUniversity.Contracts.Student;

public record StudentResponse(
    // Guid Id,
    int Id,
    string FirstName,
    string LastName,
    DateTime DateOfBirth,
    string Gender,
    string Stream);