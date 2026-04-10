namespace KooliProjekt.Application.Data.Repositories
{
    public class InvoiceLineRepository : BaseRepository<InvoiceLine>, IInvoiceLineRepository
    {
        public InvoiceLineRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
