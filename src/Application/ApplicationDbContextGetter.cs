using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Storage;
using System.Threading;
using System.Threading.Tasks;
using System.Data.Common;
using ChequeMicroservice.Application;
using Microsoft.EntityFrameworkCore.Diagnostics;
using ChequeMicroservice.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace ChequeMicroservice.Application
{
   
    public static class ApplicationDbContextGetter
    {
        public static DbContext GetDbContext<T>(this DbSet<T> dbSet) where T : class
        {
            IInfrastructure<IServiceProvider> infrastructure = dbSet as IInfrastructure<IServiceProvider>;
            IServiceProvider serviceProvider = infrastructure.Instance;
            ICurrentDbContext currentDbContext = serviceProvider.GetService(typeof(ICurrentDbContext))
                                       as ICurrentDbContext;
            return currentDbContext.Context;
        }
    }
}

