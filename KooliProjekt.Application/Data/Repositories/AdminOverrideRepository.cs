namespace KooliProjekt.Application.Data.Repositories
{
    public class AdminOverrideRepository : BaseRepository<AdminOverride>, IAdminOverrideRepository
    {
        public AdminOverrideRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
