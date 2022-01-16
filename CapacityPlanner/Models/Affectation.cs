using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace CapacityPlanner.Models
{
    public partial class Affectation
    {
        [Key]
        public int Id { get; set; }
        public int CollaborateurId { get; set; }
        public int ProjetId { get; set; }
        public int Charge { get; set; }
        public DateTime DateDebut { get; set; }
        public DateTime DateFin { get; set; }

        [ForeignKey(nameof(CollaborateurId))]
        [InverseProperty("Affectations")]
        [JsonIgnore]
        public virtual Collaborateur Collaborateur { get; set; }
        [ForeignKey(nameof(ProjetId))]
        [InverseProperty("Affectations")]
        [JsonIgnore]
        public virtual Projet Projet { get; set; }

        public Affectation()
        {
            DateDebut = DateTime.Now;
            DateFin = DateTime.Now;
        }
    }
}
