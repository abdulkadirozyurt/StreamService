using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StreamService.Business.Abstract;
using StreamService.Core.Entities.Constants;
using StreamService.DataAccess.Abstract;
using StreamService.Entities.Concrete;

namespace StreamService.Business.Concrete
{
    public class RoleSeederHostedService : IRoleSeederHostedService
    {
        private readonly IServiceProvider _serviceProvider;

        public RoleSeederHostedService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var services = scope.ServiceProvider;
                var roleDal = services.GetRequiredService<IRoleDal>();

                var roles = new List<string> { UserRoleConstants.Admin, UserRoleConstants.User };

                foreach (var roleName in roles)
                {
                    if (await roleDal.GetByNameAsync(roleName) == null)
                    {
                        await roleDal.CreateAsync(new Role { Name = roleName });
                    }
                }
            }
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
