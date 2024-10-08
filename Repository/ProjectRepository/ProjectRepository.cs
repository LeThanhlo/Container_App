using Container_App.Data;
using Container_App.Model;

namespace Container_App.Repository.ProjectRepository
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly MyDbContext _context;

        public ProjectRepository(MyDbContext context)
        {
            _context = context;
        }
        public async Task<Project> AddProjectAsync(Project project)
        {
            _context.Projects.Add(project);
            await _context.SaveChangesAsync();
            return project;
        }
    }
}
