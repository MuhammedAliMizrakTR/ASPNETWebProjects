using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreMVCLibraryProject.Models.Entities
{
    public class Islem
    {
        [Key]
        public int IslemNo { get; set; }

        [ForeignKey("Ogrenci")]
        public int OgrNo { get; set; }
        public Ogrenci? Ogrenci { get; set; }

        [ForeignKey("Kitap")]
        public int KitapNo { get; set; }
        public Kitap? Kitap { get; set; }


        public DateTime ATarih { get; set; }
        public DateTime VTarih { get; set; }

    }
}
