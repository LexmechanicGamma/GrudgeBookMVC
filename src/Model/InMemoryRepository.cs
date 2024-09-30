using GrudgeBookMvc.src.Model.Domain;
using GrudgeBookMvc.src.Model.Services;

namespace GrudgeBookMvc.src.Model
{
    public class InMemoryGrudgeRepo : IGrudgeRepository
    {
        Dictionary<string, Grudge> _damazKron;

        public InMemoryGrudgeRepo()
        {
            _damazKron = new Dictionary<string, Grudge>();
        }

        public void AddGrudge(Grudge transgression)
        {
            _damazKron?.Add(transgression.Id, transgression);
        }

        public void ChangeGrudgeStatus(string id, GrudgeStatus status)
        {
            _damazKron[id].Status = status;
        }

        public Grudge GetGrudge(string id)
        {
            return _damazKron![id];
        }
        public List<Grudge> ListGrudges()
        {
            List<Grudge> ListOfGrudges = new List<Grudge>();

            foreach (var grudge in _damazKron)
            {
                ListOfGrudges.Add(grudge.Value);
            }

            return ListOfGrudges;
        }
    }
}