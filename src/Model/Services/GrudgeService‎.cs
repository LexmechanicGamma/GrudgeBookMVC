using GrudgeBookMvc.src.Model.Domain;

namespace GrudgeBookMvc.src.Model.Services
{
    public class GrudgeService‎
    {
            private readonly IGrudgeRepository? _grudgeRepo;
            public GrudgeService(IGrudgeRepository grudgeRepo)
            {
                _grudgeRepo = grudgeRepo;
            }

            public void WriteGrudge(Grudge transgression)
            {
                _grudgeRepo!.AddGrudge(transgression);
            }

            public void AdjustGrudgeStatus(string id, GrudgeStatus status)
            {
                try
                {
                    _grudgeRepo!.ChangeGrudgeStatus(id, status);
                }
                catch (KeyNotFoundException e)
                {
                    throw new IdIsNotFoundException(e.Message + "Such ID does not exist.");
                }
                catch (ArgumentNullException e)
                {
                    throw new IdIsNotFoundException(e.Message + "Incorrect ID input.");
                }
            }

            public Grudge GetGrudge(string id)
            {
                try
                {
                    return _grudgeRepo!.GetGrudge(id)!;
                }
                catch (KeyNotFoundException e)
                {
                    throw new IdIsNotFoundException(e.Message + "Such ID does not exist.");
                }
                catch (ArgumentNullException e)
                {
                    throw new IdIsNotFoundException(e.Message + "Incorrect ID input.");
                }
            }

            public List<Grudge> ListGrudges()
            {
                return _grudgeRepo!.ListGrudges();
            }

            public void DeleteGrudge(string id)
            {
                throw new GrudgeDeleteException("SKRUFF!");
            }
    }
}