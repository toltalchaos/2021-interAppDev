using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#region additional-libraries
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
#endregion
namespace ChinookSyatem.Entities
{
    [Table("Genres")]
    internal class Genre
    {
        private string _Name;

        [Key]
        public int GenreId { get; set; }

        [StringLength(120, ErrorMessage ="Maximum charactercount of 120 on genre name")]
        public string Name
        {

            //coded as fully implamented 
            get { return _Name; }
            set { _Name = string.IsNullOrEmpty(value) ? null : value; }
        }


        //nav properties
        //many tracks to THIS genre
        public virtual ICollection<Track> Tracks { get; set; }
    }
}
