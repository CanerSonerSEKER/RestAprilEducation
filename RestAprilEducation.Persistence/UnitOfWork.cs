using RestAprilEducation.Application;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestAprilEducation.Persistence
{
    public class UnitOfWork(AppDbContext context) : IUnitOfWork
    {

        // Etkilenen satır sayısını döndürür.
        public async Task<int> CommitAsync()
        {
            return await context.SaveChangesAsync();
        }
    }
}
