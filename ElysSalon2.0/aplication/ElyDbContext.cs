using ElysSalon2._0.domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ElysSalon2._0.aplication;

public class ElyDbContext: DbContext
{

    public ElyDbContext(DbContextOptions<ElyDbContext> options): base(options)
    {

    }

    public DbSet<Article> Articles { get; set; }
    public DbSet<ArticleType>  ArticleTypes { get; set; }
    public DbSet<TicketDetails> TicketDetails { get; set; }
    public DbSet<Ticket> Tickets { get; set; }
    
}