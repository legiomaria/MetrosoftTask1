using MetrosoftSearch.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace MetrosoftSearch.Api.EF;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<SearchResult> SearchResults { get; set; }
}
