using System;
using System.Collections.Generic;
using Dsw2026EJ15.Domain.Interfaces;
using System.Text.Json;
using Dsw2026EJ15.Domain.Entities;

namespace Dsw2026EJ15.Data
{
    public class PercistenceInMemory: IPersistence
    {
        private readonly List<Speciality> _specialities = [];

        private readonly List<Doctor> _doctors = [];
        public PercistenceInMemory()
        {

            LoadSpecialities();

        }

        private void LoadSpecialities()
        {
            try
            {
                var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "specialities.json");

                if (File.Exists(path)) return;

                var json = File.ReadAllText(path);

                var list = JsonSerializer.Deserialize<List<Speciality>>(json);

                if (list is not null)
                {
                    _specialities.AddRange(list);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading specialities: {ex.Message}");
            }
        }

        public async Task<IEnumerable<Speciality>> GetSpecialitiesAsync() => await Task.FromResult(_specialities);

        public async Task<Speciality?> GetSpecialityByIdAsync(Guid id) => await Task.FromResult(_specialities.Find(s => s.Id == id));

        public async Task<IEnumerable<Doctor>> GetDoctorsAsync() => await Task.FromResult(_doctors);

        public async Task<Doctor?> GetDoctorByIdAsync(Guid id) => await Task.FromResult(_doctors.Find(d => d.Id == id));

        public async Task AddDoctorAsync(Doctor doctor)
        {
            _doctors.Add(doctor);
            await Task.CompletedTask;
        }

        public async Task UpdateDoctorAsync(Doctor doctor)
        {
            var index = _doctors.FindIndex(d => d.Id == doctor.Id);
            if (index != -1)
            {
                _doctors[index] = doctor;
            }
            await Task.CompletedTask;
        }

    }
}
