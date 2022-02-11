using System;
using System.Linq;
using WebApi.Common;
using WebApi.DBOperations;

namespace WebApi.BookOperations{
    public class GetBookDetailQuery{
        public int GetById { get; set; }
        private readonly BookStoreDbContext _dbContext;

        public GetBookDetailQuery(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
            public BookDetailViewModel Handle(){
            
              var result =_dbContext.Books.Where(book=>book.Id ==GetById).SingleOrDefault();
              if(result is null) 
                throw new InvalidOperationException("Kitap zaten mevcut");
              var getBook =new BookDetailViewModel{
                  GenreId =((GenreEnum)result.GenreId).ToString(),
                  Title =result.Title,
                  PageCount = result.PageCount,
                  PublishDate = result.PublishDate.Date.ToString("dd/MMM/yyyy")
              };
              return getBook;
            }
        }

        public class BookDetailViewModel{
            public string Title { get; set; }
            public string  GenreId { get; set; }
            public int PageCount { get; set; }
            public string PublishDate { get; set; }
        }
    }
