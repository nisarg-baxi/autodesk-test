using Microsoft.EntityFrameworkCore;
using AutoDeskTest.MemberService.Api.Data;
using AutoDeskTest.MemberService.Api.Models;

namespace AutoDeskTest.MemberService.Api.Repositories
{
    public class MemberRepository : IMemberRepository
    {
        private readonly MemberDbContext _context;

        public MemberRepository(MemberDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Member>> GetAllAsync()
            => await _context.Members.ToListAsync();

        public async Task<Member?> GetByIdAsync(int id)
            => await _context.Members.FindAsync(id);

        public async Task UpdateAsync(Member member)
        {
            _context.Members.Update(member);
            await _context.SaveChangesAsync();
        }

        public async Task CreateAsync(Member member)
        {
            await _context.Members.AddAsync(member);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var member = await _context.Members.FindAsync(id);
            if (member != null)
            {
                _context.Members.Remove(member);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(int id)
            => await _context.Members.AnyAsync(m => m.Id == id);
    }
}