using System;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.DependencyInjection;

namespace Webjob.Registration
{
    public class InversionOfControlJobActivator : IJobActivator
    {
        private readonly IServiceProvider _serviceProvider;

        public InversionOfControlJobActivator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public T CreateInstance<T>()
        {
            return _serviceProvider.GetService<T>();
        }
    }
}
