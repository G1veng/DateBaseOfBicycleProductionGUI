using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DateBaseGUI.Models
{
  public class OutcomeFromProduction
  {
    public int id_outcome_prod { get; set; }
    public int id_prod_req { get; set; }
    public int id_prod { get; set; }
    public int id_operations { get; set; }
    public int prod_count { get; set; }
    public DateTime data_outcome_prod { get; set; }
  }
}
