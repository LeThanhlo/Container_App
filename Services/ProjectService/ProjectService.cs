using Container_App.Data;
using Container_App.Model;
using Container_App.Repository.ProjectRepository;
using Microsoft.EntityFrameworkCore;

namespace Container_App.Services.ProjectService
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly MyDbContext _context;

        public ProjectService(IProjectRepository projectRepository, MyDbContext context)
        {
            _projectRepository = projectRepository;
            _context = context;
        }
        public async Task<Project> CreateProjectAsync(Project project, int userId)
        {
            var userRole = await _context.Users
            .Where(u => u.UserId == userId)
            .Select(u => u.RoleGroupId)
            .FirstOrDefaultAsync();

            var isAdmin = await _context.RoleGroups
                .Where(rg => rg.RoleGroupId == userRole && rg.RoleGroupName == "Admin")
                .AnyAsync();

            if (!isAdmin)
            {
                throw new UnauthorizedAccessException("Chỉ có quản lý mới được phép tạo dự án.");
            }

            project.CreatedBy = userId;
            return await _projectRepository.AddProjectAsync(project);
        }

        public async Task SendInviteAsync(ProjectUserInvite invite)
        {
            _context.ProjectUserInvites.Add(invite);
            await _context.SaveChangesAsync();
        }
    }
}
