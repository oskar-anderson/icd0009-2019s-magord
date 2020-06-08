using System;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.App.Identity;
using ee.itcollege.magord.healthyfood.Domain.Base;

namespace Domain.App
{
    public class Result : DomainEntityIdMetadataUser<AppUser>
    {
        public int TimesPlayed { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal TotalScore { get; set; }

        public Guid QuizId { get; set; }
        public Quiz? Quiz { get; set; }

    }
}