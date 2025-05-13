using Microsoft.EntityFrameworkCore;
using AutoDeskTest.MemberService.Api.Models;

namespace AutoDeskTest.MemberService.Api.Data
{
    public class MemberDbContext : DbContext
    {
        public MemberDbContext(DbContextOptions<MemberDbContext> options)
            : base(options) { }

        public DbSet<Member> Members => Set<Member>();
    }
}