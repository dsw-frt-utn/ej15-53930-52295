using Dsw2026EJ15.Api.Models;
using Dsw2026EJ15.Domain.Interfaces;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Dsw2026EJ15.Api.Controllers;

[ApiController]
[Route("api/doctors")]

public class DoctorsController : ControllerBase
{
    private readonly IPersistence _persistence;

    public DoctorsController(IPersistence persistence)
    {
        _persistence = persistence;
    }

    [HttpPost]
    public async Task<IActionResult> CreateDoctor(DoctorModel.Request request)
    {
        if (string.IsNullOrWhiteSpace(request.Name) || string.IsNullOrWhiteSpace(request.LicenseNumber))
        {
            return BadRequest("Nombre y matricula son requeridos");
        }
        
        var speciality = _persistence.GetSpecialityById(request.SpecialityId);
        if (speciality == null)
        {
            return NotFound("Especialidad no existe");
        }

        var doctor = new Doctor(request.Name, request.LicenseNumber, speciality);
        _persistence.SaveDoctor(doctor);

        return Created();
    }
}
