using Container_App.Model;

namespace Container_App.Services.ProjectService
{
    public interface IProjectService
    {
        Task<Project> CreateProjectAsync(Project project, int userId);
        Task SendInviteAsync(ProjectUserInvite invite);
    }
}
