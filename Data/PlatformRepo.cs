using System.Linq;
using PlatformService.Models;

namespace PlatformService.Data
{
    public class PlatformRepo: IPlatformRepo
    {
        private readonly AppDbContext _context;
        public PlatformRepo(AppDbContext context) => _context = context;
        public bool SaveChanges() => (_context.SaveChanges() >= 0);
        public IEnumerable<Platform> GetAllPlatforms() => _context.Platforms.ToList();
        public Platform GetPlatformById(int id) 
                => _context.Platforms.FirstOrDefault(x => x.Id == id)!;
        public void CreatePlatform(Platform platform) 
        {
            //if(platform is null) throw new ArgumentNullException(nameof(platform));
            //if(_context.Platforms.Contains(platform)) { }

            _context.Platforms.Add(platform);
        }
    }
}