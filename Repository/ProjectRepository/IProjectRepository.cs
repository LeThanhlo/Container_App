using Container_App.Model;

namespace Container_App.Repository.ProjectRepository
{
    public interface IProjectRepository
    {
        Task<Project> AddProjectAsync(Project project);
    }
}
