using System;
using System.Collections.Generic;
using System.Text;

namespace Dsw2026EJ15.Domain.Entities
{
    public class Doctor : BaseEntity
    {
        public string Name { get; init; } = string.Empty;

        public string LicenseNumber  { get; init; } = string.Empty;

        public bool IsActive { get; set; } = true;

        public Speciality Speciality { get; init; } 

        public Doctor(string name,string licenseNumber,Speciality speciality) 
        {
            Name = name;
            LicenseNumber = licenseNumber;
            IsActive = true;
            Speciality = speciality;
            IsActive = true;

        }
    }
}
