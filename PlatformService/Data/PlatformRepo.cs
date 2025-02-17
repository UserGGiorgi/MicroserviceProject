using PlatformService.Data;
using PlatformService.Models;

namespace platformService.Data

{
   public class PlatformRepo : IPlatformRepo
    {
        private readonly AppDbContext _context;

        public PlatformRepo(AppDbContext context)
        {
            _context = context;
        }

        public void CreatePlatform(Platform plat)
        {
            ArgumentNullException.ThrowIfNull(plat);
            _context.Platforms.Add(plat);
        }

        public IEnumerable<Platform> GetAllPlatforms()
        {
            return _context.Platforms.ToList();
        }

        public Platform GetPlatformById(int id)
        {
            return _context.Platforms.FirstOrDefault(p => p.Id == id) ?? new Platform();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}