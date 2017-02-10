using Microsoft.EntityFrameworkCore;
using NetCoreProjectsList.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace NetCoreProjectsList.Repositories
{
    public class TasksRepository :  BaseRepository<Task, int>, ITasksRepository
    {
        public TasksRepository(ProjectsDbContext dbContext)
            :base(dbContext.Tasks, dbContext)
        {
        }

        public override Expression<Func<Task, bool>> GetByIdPredicate(int id)
        {
            return t => t.TaskId == id;
        }

        public Task GetWithProject(int id)
        {
            return DbSet.Include(t => t.Project).SingleOrDefault(GetByIdPredicate(id));
        }
    }

    public interface ITasksRepository : IBaseRepository<Task, int>
    {
        Task GetWithProject(int id);
    }
}
