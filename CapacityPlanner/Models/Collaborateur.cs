using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace CapacityPlanner.Models
{
    public partial class Collaborateur
    {
        public Collaborateur()
        {
            Affectations = new List<Affectation>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Nom { get; set; }
        [Required]
        [StringLength(255)]
        public string Prenom { get; set; }

        [InverseProperty(nameof(Affectation.Collaborateur))]
        [JsonIgnore]
        public virtual List<Affectation> Affectations { get; set; }
    }
}
