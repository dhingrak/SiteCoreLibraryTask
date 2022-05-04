using SiteCoreLibraryTask.Models;
using System.Data.SqlClient;

namespace SiteCoreLibraryTask.Services
{
    public class BookServices: IBookService
    {
        public string Constr { get; set; }
        public IConfiguration _configuration;
        public SqlConnection connection;

        public BookServices(IConfiguration configuration)
        {
            _configuration = configuration;
            Constr = _configuration.GetConnectionString("DBConnection");
        }


        public List<Book> GetBooks()
        {
            List<Book> books = new List<Book>();
            try
            {
                using (connection = new SqlConnection(Constr))
                {
                    connection.Open();
                    var cmd = new SqlCommand("SP_GetBookRecords", connection);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    SqlDataReader rdr = cmd.ExecuteReader();

                    while(rdr.Read())
                    {
                        Book book = new Book();
                        book.BookId = Convert.ToInt32(rdr["BookId"]);
                        book.Title = rdr["Title"].ToString();
                        book.FirstAuthorName = rdr["FirstAuthorName"].ToString();
                        book.SecondAuthorName = rdr["SecondAuthorName"].ToString();
                        book.Price = Convert.ToInt32(rdr["Price"]);
                        book.Quantity = Convert.ToInt32(rdr["Quantity"]);
                        
                        books.Add(book);
                    }
                }

                return books.ToList();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public void CreateBook(Book book)
        {
            try
            {
                using (connection = new SqlConnection(Constr))
                {
                    connection.Open();
                    var cmd = new SqlCommand("SP_CreateBookRecord", connection);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("Title", book.Title);
                    cmd.Parameters.AddWithValue("FirstAuthorName", book.FirstAuthorName);
                    cmd.Parameters.AddWithValue("SecondAuthorName", book.SecondAuthorName);
                    cmd.Parameters.AddWithValue("Price", book.Price);
                    cmd.Parameters.AddWithValue("CreatedAt", book.CreatedAt);
                    cmd.Parameters.AddWithValue("IsAvailable", book.IsAvailable);
                    cmd.Parameters.AddWithValue("Quantity", book.Quantity);
                    cmd.Parameters.AddWithValue("AvailableQuantity", book.Quantity);
                    cmd.ExecuteNonQuery();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }




    }

    public interface IBookService
    {
        public List<Book> GetBooks();
        public void CreateBook(Book book);
    }
}
