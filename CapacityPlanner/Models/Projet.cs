using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace CapacityPlanner.Models
{
    public partial class Projet
    {
        public Projet()
        {
            Affectations = new List<Affectation>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Client { get; set; }
        [Required]
        [StringLength(255)]
        public string Nom { get; set; }
        [Required]
        [StringLength(255)]
        public string Statut { get; set; }
        [Required]
        [StringLength(255)]
        public string Type { get; set; }

        [InverseProperty(nameof(Affectation.Projet))]
        [JsonIgnore]
        public virtual List<Affectation> Affectations { get; set; }
    }
}
