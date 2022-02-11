using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApi.BookOperations;
using WebApi.BookOperations.GetBooks;
using WebApi.DBOperations;
namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController : ControllerBase
    {
     private readonly BookStoreDbContext _context;

     public BookController (BookStoreDbContext context)
     {
         _context =context;
     }

         [HttpGet]
        public IActionResult GetBooks(){
          GetBooksQuery query = new GetBooksQuery(_context);
          var result =query.Handle();
          return Ok(result);
        }

         [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            BookDetailViewModel result ;
            try
            {      
                GetBookDetailQuery getBookDetail =new GetBookDetailQuery(_context);
                getBookDetail.GetById =id;
                result = getBookDetail.Handle();
            }
            catch (Exception ex)
            {
                
                return BadRequest(ex.Message);
            }
           return Ok(result);
        }
       
       /*  [HttpGet]
        public Book GetById([FromQuery] string id)
        {
           var result =BookList.Where(book=>book.Id ==Convert.ToInt32(id)).SingleOrDefault();
           return result;
        }*/

         [HttpPost]
        public IActionResult AddBook([FromBody] CreateBookModel newBook)
        {   
    
         CreateBookCommand command = new CreateBookCommand(_context);

            try
            {
                command.Model =newBook;
                command.Handle();          
            }
            catch (Exception ex)
            {
                
               return BadRequest(ex.Message);
            }
          return Ok();
        }

         [HttpPut("{id}")]
        public IActionResult UpdateBook(int id,[FromBody] UpdateBookModel updatedBook)
        {
          try
          {    
                UpdateBookCommand command =new UpdateBookCommand(_context);
                command.UpdateBookId =id;
                command.Model =updatedBook;
                 command.Handle(); 
          }
          catch (Exception ex)
          {
              
              return BadRequest(ex.Message);
          }
        return Ok();


        }

        [HttpDelete]
        public IActionResult DeleteBook(int id){
        
        try
        {
            DeleteBook deleteBook = new DeleteBook(_context);
            deleteBook.DeleteId =id;
            deleteBook.Handle();
        }
        catch (Exception ex)
        {
            
            return BadRequest(ex.Message);
        }

        return Ok();
        }

    }
}