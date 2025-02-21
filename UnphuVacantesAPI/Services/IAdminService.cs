namespace UnphuVacantesAPI.Services
{
    public interface IAdminService
    {
        Task<object> GetDashboardStats();
        Task UpdateUserStatusAsync(int userId, string status);
        Task DeleteUserAsync(int userId);
    }
}
