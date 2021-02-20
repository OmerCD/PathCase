using System.Collections.Generic;
using PathCase.Core.Entities;
using PathCase.Infrastructure.Services.Interfaces;

namespace PathCase.Infrastructure.Repositories
{
    public interface IRepository<T> : IScopedService where T : BaseEntity
    {
        IEnumerable<T> GetAll();
    }
}