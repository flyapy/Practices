using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practices.DbConnectionPool
{
    class Program
    {
        static void Main(string[] args)
        {
            //Initialize the Pool
            DbConnectionPool pool = DbConnectionPool.Instance;

            //Set the ConnectionString of the DatabaseConnectionPool
            //ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings["NorthwindConnectionString"];
            //DBConnectionSingletion.ConnectionString = settings.ConnectionString;
            //Borrow the SqlConnection object from the pool
            SqlConnection conn = pool.BorrowDBConnection();

            //Return the Connection to the pool after using it
            pool.ReturnDBConnection(conn);

            Console.ReadLine();
        }
    }
}
