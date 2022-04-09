using System.Windows;
using MySql.Data.MySqlClient;
using System.Data;
using System.Data.Common;
using DateBaseGUI.Models;
using System.Collections.ObjectModel;
using System;
using System.Collections.Generic;

namespace DateBaseGUI.Data
{
  public class DBInteraction
  {
    MySqlConnection conn;
    private MessageBoxResult Alarm(string message, string caption, MessageBoxButton button, MessageBoxImage icon) =>
      System.Windows.MessageBox.Show(message, caption, button, icon);
    public DBInteraction()
    {
      conn = DBUtils.GetDBConnection();
    }
    public void Income(int CountOfRepeats)
    {
      conn.Open();
      try
      {
        MySqlCommand cmd = new MySqlCommand("income", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@CountOfRepeats", MySqlDbType.Int32).Value = CountOfRepeats;
        cmd.ExecuteNonQuery();
      }
      catch
      {
        Alarm("Error in creating storage procedure", "Word Processor", MessageBoxButton.OK, MessageBoxImage.Information);
      }
      conn.Close();
    }
    public List<int> CreateRequestForTransitionForOneProduct(int ReqForTrans, int StartWarId, int TransWarId, int EndWarId, int prodId, int innerCount, int position)
    {
      int id_trans_war = -1;
      List<int> toReturn = null;
      conn.Open();
      try
      {
        string sql = "select id_trans_war from `transit warehouse` where `Limit` = (select count(id_trans_war) from outcome_on_trans_req " +
          "inner join `request for transition` on outcome_on_trans_req.id_trans_req = `request for transition`.id_trans_req " +
          "inner join path on `request for transition`.id_path = path.id_path " +
          "where id_trans_war = @IdTransWar) and id_trans_war = @IdTransWar";
        MySqlCommand cmd1 = new MySqlCommand();
        cmd1.Connection = conn;
        cmd1.CommandText = sql;
        cmd1.Parameters.Add("@IdTransWar", MySqlDbType.Int32).Value = TransWarId;
        using (DbDataReader reader = cmd1.ExecuteReader())
        {
          if (reader.HasRows)
          {
            while (reader.Read())
            {
              id_trans_war = reader.GetInt32(0);
            }
          }
        }
        if (id_trans_war != -1)
        {
          string sql1 = "select id_trans_war from path where id_reg_war_start = @IdRegWar and id_reg_war_end = @IdRegWarEnd and id_trans_war != @TransWarId";
          MySqlCommand cmd2 = new MySqlCommand();
          cmd2.Connection = conn;
          cmd2.CommandText = sql1;
          cmd2.Parameters.Add("@IdRegWar", MySqlDbType.Int32).Value = StartWarId;
          cmd2.Parameters.Add("@IdRegWarEnd", MySqlDbType.Int32).Value = EndWarId;
          cmd2.Parameters.Add("@TransWarId", MySqlDbType.Int32).Value = TransWarId;
          toReturn = new List<int>();
          using (DbDataReader reader = cmd2.ExecuteReader())
          {
            if (reader.HasRows)
            {
              while (reader.Read())
              {
                toReturn.Add(reader.GetInt32(0));
              }
            }
          }
          conn.Close();
          return toReturn;
        }
        else
        {
          MySqlCommand cmd = new MySqlCommand("createRequestForTransitionForOneProduct", conn);
          cmd.CommandType = CommandType.StoredProcedure;
          cmd.Parameters.Add("@ReqForTrans", MySqlDbType.Int32).Value = ReqForTrans;
          cmd.Parameters.Add("@StartWarId", MySqlDbType.Int32).Value = StartWarId;
          cmd.Parameters.Add("@TransWarId", MySqlDbType.Int32).Value = TransWarId;
          cmd.Parameters.Add("@EndWarId", MySqlDbType.Int32).Value = EndWarId;
          cmd.Parameters.Add("@prodId", MySqlDbType.Int32).Value = prodId;
          cmd.Parameters.Add("@innerCount", MySqlDbType.Int32).Value = innerCount;
          cmd.Parameters.Add("@position", MySqlDbType.Int32).Value = position;
          cmd.ExecuteNonQuery();
        }
      }
      catch
      {
        Alarm("Error in creating storage procedure", "Word Processor", MessageBoxButton.OK, MessageBoxImage.Information);
      }
      conn.Close();
      return toReturn;
    }
    public void WriteOffForTransition(int idTransReq)
    {
      conn.Open();
      try
      {
        MySqlCommand cmd = new MySqlCommand("WriteOffForTransition", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@idTransReq", MySqlDbType.Int32).Value = idTransReq;
        cmd.ExecuteNonQuery();
      }
      catch
      {
        Alarm("Error in creating storage procedure", "Word Processor", MessageBoxButton.OK, MessageBoxImage.Information);
      }
      conn.Close();
    }
    public void RecieveTransRequest(int idTransReq)
    {
      conn.Open();
      try
      {
        MySqlCommand cmd = new MySqlCommand("RecieveTransRequest", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@idTransReq", MySqlDbType.Int32).Value = idTransReq;
        cmd.ExecuteNonQuery();
      }
      catch
      {
        Alarm("Error in creating storage procedure", "Word Processor", MessageBoxButton.OK, MessageBoxImage.Information);
      }
      conn.Close();
    }
    /*public void CreateRequestForProducton(int OperationId, int WarWhereProductId, int ProductId, int CountOfProduct)
    {
      conn.Open();
      try
      {
        MySqlCommand cmd = new MySqlCommand("CreateRequestForProducton", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@OperationId", MySqlDbType.Int32).Value = OperationId;
        cmd.Parameters.Add("@WarWhereProductId", MySqlDbType.Int32).Value = WarWhereProductId;
        cmd.Parameters.Add("@ProductId", MySqlDbType.Int32).Value = ProductId;
        cmd.Parameters.Add("@CountOfProduct", MySqlDbType.Int32).Value = CountOfProduct;
        cmd.ExecuteNonQuery();
      }
      catch
      {
        Alarm("Error in creating storage procedure", "Word Processor", MessageBoxButton.OK, MessageBoxImage.Information);
      }
      conn.Close();
    }*/
    public void OutcomeFromProduction(int ProductionRequestId, int CountOfProducedProduct, int ProducedProduct)
    {
      conn.Open();
      try
      {
        MySqlCommand cmd = new MySqlCommand("OutcomeFromProduction", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@ProductionRequestId", MySqlDbType.Int32).Value = ProductionRequestId;
        cmd.Parameters.Add("@CountOfProducedProduct", MySqlDbType.Int32).Value = CountOfProducedProduct;
        cmd.Parameters.Add("@ProducedProduct", MySqlDbType.Int32).Value = ProducedProduct;
        cmd.ExecuteNonQuery();
      }
      catch
      {
        Alarm("Error in creating storage procedure", "Word Processor", MessageBoxButton.OK, MessageBoxImage.Information);
      }
      conn.Close();
    }
    public void RealizationProcedure(int PoductId, int VendorId, int ProductCount, int RegularWarehouseId)
    {
      conn.Open();
      try
      {
        MySqlCommand cmd = new MySqlCommand("RealizationProcedure", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@PoductId", MySqlDbType.Int32).Value = PoductId;
        cmd.Parameters.Add("@VendorId", MySqlDbType.Int32).Value = VendorId;
        cmd.Parameters.Add("@ProductCount", MySqlDbType.Int32).Value = ProductCount;
        cmd.Parameters.Add("@RegularWarehouseId", MySqlDbType.Int32).Value = RegularWarehouseId;
        cmd.ExecuteNonQuery();
      }
      catch
      {
        Alarm("Error in creating storage procedure", "Word Processor", MessageBoxButton.OK, MessageBoxImage.Information);
      }
      conn.Close();
    }
    public void SetPaths(int id_start_reg_war, int id_end_reg_war, int id_war_trans)
    {
      conn.Open();
      try
      {
        MySqlCommand cmd = new MySqlCommand("setPaths", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@id_start_reg_war", MySqlDbType.Int32).Value = id_start_reg_war;
        cmd.Parameters.Add("@id_end_reg_war", MySqlDbType.Int32).Value = id_end_reg_war;
        cmd.Parameters.Add("@id_war_trans", MySqlDbType.Int32).Value = id_war_trans;
        cmd.ExecuteNonQuery();
      }
      catch
      {
        Alarm("Error in creating storage procedure", "Word Processor", MessageBoxButton.OK, MessageBoxImage.Information);
      }
      conn.Close();
    }
    public int MaxIndexTransReq()
    {
      int result = -1;
      conn.Open();
      try
      {
        string sql = "select MAX(id_trans_req) from `request for transition`";
        MySqlCommand cmd = new MySqlCommand();
        cmd.Connection = conn;
        cmd.CommandText = sql;
        using (DbDataReader reader = cmd.ExecuteReader())
        {
          if (reader.HasRows)
          {
            while (reader.Read())
            {
              result = reader.GetInt32(0);
            }
          }
        }
      }
      catch
      {
        Alarm("Error in creating storage procedure", "Word Processor", MessageBoxButton.OK, MessageBoxImage.Information);
      }
      conn.Close();
      return result;
    }
    public ObservableCollection<Paths> GetPaths()
    {
      conn.Open();
      ObservableCollection<Paths> paths = new ObservableCollection<Paths>();
      string sql = "select  id_path, id_reg_war_start, id_trans_war, id_reg_war_end from path";
      MySqlCommand cmd = new MySqlCommand();
      cmd.Connection = conn;
      cmd.CommandText = sql;
      using (DbDataReader reader = cmd.ExecuteReader())
      {
        if (reader.HasRows)
        {
          while (reader.Read())
          {
            var id_path = reader.GetInt32(0);
            var id_reg_war_start = reader.GetInt32(1);
            var id_trans_war = reader.GetInt32(2);
            var id_reg_war_end = reader.GetInt32(3);
            paths.Add(new Paths() { endWarId = id_reg_war_end, pathId = id_path, startWarId = id_reg_war_start, transWarId = id_trans_war });
          }
        }
      }
      conn.Close();
      return paths;
    }
    public ObservableCollection<RegularQuantity> GetRegularQuantity()
    {
      conn.Open();
      ObservableCollection<RegularQuantity> regularQuantity = new ObservableCollection<RegularQuantity>();
      string sql = "select id_prod, id_reg_war, count from `regular quantity`";
      MySqlCommand cmd = new MySqlCommand();
      cmd.Connection = conn;
      cmd.CommandText = sql;
      using (DbDataReader reader = cmd.ExecuteReader())
      {
        if (reader.HasRows)
        {
          while (reader.Read())
          {
            var id_prod = reader.GetInt32(0);
            var id_reg_start = reader.GetInt32(1);
            var count = reader.GetInt32(2);
            regularQuantity.Add(new RegularQuantity() { productId = id_prod, quantity = count, warehouseId = id_reg_start });
          }
        }
      }
      conn.Close();
      return regularQuantity;
    }
    public ObservableCollection<RequestForTransition> GetRequestsForTransition()
    {
      conn.Open();
      ObservableCollection<RequestForTransition> regularQuantity = new ObservableCollection<RequestForTransition>();
      string sql = "select id_trans_req, id_path, trans_req_creation_time, trans_req_start_time, trans_req_end_time from `request for transition`";
      MySqlCommand cmd = new MySqlCommand();
      cmd.Connection = conn;
      cmd.CommandText = sql;
      using (DbDataReader reader = cmd.ExecuteReader())
      {
        if (reader.HasRows)
        {
          while (reader.Read())
          {
            var id_trans_req = reader.GetInt32(0);
            var id_path = reader.GetInt32(1);
            var trans_req_creation_time = reader.GetDateTime(2);
            DateTime? trans_req_start_time;
            try { trans_req_start_time = reader.GetDateTime(3); }
            catch { trans_req_start_time = null; }
            DateTime? trans_req_end_time;
            try { trans_req_end_time = reader.GetDateTime(4); }
            catch { trans_req_end_time = null; }
            regularQuantity.Add(new RequestForTransition()
            {
              id_path = id_path,
              id_trans_req = id_trans_req,
              trans_req_creation_time = trans_req_creation_time,
              trans_req_end_time = trans_req_end_time,
              trans_req_start_time = trans_req_start_time
            });
          }
        }
      }
      conn.Close();
      return regularQuantity;
    }
    public ObservableCollection<TransitOrderComposition> GetTransitOrderComposition()
    {
      conn.Open();
      ObservableCollection<TransitOrderComposition> regularQuantity = new ObservableCollection<TransitOrderComposition>();
      string sql = "select id_trans_req, id_position, id_prod, prod_count from `transit order composition`";
      MySqlCommand cmd = new MySqlCommand();
      cmd.Connection = conn;
      cmd.CommandText = sql;
      using (DbDataReader reader = cmd.ExecuteReader())
      {
        if (reader.HasRows)
        {
          while (reader.Read())
          {
            var id_trans_req = reader.GetInt32(0);
            var id_position = reader.GetInt32(1);
            var id_prod = reader.GetInt32(2);
            var prod_count = reader.GetInt32(3);
            regularQuantity.Add(new TransitOrderComposition()
            {
              id_position = id_position,
              id_prod = id_prod,
              prod_count = prod_count,
              id_trans_req = id_trans_req,
            });
          }
        }
      }
      conn.Close();
      return regularQuantity;
    }
    public ObservableCollection<TransitQuantity> GetTransitQuantity()
    {
      conn.Open();
      ObservableCollection<TransitQuantity> regularQuantity = new ObservableCollection<TransitQuantity>();
      string sql = "select id_prod, id_trans_war, count from `transit quantity`";
      MySqlCommand cmd = new MySqlCommand();
      cmd.Connection = conn;
      cmd.CommandText = sql;
      using (DbDataReader reader = cmd.ExecuteReader())
      {
        if (reader.HasRows)
        {
          while (reader.Read())
          {
            var id_prod = reader.GetInt32(0);
            var id_trans_war = reader.GetInt32(1);
            var count = reader.GetInt32(2);
            regularQuantity.Add(new TransitQuantity()
            {
              count = count,
              id_prod = id_prod,
              id_trans_war = id_trans_war,
            });
          }
        }
      }
      conn.Close();
      return regularQuantity;
    }
    public ObservableCollection<IncomeOnTransRequest> GetIncomeOnTransRequest()
    {
      conn.Open();
      ObservableCollection<IncomeOnTransRequest> regularQuantity = new ObservableCollection<IncomeOnTransRequest>();
      string sql = "select id_income_on_trans_req, id_trans_req, data from income_on_trans_req";
      MySqlCommand cmd = new MySqlCommand();
      cmd.Connection = conn;
      cmd.CommandText = sql;
      using (DbDataReader reader = cmd.ExecuteReader())
      {
        if (reader.HasRows)
        {
          while (reader.Read())
          {
            var id_income_on_trans_req = reader.GetInt32(0);
            var id_trans_req = reader.GetInt32(1);
            var data = reader.GetDateTime(2);
            regularQuantity.Add(new IncomeOnTransRequest()
            {
              data = data,
              id_income_on_trans_req = id_income_on_trans_req,
              id_trans_request = id_trans_req,
            });
          }
        }
      }
      conn.Close();
      return regularQuantity;
    }
    public ObservableCollection<OutcomeOnTransRequest> GetOutcomeOnTransRequest()
    {
      conn.Open();
      ObservableCollection<OutcomeOnTransRequest> regularQuantity = new ObservableCollection<OutcomeOnTransRequest>();
      string sql = "select id_outcome_trans_req, id_trans_req, data from outcome_on_trans_req";
      MySqlCommand cmd = new MySqlCommand();
      cmd.Connection = conn;
      cmd.CommandText = sql;
      using (DbDataReader reader = cmd.ExecuteReader())
      {
        if (reader.HasRows)
        {
          while (reader.Read())
          {
            var id_outcome_on_trans_req = reader.GetInt32(0);
            var id_trans_req = reader.GetInt32(1);
            var data = reader.GetDateTime(2);
            regularQuantity.Add(new OutcomeOnTransRequest()
            {
              id_trans_request = id_trans_req,
              data = data,
              id_outcome_on_trans_req = id_outcome_on_trans_req,
            });
          }
        }
      }
      conn.Close();
      return regularQuantity;
    }
    public ObservableCollection<Operations> GetOperations()
    {
      conn.Open();
      ObservableCollection<Operations> regularQuantity = new ObservableCollection<Operations>();
      string sql = "select * from operations";
      MySqlCommand cmd = new MySqlCommand();
      cmd.Connection = conn;
      cmd.CommandText = sql;
      using (DbDataReader reader = cmd.ExecuteReader())
      {
        if (reader.HasRows)
        {
          while (reader.Read())
          {
            var id_operations = reader.GetInt32(0);
            var name = reader.GetString(1);
            regularQuantity.Add(new Operations()
            {
              name = name,
              id_operations = id_operations,
            });
          }
        }
      }
      conn.Close();
      return regularQuantity;
    }
    public void AddOperation(string name)
    {
      conn.Open();
      try
      {
        MySqlCommand cmd = new MySqlCommand("AddOperation", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@nameOfOperation", MySqlDbType.VarChar).Value = name;
        cmd.ExecuteNonQuery();
      }
      catch
      {
        Alarm("Error in creating storage procedure", "Word Processor", MessageBoxButton.OK, MessageBoxImage.Information);
      }
      conn.Close();
    }
    public ObservableCollection<UnfinishedProduction> GetUnfinishedProduction()
    {
      conn.Open();
      ObservableCollection<UnfinishedProduction> paths = new ObservableCollection<UnfinishedProduction>();
      string sql = "select id_prod, id_prod_req, unfin_prod_count from `unfinished production`";
      MySqlCommand cmd = new MySqlCommand();
      cmd.Connection = conn;
      cmd.CommandText = sql;
      using (DbDataReader reader = cmd.ExecuteReader())
      {
        if (reader.HasRows)
        {
          while (reader.Read())
          {
            var id_prod = reader.GetInt32(0);
            var id_prod_req = reader.GetInt32(1);
            var unfin_prod_count = reader.GetInt32(2);
            paths.Add(new UnfinishedProduction()
            {
              id_prod = id_prod,
              id_prod_req = id_prod_req,
              unfin_prod_count = unfin_prod_count,
            });
          }
        }
      }
      conn.Close();
      return paths;
    }
    public void CreateRequestForProduction(int OperationId, int WarWhereProductId, int ProductId, int CountOfProduct)
    {
      conn.Open();
      try
      {
        MySqlCommand cmd = new MySqlCommand("CreateRequestForProducton", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@OperationId", MySqlDbType.Int32).Value = OperationId;
        cmd.Parameters.Add("@WarWhereProductId", MySqlDbType.Int32).Value = WarWhereProductId;
        cmd.Parameters.Add("@ProductId", MySqlDbType.Int32).Value = ProductId;
        cmd.Parameters.Add("@CountOfProduct", MySqlDbType.Int32).Value = CountOfProduct;
        cmd.ExecuteNonQuery();
      }
      catch
      {
        Alarm("Error in creating storage procedure", "Word Processor", MessageBoxButton.OK, MessageBoxImage.Information);
      }
      conn.Close();
    }
    public ObservableCollection<RequestForProduction> GetRequsetsForProduction()
    {
      conn.Open();
      ObservableCollection<RequestForProduction> paths = new ObservableCollection<RequestForProduction>();
      string sql = "select `request for production`.id_prod_req, id_reg_war, prod_req_creat_time, `request for production`.prod_count, `request for production`.id_prod, id_operations from `request for production`inner join write_off on `request for production`.id_prod_req = write_off.id_prod_req";
      MySqlCommand cmd = new MySqlCommand();
      cmd.Connection = conn;
      cmd.CommandText = sql;
      using (DbDataReader reader = cmd.ExecuteReader())
      {
        if (reader.HasRows)
        {
          while (reader.Read())
          {
            var id_prod_req = reader.GetInt32(0);
            var id_reg_war = reader.GetInt32(1);
            var prod_req_creat_time = reader.GetDateTime(2);
            var prod_count = reader.GetInt32(3);
            var id_prod = reader.GetInt32(4);
            var id_operations = reader.GetInt32(5);
            paths.Add(new RequestForProduction()
            {
              id_prod_req = id_prod_req,
              id_reg_war = id_reg_war,
              prod_req_create_time = prod_req_creat_time,
              prod_count = prod_count,
              id_prod = id_prod,
              id_operations = id_operations,
            });
          }
        }
      }
      conn.Close();
      return paths;
    }
    public ObservableCollection<OutcomeFromProduction> GetOutcomeFromProduction()
    {
      conn.Open();
      ObservableCollection<OutcomeFromProduction> paths = new ObservableCollection<OutcomeFromProduction>();
      string sql = "select * from outcome_production";
      MySqlCommand cmd = new MySqlCommand();
      cmd.Connection = conn;
      cmd.CommandText = sql;
      using (DbDataReader reader = cmd.ExecuteReader())
      {
        if (reader.HasRows)
        {
          while (reader.Read())
          {
            var id_outcome_prod = reader.GetInt32(0);
            var id_prod_req = reader.GetInt32(1);
            var id_prod = reader.GetInt32(2);
            var id_operations = reader.GetInt32(3);
            var prod_count = reader.GetInt32(4);
            var data_outcome_prod = reader.GetDateTime(5);
            paths.Add(new OutcomeFromProduction()
            {
              id_outcome_prod = id_outcome_prod,
              id_operations = id_operations,
              id_prod = id_prod,
              id_prod_req = id_prod_req,
              prod_count = prod_count,
              data_outcome_prod = data_outcome_prod,
            });
          }
        }
      }
      conn.Close();
      return paths;
    }
  }
}
