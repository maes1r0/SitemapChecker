using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using SitemapChecker.DAL.Contexts;
using SitemapChecker.DAL.Entities;

namespace SitemapChecker.DAL.Contexts
{
    class HistoryContext : DbContext
    {
        public HistoryContext(DbContextOptions<HistoryContext> options) : base(options)
        { }
        DbSet<Site> Sites { get; set; }

    }
}
