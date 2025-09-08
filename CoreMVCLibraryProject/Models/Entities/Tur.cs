using System.ComponentModel.DataAnnotations;

namespace CoreMVCLibraryProject.Models.Entities
{
    public class Tur
    {
        [Key]
        public int TurNo { get; set; }
        [MaxLength(50)]
        public string? TurAdi { get; set; }

        public ICollection<Kitap>? Kitaps { get; set; }

    }
}
