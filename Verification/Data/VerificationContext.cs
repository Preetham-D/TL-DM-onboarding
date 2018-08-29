using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Verification.Model;

namespace Verification.Models
{
    public class VerificationContext : DbContext
    {
        public VerificationContext (DbContextOptions<VerificationContext> options)
            : base(options)
        {
        }

        public DbSet<Verification.Model.User> User { get; set; }
    }
}
