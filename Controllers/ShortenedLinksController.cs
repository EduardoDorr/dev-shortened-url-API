using Microsoft.AspNetCore.Mvc;
using DevEncurtaUrl.API.Models;
using DevEncurtaUrl.API.Entities;
using DevEncurtaUrl.API.Persistence;
using Serilog;

namespace DevEncurtaUrl.API.Controllers
{
  [ApiController]
  [Route("api/shortenedLinks")]
  public class ShortenedLinksController : ControllerBase
  {
    private readonly DevShortenedLinkDbContext _context;

    public ShortenedLinksController(DevShortenedLinkDbContext context)
    {
      _context = context;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
      // Log.Information("GetAll is called!");
      return Ok(_context.Links);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
      var link = _context.Links.SingleOrDefault(l => l.Id == id);

      if (link == null)
        return NotFound();

      return Ok(link);
    }

    [HttpGet("/{code}")]
    public IActionResult RedirectLink(string code)
    {
      var link = _context.Links.SingleOrDefault(l => l.Code == code);

      if (link == null)
        return NotFound();

        return Redirect(link.DestinationLink);
    }

    /// <summary>
    /// Cadastrar um link encurtado
    /// </summary>
    /// <remarks>
    /// { "title": "ultimo-artigo Blog", "destinationLink": "http://127.0.0.1:5500/index.html" }
    /// </remarks>
    /// <param name="model">Dados do link a ser encurtado</param>
    /// <returns>Objeto recém criado</returns>
    /// <response code="201">Sucesso!</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public IActionResult CreateNewShortenedLink(AddOrUpdateShortenedLinkModel model)
    {
      var domain = HttpContext.Request.Host.Value;
      var link = new ShortenedCustomLink(model.Title, model.DestinationLink, domain);

      _context.Links.Add(link);
      _context.SaveChanges();

      return CreatedAtAction("GetById", new { id = link.Id }, link);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateShortenedLinkById(int id, AddOrUpdateShortenedLinkModel model)
    {
      var domain = HttpContext.Request.Host.Value;
      var link = _context.Links.SingleOrDefault(l => l.Id == id);

      if (link == null)
        return NotFound();

      link.Update(model.Title, model.DestinationLink, domain);
      
      _context.Links.Update(link);
      _context.SaveChanges();

      return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteShortenedLinkById(int id)
    {
      var link = _context.Links.SingleOrDefault(l => l.Id == id);

      if (link == null)
        return NotFound();

      _context.Links.Remove(link);
      _context.SaveChanges();

      return NoContent();
    }
  }
}