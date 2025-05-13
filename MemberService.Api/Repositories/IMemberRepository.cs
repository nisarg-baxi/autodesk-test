using AutoDeskTest.MemberService.Api.Models;

namespace AutoDeskTest.MemberService.Api.Repositories
{
    public interface IMemberRepository
    {
        Task<IEnumerable<Member>> GetAllAsync();
        Task<Member?> GetByIdAsync(int id);
        Task UpdateAsync(Member member);
        Task CreateAsync(Member member);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}