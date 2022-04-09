using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DateBaseGUI.Models
{
  public class UnfinishedProduction
  {
    public int id_prod { get; set; }
    public int id_prod_req { get; set; }
    public int unfin_prod_count { get; set; }
  }
}
