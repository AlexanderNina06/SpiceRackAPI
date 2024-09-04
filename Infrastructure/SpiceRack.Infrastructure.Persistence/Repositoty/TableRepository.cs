using SpiceRack.Core.Application.Interfaces.Repositories;
using SpiceRack.Core.Domain.Entities;
using SpiceRack.Infrastructure.Persistence.Contexts;

namespace SpiceRack.Infrastructure.Persistence.Repositoty;

public class TableRepository : GenericRepository<Table>, ITableRepository
{
  private readonly ApplicationContext _db;
    public TableRepository(ApplicationContext context) : base(context)
    {
      _db = context;
    }
}