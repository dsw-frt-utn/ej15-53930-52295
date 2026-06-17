using Dsw2026EJ15.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dsw2026EJ15.Domain.Interfaces
{
    public interface IPersistence
    {
        Task<IEnumerable<Speciality>> GetSpecialitiesAsync();
        Task<Speciality?> GetSpecialityByIdAsync(Guid id);

        Task<IEnumerable<Doctor>> GetDoctorsAsync();
        Task<Doctor?> GetDoctorByIdAsync(Guid id);

        Task AddDoctorAsync(Doctor doctor);
        Task UpdateDoctorAsync(Doctor doctor);
    }
}
