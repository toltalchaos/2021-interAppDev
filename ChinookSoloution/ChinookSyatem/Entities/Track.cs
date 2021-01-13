using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#region additional-libraries
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
#endregion
namespace ChinookSystem.Entities
{
    [Table("Tracks")]
    internal class Track
    {
        private string _Composer;


        [Key]
        public int TrackId { get; set; }

        [Required(ErrorMessage ="Name is required")]
        [StringLength(200, ErrorMessage ="Name is limited to a max of 200 chaters")]
        public string Name { get; set; }
        public int? AlbumId { get; set; }
        public int MediaTypeId { get; set; }
        public int? GenreId { get; set; }

        [StringLength(220, ErrorMessage ="Composer has a Maximum of 220 characters")]
        public string Composer
        {
            //coded as fully implamented 
            get { return _Composer; }
            set { _Composer = string.IsNullOrEmpty(value) ? null : value; }
        }
        public int Miliseconds { get; set; }
        public int? Bytes { get; set; }
        public double UnitPrice { get; set; }

        //Navigational Properties
        //one property to THIS track
        //album
        public virtual Album Album { get; set; }
        //mediatype
        public virtual MediaType MediaType { get; set; }
        //genre
        public virtual Genre Genre { get; set; }
    }
}
