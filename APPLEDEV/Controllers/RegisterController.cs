using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APPLEDEV.Models;
using Microsoft.Extensions.Logging;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
namespace APPLEDEV.Controllers
{
    [Route("User/[controller]")]
    [ApiController]
    public class RegisterController : Controller
    {
        //POST /api/Login
        [HttpPost]
        public RegisterResponse Register([FromBody] RegisterRequest req)
        {
            RegisterResponse resp=new();
            string Strconn =
                "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=220.179.227.205)(PORT=6001)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=ORCL)));" +
                "User Id=C##APPLEDEV;" +
                "Password=Lty20001212;";
            OracleConnection conn = new(Strconn);
            conn.Open();
            var strSql = "select * " +
                         "from \"RoomMember\" " +
                         "where \"username\"='" + req.Username+"'";
            OracleCommand oraCmd = new(strSql, conn);
            var oraReader = oraCmd.ExecuteReader();
            if (oraReader.Read())
            {
                resp.Status=-1;
            }
            else
            {
                //insert into "Room" VALUES('605','LTY');
                strSql = "insert into \"RoomMember\" (\"username\",\"password_hash\")VALUES('" + req.Username + "','" + req.Password + "')";
                oraCmd.CommandText = strSql;
                int row = oraCmd.ExecuteNonQuery();
                if (row == 1)
                {
                    resp.Status = 1;
                }
                else
                {
                    resp.Status = 0;
                }
            }
            conn.Close();
            conn.Dispose();
            return resp;
        }
    }
}