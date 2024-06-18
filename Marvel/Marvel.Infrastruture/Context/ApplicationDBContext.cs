using Microsoft.EntityFrameworkCore;

namespace Marvel.Infrastruture.Context
{
    public class ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : DbContext(options)
    {

    }
}
