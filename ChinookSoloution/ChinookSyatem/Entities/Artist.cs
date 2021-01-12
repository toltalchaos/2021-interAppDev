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
    [Table("Artists")]

    //internal class to make restricted access within library
    internal class Artist
    {
        private string _Name;


        [Key]
        public int ArtistID { get; set; }

        [StringLength(120, ErrorMessage = "error limit of 120 characters for artist name.")]
        public string Name { 
            //coded as fully implamented 
            get { return _Name; }
            set { _Name = string.IsNullOrEmpty(value) ? null : value;}
        }


    }
}
