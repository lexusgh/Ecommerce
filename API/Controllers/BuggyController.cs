using API.Errors;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class BuggyController : BaseApiController
    {
    

    private readonly StoreContext _context;

    public BuggyController(StoreContext context)
    {
        _context = context;
    }

    [HttpGet("notfound")]

    public ActionResult GetNotFound()
    {
        var thing = _context.Products.Find(42);

        if( thing == null )
        {
            return NotFound(new ApiResponse(404));
        }
        return Ok();
    }


     [HttpGet("servererror")]

    public ActionResult GetServerError()
    {
        var thing = _context.Products.Find(42);
        var thingtoReturn = thing.ToString();
        return Ok();
    }

     [HttpGet("badrequest")]

    public ActionResult GetbadRequest()
    {
        return BadRequest(new ApiResponse(404));
    }

    [HttpGet("badrequest/{id}")]

    public ActionResult GetbadRequest(int id)
    {
        return Ok();
    }
}
}