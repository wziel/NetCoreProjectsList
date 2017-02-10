using Microsoft.EntityFrameworkCore;
using NetCoreProjectsList.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;
using NetCoreProjectsList.Componenets.Paging;

namespace NetCoreProjectsList.Repositories
{
    public class ProjectsRepository : BaseRepository<Project, int>, IProjectsRepository
    {
        public ProjectsRepository(ProjectsDbContext dbContext) :
            base(dbContext.Projects, dbContext)
        {
        }

        public override Expression<Func<Project, bool>> GetByIdPredicate(int id)
        {
            return p => p.ProjectId == id;
        }

        public Project SingleOrDefaultWithTasks(int id)
        {
            return DbSet.Include(p => p.Tasks).SingleOrDefault(GetByIdPredicate(id));
        }

        public PagedList<Project> GetAll(string nameFilter, PagingInfo pagingInfo)
        {
            IQueryable<Project> query = DbSet;
            if (nameFilter != null && nameFilter.Length > 0)
            {
                query = query.Where(p => p.Name.Contains(nameFilter));
            }
            return query.ToPagedList(pagingInfo);
        }
    }

    public interface IProjectsRepository : IBaseRepository<Project, int>
    {
        Project SingleOrDefaultWithTasks(int projectId);
        PagedList<Project> GetAll(string nameFilter, PagingInfo pagingInfo);
    }
}
