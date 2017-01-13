namespace Markmove.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Destination
    {
        public Destination()
        {
            this.Tags = new HashSet<Tag>();
        }

        public Destination(string name, string country, string description, int categoryId)
        {
            this.Name = name;
            this.Country = country;
            this.Description = description;
            this.CategoryId = categoryId;
            this.Tags = new HashSet<Tag>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        [StringLength(255)]
        public string Country { get; set; }

        public string Description { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }
    }
}