using BuildingBlocks.Persistence.DBContex;
using BuildingBlocks.Persistence.Interfaces;
using BuildingBlocks.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuildingBlocks.Persistence.Repositories
{
    public abstract class BaseSQLServerRepository<T, TContext>(TContext context) : IBaseSQLServerRepository<T>
        where T : class
        where TContext : SQLServerBaseContext
    {
        protected TContext Context { get; set; } = context;

        public abstract Task<PagedResult<T>> GetAllAsync(int pageNumber = 1, int pageSize = 20);
        public abstract Task<T?> GetByAliasAsync(int id);
    }
}
