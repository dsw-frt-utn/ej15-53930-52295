using Dsw2026EJ15.Api.Models;
using Dsw2026EJ15.Domain.Entities;
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

    [HttpGet]
    public async Task<IActionResult> GetaActiveDoctors(DoctorModel.Request request)
    {
        var doctors = await _persistence.GetDoctorsAsync();
        var response = doctors
            .Where(d => d.IsActive)
            .Select(d => new DoctorModel.Response(
            d.Id,
            d.Name,
            d.LicenseNumber,
            d.Speciality?.Name ?? String.Empty));
        return Ok(response);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetDoctorById(Guid id)
    {
        var doctor = await _persistence.GetDoctorByIdAsync(id);
        if (doctor == null || !doctor.IsActive)
        {
            return NotFound();
        }

        var response = new DoctorModel.Response(
            doctor.Id,
            doctor.Name,
            doctor.LicenseNumber,
            doctor.Speciality?.Name ?? String.Empty);
        return Ok(response);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteDoctor(Guid id)
    {
        var doctor = await _persistence.GetDoctorByIdAsync(id);

        if (doctor == null || !doctor.IsActive)
        {
            return NotFound();
        }

        doctor.IsActive = false;
        await _persistence.UpdateDoctorAsync(doctor);
        return NoContent();
    }

}
