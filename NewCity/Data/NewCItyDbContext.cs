using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NewCity.Models;

namespace NewCity.Data
{
    public class NewCityDbContext : IdentityDbContext
    {
        public NewCityDbContext(DbContextOptions<NewCityDbContext> options)
            : base(options)
        {
        }
        public DbSet<NewCity.Models.StorySeries> StorySeries { get; set; }
        public DbSet<NewCity.Models.StoryCard> StoryCard { get; set; }
        public DbSet<NewCity.Models.StoryOption> StoryOption { get; set; }
        public DbSet<NewCity.Models.StoryStatus> StoryStatus { get; set; }
        public DbSet<NewCity.Models.UserCharacter> UserCharacter { get; set; }
        public DbSet<NewCity.Models.UserSchedule> UserSchedule { get; set; }
        public DbSet<NewCity.Models.CreatorSchedule> CreatorSchedule { get; set; }
        public DbSet<NewCity.Models.Creator> Creator { get; set; }
        public DbSet<NewCity.Models.Reviewer> Reviewer { get; set; }
        public DbSet<NewCity.Models.HomeNews> HomeNews { get; set; }
        public DbSet<NewCity.Models.CharacterItem> CharacterItem { get; set; }
        public DbSet<NewCity.Models.CharacterLog> CharacterLog { get; set; }
        public DbSet<NewCity.Models.Item> Item { get; set; }
        public DbSet<NewCity.Models.ItemLog> ItemLog { get; set; }
        public DbSet<NewCity.Models.Location> Location { get; set; }
        public DbSet<NewCity.Models.CharacterStatus> CharacterStatus { get; set; }
        public DbSet<StoryCardRandom> StoryCardRandom { get; set; }

        
    }
}
