using AutoDeskTest.MemberService.Api.Models;
using AutoDeskTest.MemberService.Api.Repositories;

namespace AutoDeskTest.MemberService.Api.Services
{
    public class MemberServices : IMemberServices
    {
        private readonly IMemberRepository _repository;

        public MemberServices(IMemberRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Member>> GetAllAsync()
            => await _repository.GetAllAsync();

        public async Task<Member?> GetByIdAsync(int id)
            => await _repository.GetByIdAsync(id);

        public async Task<bool> UpdateAsync(int id, Member updatedMember)
        {
            if (!await _repository.ExistsAsync(id))
                return false;

            updatedMember.Id = id;
            await _repository.UpdateAsync(updatedMember);
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            if (!await _repository.ExistsAsync(id))
                return false;

            await _repository.DeleteAsync(id);
            return true;
        }

        public Task<Member> CreateAsync(Member ceatedMember)
        {
            if (ceatedMember == null)
                throw new ArgumentNullException(nameof(ceatedMember));

            _repository.CreateAsync(ceatedMember);
            return Task.FromResult(ceatedMember);
        }
    }
}