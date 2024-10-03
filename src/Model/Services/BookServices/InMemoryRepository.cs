using GrudgeBookMvc.src.Model.Domain.Book;
using GrudgeBookMvc.src.Model.Postgres.Migration;

namespace GrudgeBookMvc.src.Model.Services.BookServices
{
    public class InMemoryGrudgeRepo : IGrudgeRepository
    {
        Dictionary<string, Domain.Book.Grudge> _damazKron;

        public InMemoryGrudgeRepo()
        {
            _damazKron = new Dictionary<string, Domain.Book.Grudge>();
        }

        public void AddGrudge(Domain.Book.Grudge transgression)
        {
            _damazKron?.Add(transgression.Id, transgression);
        }

        public void ChangeGrudgeStatus(string id, GrudgeStatus status)
        {
            _damazKron[id].Status = status;
        }

        public Domain.Book.Grudge GetGrudge(string id)
        {
            return _damazKron![id];
        }
        public List<Domain.Book.Grudge> ListGrudges()
        {
            List<Domain.Book.Grudge> ListOfGrudges = new List<Domain.Book.Grudge>();

            foreach (var grudge in _damazKron)
            {
                ListOfGrudges.Add(grudge.Value);
            }

            return ListOfGrudges;
        }
    }
}