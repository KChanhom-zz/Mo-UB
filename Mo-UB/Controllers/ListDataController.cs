using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Umbraco.Web.Mvc;

namespace Mo_UB.Controllers
{
    public class ListDataController : SurfaceController
    {

        public ActionResult Click()
        {
            return View();            
        }

        public JsonResult GetDataJson()
        {
            //IEnumerable<Product> data = GetData(selectedCategory);
            string connetionString;
            SqlConnection cnn;
            connetionString = @"Data Source=pranserv;Initial Catalog=HZ-MASTER;User ID=EHAPPS;Password=sl@ve123";
            cnn = new SqlConnection(connetionString);
            cnn.Open();
            SqlCommand command;

            string sql;
            sql = "select * from FacThailand where FactoryAddrMoo = '15'";
            command = new SqlCommand(sql, cnn);
            SqlDataReader rdr = command.ExecuteReader();
            var fieldCount = rdr.FieldCount;

            string data;
            if (rdr.HasRows)
            {
                DataTable dt = new DataTable();
                dt.Load(rdr);

                data = JsonConvert.SerializeObject(dt);
                //rdr.Close();
                command.Dispose();
                cnn.Close();
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return null;
            }
        }
    }
}