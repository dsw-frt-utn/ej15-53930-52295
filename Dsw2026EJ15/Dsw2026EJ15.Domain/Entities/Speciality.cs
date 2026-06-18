using System;
using System.Collections.Generic;
using System.Text;

namespace Dsw2026EJ15.Domain.Entities
{
    public class Speciality : BaseEntity
    {
        public string Name { get; init; } = string.Empty;

        public string Description { get; init; } = string.Empty;

        public Speciality(string name, string description, Guid? id = null) : base(id)
        {
            Name = name;
            Description = description;
        }

    }
}
