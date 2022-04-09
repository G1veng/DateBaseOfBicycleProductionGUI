using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace DateBaseGUI.Data
{
  public class DBUtils
  {
    public static MySqlConnection GetDBConnection()
    {
      string host = "localhost";
      int port = 3306;
      string database = "bicycle";
      string username = "Giveng";
      string password = "Fybcbvjdf2002";
      return DBMySQLUtils.GetDBConnection(host, port, database, username, password);
    }
  }
}
