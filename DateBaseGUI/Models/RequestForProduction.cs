using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DateBaseGUI.Models
{
  public class RequestForProduction
  {
    public int id_prod_req { get; set; }
    public int id_reg_war { get; set; }
    public DateTime prod_req_create_time { get; set; }
    public int prod_count { get; set; }
    public int id_prod { get; set; }
    public int id_operations { get; set; }
  }
}
