using System;

namespace DateBaseGUI.Models
{
  public class RequestForTransition
  {
    public int id_trans_req { get; set; }
    public int id_path { get; set; }
    public DateTime? trans_req_creation_time { get; set; }
    public DateTime? trans_req_start_time { get; set; }
    public DateTime? trans_req_end_time { get; set; }
  }
}
