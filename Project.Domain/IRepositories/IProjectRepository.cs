using Project.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ProjectEntity = Project.Domain.AggregatesModel.Project;
namespace Project.Domain.IRepositories
{
    public interface IProjectRepository : IRepository<ProjectEntity>
    {
        Task<ProjectEntity> GetAsync(int id);

        ProjectEntity Add(ProjectEntity projectEntity);

        void Update(ProjectEntity projectEntity);
    }
}
