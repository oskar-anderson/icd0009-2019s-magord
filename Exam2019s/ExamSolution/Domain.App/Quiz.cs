using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.App.Identity;
using ee.itcollege.magord.healthyfood.Domain.Base;

namespace Domain.App
{
    public class Quiz : DomainEntityIdMetadataUser<AppUser>
    {
        [MinLength(1)] [MaxLength(256)] public string Name { get; set; } = default!;

        [Column(TypeName = "decimal(18,4)")]
        public decimal? TotalPoints { get; set; }
        public ICollection<Question>? Questions { get; set; }
        public ICollection<Result>? Results { get; set; }
    }
}