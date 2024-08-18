using AtomORM.Core;
using AtomORM.Test.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace AtomORM.Test.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly TestAtomContext _testAtomContext;

    public WeatherForecastController(TestAtomContext testAtomContext)
    {
        _testAtomContext = testAtomContext;

    }
    
    [HttpGet("ReadString")] 
    public IActionResult ReadString()
    {
        _testAtomContext.GetConnectionString();
        
        _testAtomContext.MapEntities();
        
        _testAtomContext.stringProperty.Add(new Person
        {
            name = "Temo",
            age = 20,
        });
        return Ok();
    }
}