using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Markmove.Models
{
    public class MarkmoveDbContext : IdentityDbContext<ApplicationUser>
    {
        public MarkmoveDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public virtual IDbSet<Destination> Destinations { get; set; }

        public virtual IDbSet<Category> Categories { get; set; }

        public virtual IDbSet<Tag> Tags { get; set; }

        public static MarkmoveDbContext Create()
        {
            return new MarkmoveDbContext();
        }
    }
}