using XYZUniversity.Contracts.Student;
using XYZUniversity.Models;
using XYZUniversity.Services.Students;
using XYZUniversity.Repositories.Students;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;

namespace XYZUniversity.Controllers;

public class StudentsController : ApiController
{
    private readonly IStudentService _studentService;

    public StudentsController(IStudentService studentService)
    {
        _studentService = studentService;
    }

    [HttpPost]
    public IActionResult CreateStudent(CreateStudentRequest request)
    {
        ErrorOr<Student> requestToStudentResult = Student.From(request);

        if (requestToStudentResult.IsError)
        {
            return Problem(requestToStudentResult.Errors);
        }

        var student = requestToStudentResult.Value;
        ErrorOr<Created> createStudentResult = _studentService.CreateStudent(student);

        return createStudentResult.Match(
            created => CreatedAtGetStudent(student),
            errors => Problem(errors));
    }

    [HttpGet("{id:int}")]
    public IActionResult GetStudent(//Guid id
                                    int id)
    {
        ErrorOr<Student> getStudentResult = _studentService.GetStudent(id);

        return getStudentResult.Match(
            student => Ok(MapStudentResponse(student)),
            errors => Problem(errors));
    }

    [HttpPut("{id:int}")]
    public IActionResult UpsertStudent(//Guid id,
                                            int id, 
                                            UpsertStudentRequest request)
    {
        ErrorOr<Student> requestToStudentResult = Student.From(id, request);

        if (requestToStudentResult.IsError)
        {
            return Problem(requestToStudentResult.Errors);
        }

        var student = requestToStudentResult.Value;
        ErrorOr<UpsertedStudent> upsertStudentResult = _studentService.UpsertStudent(student);

        return upsertStudentResult.Match(
            upserted => upserted.IsNewlyCreated ? CreatedAtGetStudent(student) : NoContent(),
            errors => Problem(errors));
    }

    [HttpDelete("{id:int}")]
    public IActionResult DeleteStudent(//Guid id
                                            int id)
    {
        ErrorOr<Deleted> deleteStudentResult = _studentService.DeleteStudent(id);

        return deleteStudentResult.Match(
            deleted => NoContent(),
            errors => Problem(errors));
    }

    private static StudentResponse MapStudentResponse(Student student)
    {
        return new StudentResponse(
            student.Id,
            student.FirstName,
            student.LastName,
            student.DateOfBirth,
            student.Gender,
            student.Stream);
    }

    private CreatedAtActionResult CreatedAtGetStudent(Student student)
    {
        return CreatedAtAction(
            actionName: nameof(GetStudent),
            routeValues: new { id = student.Id },
            value: MapStudentResponse(student));
    }
}