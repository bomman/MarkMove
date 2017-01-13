namespace Markmove.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class DestinationViewModel
    {
        public DestinationViewModel()
        {
        }

        public DestinationViewModel(string name, string description, string country, int categoryId)
        {
            this.Name = name;
            this.Description = description;
            this.Country = country;
            this.CategoryId = categoryId;
        }

        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public string Country { get; set; }

        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        public string Tags { get; set; }
    }
}