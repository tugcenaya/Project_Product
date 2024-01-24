using Project.Core.UnitOfWorks;

namespace Project.Data.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ProjectDbContext _context;

        public UnitOfWork(ProjectDbContext context)
        {
            _context = context;
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}