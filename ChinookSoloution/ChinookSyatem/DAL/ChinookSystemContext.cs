using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#region additional namespaces
using System.Data.Entity;
using ChinookSyatem.Entities;
#endregion
namespace ChinookSyatem.DAL
{
    internal class ChinookSystemContext: DbContext //inheritance ties DB context
    {
        public ChinookSystemContext() : base("name=ChinookDB")
        {

        }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Album> Albums { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<MediaType> MediaTypes { get; set; }
        public DbSet<Track> Tracks { get; set; }
    }
}
