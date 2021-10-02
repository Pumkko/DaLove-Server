using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaLove_Server.Data
{
    public class DaLoveDbContext : DbContext
    {
        public DbSet<UserMemory> Memories { get; set; }

        public DaLoveDbContext(DbContextOptions<DaLoveDbContext> option): base(option)
        {

        }
    }
}
