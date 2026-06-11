using System;
using System.Collections.Generic;
using System.Text;

namespace RestAprilEducation.Application
{
    public interface IUnitOfWork
    {
        Task<int> CommitAsync();


    }
}
