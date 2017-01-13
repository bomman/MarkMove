namespace Markmove.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Tag
    {
        private ICollection<Destination> destinations;

        public Tag()
        {
            this.destinations = new HashSet<Destination>();    
        }

        [Key]
        public int Id { get; set; }

        [StringLength(20)]
        [Index(IsUnique = true)]
        public string Name { get; set; }

        public virtual ICollection<Destination> Destinations
        {
            get { return this.destinations; }
            set { this.destinations = value; }
        }
    }
}