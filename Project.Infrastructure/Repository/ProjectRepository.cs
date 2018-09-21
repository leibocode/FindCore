using Project.Domain.AggregatesModel;
using Project.Domain.IRepositories;
using Project.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ProjectEntity = Project.Domain.AggregatesModel.Project;s
namespace Project.Infrastructure.Repository
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly ProjectContext _dbContext;
        public ProjectRepository(ProjectContext projectContext)
        {
            _dbContext = projectContext;
        }

        public IUnitOfWork unitOfWork => _dbContext;

        public ProjectEntity Add(ProjectEntity projectEntity)
        {
            throw new NotImplementedException();
        }

        public Task<ProjectEntity> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(ProjectEntity projectEntity)
        {
            _dbContext.Projects.Update(projectEntity);
        }
    }
}
