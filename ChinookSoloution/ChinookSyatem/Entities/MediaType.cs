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
    [Table("MediaTypes")]
    internal class MediaType
    {
        private string _Name;

        [Key]
        public int MediaTypeId { get; set; }
        public string Name
        {
            //coded as fully implamented 
            get { return _Name; }
            set { _Name = string.IsNullOrEmpty(value) ? null : value; }
        }

        //Navigational property
        //child to parent - many tracks to THIS media
        public virtual ICollection<Track> Tracks { get; set; }

    }
}
