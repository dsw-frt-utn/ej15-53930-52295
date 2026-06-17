using System;
using System.Collections.Generic;
using System.Text;

namespace Dsw2026EJ15.Domain.Entities
{
    public abstract class BaseEntity
    {
        public Guid Id { get; init; } = Guid.NewGuid();

    }
}
