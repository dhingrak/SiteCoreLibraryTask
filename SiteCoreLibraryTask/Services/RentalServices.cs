using SiteCoreLibraryTask.Models;
using System.Data.SqlClient;

namespace SiteCoreLibraryTask.Services
{
    public class RentalServices: IRentalService
    {

        public string Constr { get; set; }
        public IConfiguration _configuration;
        public SqlConnection connection;

        public RentalServices(IConfiguration configuration)
        {
            _configuration = configuration;
            Constr = _configuration.GetConnectionString("DBConnection");
        }


       /* public List<Rental> GetUsers()
        {
            List<User> users = new List<User>();
            try
            {
                using (connection = new SqlConnection(Constr))
                {
                    connection.Open();
                    var cmd = new SqlCommand("SP_GetUserRecords", connection);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        User user = new User();
                        //user.UserId = Convert.ToInt32(rdr["UserId"]);
                        user.FirstName = rdr["FirstName"].ToString();
                        user.LastName = rdr["LastName"].ToString();
                        user.Email = rdr["Email"].ToString();

                        users.Add(user);
                    }
                }

                return users.ToList();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }*/

        public Boolean CreateRental(Rental rental)
        {
            try
            {
                using (connection = new SqlConnection(Constr))
                {
                    connection.Open();

                    var cmd = new SqlCommand("SP_CheckEmailExists", connection);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("UserEmailId", rental.UserEmailId);
                    SqlDataReader rdr = cmd.ExecuteReader();

                    if(rdr.Read())
                    {
                        cmd = new SqlCommand("SP_CreateRentalRecord", connection);
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("UserEmailId", rental.UserEmailId);
                        cmd.Parameters.AddWithValue("BookId", rental.BookId);
                        cmd.Parameters.AddWithValue("RentalDate", rental.RentalDate);
                        var result = cmd.ExecuteNonQuery();
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("Email does not exist");
                        return false;
                    }
                    
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }

    public interface IRentalService
    {
        public Boolean CreateRental(Rental rental);
    }
}
