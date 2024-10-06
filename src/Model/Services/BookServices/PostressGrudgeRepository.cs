using GrudgeBookMvc.src.Model.Domain.Book;
using GrudgeBookMvc.src.Model.Postgres.Book;
using GrudgeBookMvc.src.Model.Postgres.Context;

namespace GrudgeBookMvc.src.Model.Services.BookServices
{
    public class PostgresGrudgeRepository : IGrudgeRepository
    {
        private DamazKronContext _grudgeContext;

        public PostgresGrudgeRepository(DamazKronContext grudgeContext)
        {
            _grudgeContext = grudgeContext;
        }

        public void AddGrudge(Domain.Book.Grudge grudge)
        {
            DBGrudgeAdapter adapter = new();

            _grudgeContext.Grudges.Add(adapter.ToModel(grudge));
            _grudgeContext.SaveChanges();
        }

        public void ChangeGrudgeStatus(string id, GrudgeStatus status)
        {
            _grudgeContext.Grudges.Where(grudge => grudge.Id == id).FirstOrDefault()!.
                Status = Enum.GetName(status)!;

            _grudgeContext.SaveChanges();
        }
        public Domain.Book.Grudge GetGrudge(string id)
        {

            Postgres.Book.Grudge grudge = _grudgeContext.Grudges.
                 Where(grudge => grudge.Id == id).FirstOrDefault()!;

            if(grudge == null) throw new IdIsNotFoundException("Incorrect ID input.");

            Domain.Book.Grudge parsedGrudge = grudge.ToDomain();
            return parsedGrudge;

        }
        public List<Domain.Book.Grudge> ListGrudges()
        {
            List<Domain.Book.Grudge> grudges = new();
            var dblist = _grudgeContext.Grudges.ToList();

            foreach (Postgres.Book.Grudge grudge in dblist)
            {
                grudges.Add(grudge.ToDomain());
            }

            return grudges;
        }

        ~PostgresGrudgeRepository()
        {
            if (_grudgeContext != null) _grudgeContext.Dispose();
        }
    }
}