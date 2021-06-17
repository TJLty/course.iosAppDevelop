using System;
using Microsoft.AspNetCore.Mvc;
using APPLEDEV.Models;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
namespace APPLEDEV.Controllers
{
    [Route("Room/[controller]")]
    [ApiController]
    public class AddController : Controller
    {
        [HttpPost]
        public int AddRoom([FromBody] Room req)
        {
            int resp;
            string Strconn =
                "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=220.179.227.205)(PORT=6001)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=ORCL)));" +
                "User Id=C##APPLEDEV;" +
                "Password=Lty20001212;";
            OracleConnection conn = new(Strconn);
            conn.Open();
            var strSql = "select * " +
                         "from \"Room\" " +
                         "where \"RoomName\"=" + req.Name;
            OracleCommand oraCmd = new(strSql, conn);
            var oraReader = oraCmd.ExecuteReader();
            if (oraReader.Read())
            {
                resp = -1;
            }
            else
            {
                //insert into "Room" VALUES('605','LTY');
                strSql = "insert into \"Room\" VALUES('" + req.Name + "','" + req.Owner + "')";
                oraCmd.CommandText = strSql;
                int row = oraCmd.ExecuteNonQuery();
                if (row == 1)
                {
                    resp = 1;
                }
                else
                {
                    resp = 2;
                }
            }
            conn.Close();
            conn.Dispose();
            return resp;
        }
    }
    
    [Route("Room/[controller]")]
    [ApiController]
    public class DeleteController : Controller
    {
        [HttpPost]
        public void deleteRoom([FromBody] Room req)
        {
            string Strconn =
                "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=220.179.227.205)(PORT=6001)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=ORCL)));" +
                "User Id=C##APPLEDEV;" +
                "Password=Lty20001212;";
            OracleConnection conn = new(Strconn);
            conn.Open();
            var strSql = "DELETE FROM \"Room\" WHERE \"Room\".\"RoomName\" =" + req.Name;
            OracleCommand oraCmd = new();
            oraCmd.Connection = conn;
            oraCmd.CommandText = strSql;
            Console.WriteLine(oraCmd.ExecuteNonQuery());
            conn.Close();
            conn.Dispose();
            return ;
        }
    }
}