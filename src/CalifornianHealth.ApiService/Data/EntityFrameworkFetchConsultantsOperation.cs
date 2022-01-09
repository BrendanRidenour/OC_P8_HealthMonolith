using Microsoft.EntityFrameworkCore;

namespace CalifornianHealth.Data
{
    public class EntityFrameworkFetchConsultantsOperation : IFetchConsultantsOperation
    {
        private readonly CHDBContext _db;

        public EntityFrameworkFetchConsultantsOperation(CHDBContext db)
        {
            this._db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public async Task<IReadOnlyList<Consultant>?> FetchConsultants() =>
            await this._db.Consultants.ToListAsync();
    }
}