using Microsoft.AspNetCore.Mvc;
using WebApplication2.Exceptions;

namespace WebApplication2.Controllers;
[ApiController]
[Route("/api/[controller]")]
public class PrescribtionController : ControllerBase
{
    private readonly IPrescribtionService _prescribtionService;

    public PrescribtionController(IPrescribtionService prescribtionService)
    {
        _prescribtionService = prescribtionService;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> GetInformationAboutPrescriibtion([FromBody] PrescriptionDto dto)
    {
        try
        {
            var idMedication = await _prescribtionService.GetInformationAboutPrescriibtion(dto);
            return Ok(idMedication);
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
    }
    
}

