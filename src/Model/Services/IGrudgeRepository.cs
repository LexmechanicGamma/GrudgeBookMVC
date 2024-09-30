using GrudgeBookMvc.src.Model.Domain;

namespace GrudgeBookMvc.src.Model.Services
{
        public interface IGrudgeRepository
        {
            void AddGrudge(Grudge transgression);
            void ChangeGrudgeStatus(string id, GrudgeStatus status);
            Grudge GetGrudge(string id);
            List<Grudge> ListGrudges();
        }
}