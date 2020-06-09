using System;
using DAL.App.DTO.Identity;
using ee.itcollege.magord.healthyfood.Contracts.Domain.Base;

namespace DAL.App.DTO
{
    public class Result : IDomainEntityId
    {
        public Guid Id { get; set; }
        
        public int TimesPlayed { get; set; }

        public decimal? TotalScore { get; set; }

        public Guid QuizId { get; set; }
        public Quiz? Quiz { get; set; }
        public Guid AppUserId { get; set; } = default!;
        public AppUser? AppUser { get; set; }
    }
}