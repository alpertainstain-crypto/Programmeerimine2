namespace KooliProjekt.Application.Data.Repositories
{
    public class AvailabilityRepository : BaseRepository<Availability>, IAvailabilityRepository
    {
        public AvailabilityRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
