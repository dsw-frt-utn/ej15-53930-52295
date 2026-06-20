using Dsw2026EJ15.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dsw2026EJ15.Domain.Interfaces
{
    public interface IPersistence
    { 
        
        IEnumerable<Speciality> GetSpecialities();
        Speciality? GetSpecialityById(Guid id);

        IEnumerable<Doctor> GetDoctors();
        Doctor? GetDoctorById(Guid id);

        void SaveDoctor(Doctor doctor);
        void UpdateDoctor(Doctor doctor);
    }
}
