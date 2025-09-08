using System.ComponentModel.DataAnnotations;

namespace CoreMVCLibraryProject.Models.Entities
{
    public class Yazar
    {
        [Key]
        public int YazarNo { get; set; }
        [MaxLength(50)]
        public string YazarAd { get; set; }
        [MaxLength(50)]
        public string YazarSoyad { get; set; }

        public ICollection<Kitap>? Kitaps { get; set; }
    }
}
