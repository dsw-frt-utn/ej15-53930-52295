using Dsw2026EJ15.Api.Models;
using Dsw2026EJ15.Domain; 
using Dsw2026EJ15.Domain.Entities;
using Dsw2026EJ15.Domain.Interfaces;
using Dsw2026EJ15.Domain.Exceptions;
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
    public async Task<IActionResult> CreateDoctor([FromBody] DoctorModel.Request request) 
    {
      
        if (string.IsNullOrWhiteSpace(request.Name))
        {
            throw new ValidationException("Name: requerido");
        }

        if (string.IsNullOrWhiteSpace(request.LicenseNumber))
        {
            throw new ValidationException("License Number: requerido"); 
        }

        
        var speciality = await _persistence.GetSpecialityByIdAsync(request.SpecialityId);
        if (speciality == null)
        {
            throw new ValidationException("SpecialityId: debe existir"); 
        }

        
        var doctor = new Doctor(request.Name, request.LicenseNumber, speciality);

        
        await _persistence.AddDoctorAsync(doctor);

       
        return Created(); 
    }

    
    [HttpGet]
    public async Task<IActionResult> GetaActiveDoctors() 
    {
       
        var doctors = await _persistence.GetDoctorsAsync();

        var response = doctors.Select(d =>
        {
            
            return new DoctorModel.Response(
                        d.Id,
                        d.Name,
                        d.LicenseNumber,
                        d.Speciality.Name);
        });

        return Ok(response);
    }

   
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetDoctorById(Guid id)
    {
       
        var doctor = await _persistence.GetDoctorByIdAsync(id);

        
        if (doctor == null)
        {
            return NotFound();
        }

        var response = new DoctorModel.Response(
            doctor.Id,
            doctor.Name,
            doctor.LicenseNumber,
            doctor.Speciality.Name);

        return Ok(response); 
    }

    
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteDoctor(Guid id)
    {
        
        var doctor = await _persistence.GetDoctorByIdAsync(id);

        if (doctor == null)
        {
            return NotFound(); 
        }

        
        doctor.IsActive = false;

        
        await _persistence.UpdateDoctorAsync(doctor);

        return NoContent();
    }
}