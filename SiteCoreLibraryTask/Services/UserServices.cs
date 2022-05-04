using SiteCoreLibraryTask.Models;
using System.Data.SqlClient;



namespace SiteCoreLibraryTask.Services
{
    public class UserServices: IUserService
    {

        public string Constr { get; set; }
        public IConfiguration _configuration;
        public SqlConnection connection;

        public UserServices(IConfiguration configuration)
        {
            _configuration = configuration;
            Constr = _configuration.GetConnectionString("DBConnection");
        }


        public List<User> GetUsers()
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
        }

        public void CreateUser(User user)
        {
            try
            {
                using (connection = new SqlConnection(Constr))
                {
                    connection.Open();
                    var cmd = new SqlCommand("SP_CreateUserRecord", connection);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("FirstName", user.FirstName);
                    cmd.Parameters.AddWithValue("LastName", user.LastName);
                    cmd.Parameters.AddWithValue("Email", user.Email);
                    cmd.Parameters.AddWithValue("Password", user.Password);
                    cmd.Parameters.AddWithValue("CreatedAt", user.CreatedAt);
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



    public interface IUserService
    {
        public List<User> GetUsers();
        public void CreateUser(User user);
    }
}
