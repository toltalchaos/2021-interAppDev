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
    [Table("Albums")]
    internal class Album
    {
        private string _ReleaseLabel;
        [Key]
        public int AlbumId { get; set; }

        [Required(ErrorMessage = "Album Title is a required field")]
        [StringLength(160, ErrorMessage ="album title is limited to 160 characters")]
        public string Title { get; set; }

        
        //FK annotation not needed if same name
        public int ArtistId { get; set; } //not int? which = not nullable

        public int ReleaseYear { get; set; }

        [StringLength(50, ErrorMessage = "_ReleaseLabel length is 50 charater maximum")]
        public string ReleaseLabel
        {
            //coded as fully implamented 
            get { return _ReleaseLabel; }
            set { _ReleaseLabel = string.IsNullOrEmpty(value) ? null : value; }
        }

        //[NOTMAPPED] ANNOTATIONS are allowed but do not exist in DB

        //navigational properties - not real data
        //object datatype 
        //many to one/child to parent relationship
        public virtual Artist Artist { get; set; }

        //Many tracks belong on one album
        public virtual ICollection<Track> Tracks { get; set; }
    }
}
