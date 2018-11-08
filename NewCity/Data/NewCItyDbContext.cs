using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NewCity.Models;

namespace NewCity.Data
{
    public class NewCItyDbContext : IdentityDbContext
    {
        public NewCItyDbContext(DbContextOptions<NewCItyDbContext> options)
            : base(options)
        {
        }
        public DbSet<NewCity.Models.StorySeries> StorySeries { get; set; }
        public DbSet<NewCity.Models.StoryCard> StoryCard { get; set; }
        public DbSet<NewCity.Models.StoryOption> StoryOption { get; set; }
        public DbSet<NewCity.Models.UserCharacter> UserCharacter { get; set; }
        public DbSet<NewCity.Models.CharacterSchedule> CharacterSchedule { get; set; }
    }
}
