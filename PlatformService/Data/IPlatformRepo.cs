using PlatformService.Models;

namespace PlatformService.Data
{
    public interface IPlatformRepo
    {
        void SaveChanges();
        IEnumerable<Platform> GetAllPlatforms();
        Platform GetPlatformById(int id);
        void CreatePlatform(Platform plat);
    }
}