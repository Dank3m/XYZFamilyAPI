using Microsoft.AspNetCore.Mvc;

namespace XYZUniversity.Controllers;

public class ErrorsController : ControllerBase
{
    [Route("/error")]
    public IActionResult Error()
    {
        return Problem();
    }
}