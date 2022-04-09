using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DateBaseGUI.Models
{
  public class OutcomeOnTransRequest
  {
    public int id_outcome_on_trans_req { get; set; }
    public int id_trans_request { get; set; }
    public DateTime data { get; set; }
  }
}
