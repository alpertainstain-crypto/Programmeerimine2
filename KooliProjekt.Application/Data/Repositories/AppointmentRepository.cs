namespace KooliProjekt.Application.Data.Repositories
{
    public class AppointmentRepository : BaseRepository<Appointment>, IAppointmentRepository
    {
        public AppointmentRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
