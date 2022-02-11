using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace WebApi.DBOperations{

public class DataGenerator
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new BookStoreDbContext(
        serviceProvider.GetRequiredService<DbContextOptions<BookStoreDbContext>>()))
        {
            // Look for any book.
            if (context.Books.Any())
            {
                return;   // Data was already seeded
            }

            context.Books.AddRange(
                 new Book{
               // Id =1,
                Title ="Lean StartUp",
                GenreId=1,
                PageCount=200,
                PublishDate =new DateTime(2001,06,12)
                },
                 new Book{
                //Id =2,
                Title ="Lean",
                GenreId=2,
                PageCount=200,
                PublishDate =new DateTime(2022,06,12)
                },
                 new Book{
               // Id =3,
                Title ="Startup",
                GenreId=2,
                PageCount=3450,
                PublishDate =new DateTime(2022,06,12)
                }
                   
        
             );

            context.SaveChanges();
        }
    }
}
}
