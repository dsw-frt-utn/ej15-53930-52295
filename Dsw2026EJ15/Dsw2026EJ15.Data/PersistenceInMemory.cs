using System;
using System.Collections.Generic;
using Dsw2026EJ15.Domain.Interfaces;
using System.Text.Json;
using Dsw2026EJ15.Domain.Entities;
using Dsw2026EJ15.Data.Dtos;

namespace Dsw2026EJ15.Data
{
    public class PersistenceInMemory : IPersistence
    {
        private List<Speciality> _specialities = [];

        private List<Doctor> _doctors = [];
        public PersistenceInMemory()
        {

            LoadSpecialities();

        }

        private void LoadSpecialities()
        {
            try
            {
                string jsonPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Sources", "specialities.json");

                var json = File.ReadAllText(jsonPath);

                var specialities = JsonSerializer.Deserialize<List<SpecialityDto>>(json, new JsonSerializerOptions()
                {
                    PropertyNameCaseInsensitive = true
                }) ?? [];

                _specialities = [.. specialities.Select(s => new Speciality(s.Name, s.Description, s.Id))];

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading specialities: {ex.Message}");
            }
        }

        public Speciality? GetSpecialityById(Guid id)
        {
            return _specialities.SingleOrDefault(e => e.Id == id);
        }

        public void SaveDoctor(Doctor doctor)
        {
            _doctors.Add(doctor);
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
