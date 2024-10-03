using GrudgeBookMvc.src.Model.Domain.Book;
using GrudgeBookMvc.src.Model.Postgres.Migration;

namespace GrudgeBookMvc.src.Model.Services.BookServices
{
    public interface IGrudgeRepository
    {
        void AddGrudge(Domain.Book.Grudge transgression);
        void ChangeGrudgeStatus(string id, GrudgeStatus status);
        Domain.Book.Grudge GetGrudge(string id);
        List<Domain.Book.Grudge> ListGrudges();
    }
}