using GrudgeBookMvc.src.Model.Domain;
using GrudgeBookMvc.src.Model.Services;

namespace GrudgeBookMvc.src.Model.Postgres
{
    public class PostgresGrudgeRepository : IGrudgeRepository
    {
        private GrudgeContext _grudgeContext;

        public PostgresGrudgeRepository(GrudgeContext grudgeContext)
        {
            _grudgeContext = grudgeContext;
        }

        public void AddGrudge(Domain.Grudge grudge)
        {
            DBGrudgeAdapter adapter = new();

            var parsedGrudge = adapter.ToModel(grudge);
            _grudgeContext.grudges.Add(parsedGrudge);
            _grudgeContext.SaveChanges();
        }

        public void ChangeGrudgeStatus(string id, GrudgeStatus status)
        {
            _grudgeContext.grudges.Where(grudge => grudge.Id == id).FirstOrDefault()!.
                Status = Enum.GetName(status)!;

            _grudgeContext.SaveChanges();
        }
        public Domain.Grudge GetGrudge(string id)
        {

            Postgres.Grudge grudge = _grudgeContext.grudges.
                 Where(grudge => grudge.Id == id).FirstOrDefault()!;

            Domain.Grudge parsedGrudge = grudge.ToDomain();

            return parsedGrudge;
        }
        public List<Domain.Grudge> ListGrudges()
        {
            List<Domain.Grudge> grudges = new();
            var dblist = _grudgeContext.grudges.ToList();

            foreach (Postgres.Grudge grudge in dblist)
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