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
    public class LoginController : Controller
    {
        //POST /api/Login
        [HttpPost]
        public LoginResponse Login([FromBody] LoginRequest req)
        {
            var resp = new LoginResponse();
            try
            {
                if (LoginCheck(req.Username, req.Password))
                {
                    resp.Status = 1;
                    resp.Token = "success";
                }
                else
                {
                    resp.Status = 0;
                    resp.Token = "fail";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return resp;
        }

        private bool LoginCheck(string username, string password)
        {
            bool resp = true;
            if (username == "" || password == "")
                return false;
            string Strconn =
                "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=220.179.227.205)(PORT=6001)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=ORCL)));" +
                "User Id=C##APPLEDEV;" +
                "Password=Lty20001212;";
            OracleConnection conn = new(Strconn);
            conn.Open();
            var strSql = "select \"password_hash\" " +
                         "from \"RoomMember\" " +
                         "where \"username\"='" + username+"'";
            OracleCommand oraCmd = new(strSql, conn);
            var oraReader = oraCmd.ExecuteReader();
            if (!oraReader.Read() || oraReader.GetString(0) != password)
            {
                resp = false;
            }
            oraReader.Dispose();
            conn.Close();
            return resp;
        }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class TestController : Controller
    {
        //POST /api/Login
        [HttpGet]
        public string Test()
        {
            string ans = "hello world";
            return ans;
        }
    }
}