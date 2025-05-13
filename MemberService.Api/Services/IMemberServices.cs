using AutoDeskTest.MemberService.Api.Models;

namespace AutoDeskTest.MemberService.Api.Services
{
    public interface IMemberServices
    {
        Task<IEnumerable<Member>> GetAllAsync();
        Task<Member?> GetByIdAsync(int id);
        Task<bool> UpdateAsync(int id, Member updatedMember);
        Task<Member> CreateAsync(Member updatedMember);
        Task<bool> DeleteAsync(int id);
    }
}