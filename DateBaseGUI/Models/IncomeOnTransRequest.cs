using System;

namespace DateBaseGUI.Models
{
  public class IncomeOnTransRequest
  {
    public int id_income_on_trans_req { get; set; }
    public int id_trans_request { get; set; }
    public DateTime data { get; set; }
  }
}
