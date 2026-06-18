namespace Dsw2026EJ15.Api.Models;

public record DoctorModel
{
    public record Request(string Name, string LicenseNumber, Guid SpecialityId);
}
