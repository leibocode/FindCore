using MediatR;
using Microsoft.EntityFrameworkCore;
using Project.Domain.SeedWork;
using Project.Infrastructure.EntityConfiguration;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Project.Infrastructure
{
    public class ProjectContext : DbContext, IUnitOfWork
    {
        private IMediator _mediator;
        
        public DbSet<Domain.AggregatesModel.Project> Projects { get; set; }

        public ProjectContext(DbContextOptions<ProjectContext> options,IMediator mediator) : base(options)
        {
            _mediator = mediator;
        }
      

        public Task<bool> SaveEntitesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProjectEntityConfiguration());
        }
    }
}
