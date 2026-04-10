namespace KooliProjekt.Application.Data.Repositories
{
    public class VisitDocumentRepository : BaseRepository<VisitDocument>, IVisitDocumentRepository
    {
        public VisitDocumentRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
