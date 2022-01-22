using Microsoft.AspNetCore.Mvc;
using Pizza_App.Models;
using Pizza_App.Services;

namespace Pizza_App.Controllers;

/// <summary>
/// The class responsible for defining the pizza-related endpoints.
/// </summary>
/// <remarks>Because we call the class PizzaController, and we use the route [controller] our endpoints are all pizza/[whatever]</remarks>
[ApiController]
[Route("[controller]")]
public class PizzaController : ControllerBase
{


    private readonly ILogger<PizzaController> _logger;

    public PizzaController(ILogger<PizzaController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public ActionResult<List<Pizza>> GetAll() => PizzaService.GetAll();

    [HttpGet("{id}")]
    public ActionResult<Pizza> Get(int id)
    {
        var pizza = PizzaService.Get(id);
        if (pizza == null) return NotFound();
        return pizza;
    }


    [HttpPost]
    public IActionResult Create(Pizza pizza)
    {
        PizzaService.Add(pizza);
        return CreatedAtAction(nameof(Create), new { id = pizza.Id }, pizza);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, Pizza pizza)
    {
        if (id != pizza.Id) return BadRequest();
        var existingPizza = PizzaService.Get(id);
        if (existingPizza is null) return NotFound();
        PizzaService.Update(pizza);
        return NoContent();
    }


    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var pizza = PizzaService.Get(id);
        if (pizza is null) return NotFound();
        PizzaService.Delete(id);
        return NoContent();
    }
}
