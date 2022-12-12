using Postgrest.Attributes;
using Postgrest.Models;

namespace NftHigherOrLowerGame.Model.DataBaseModels
{
    [Table("HighScore")]
    public class HighScore : BaseModel
    {
        [PrimaryKey("id")]
        public int Id { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("points")]
        public int Points { get; set; }

        [Column("correct")]
        public int Correct { get; set; }

        [Column("wrong")]
        public int Wrong { get; set; }

        [Column("total")]
        public int Total { get; set; }

        [Column("mode")]
        public string Mode { get; set; }

        public override bool Equals(object obj)
        {
            return obj is HighScore highscore && Id == highscore.Id;
        }

        public override int GetHashCode() { return HashCode.Combine(Id); }
    }
}

