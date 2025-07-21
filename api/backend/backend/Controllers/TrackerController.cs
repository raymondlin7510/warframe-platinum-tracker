using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace backend.Controllers
{
    
    [ApiController]
    public class TrackerController : ControllerBase
    {
        private IConfiguration _configuration;
        public TrackerController(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        [HttpGet("get_all")]
        public JsonResult get_all()
        {
            string query = "select * from plat_list";
            DataTable table = new DataTable();
            string SqlDatasource = _configuration.GetConnectionString("wfdb");
            SqlDataReader myHeader;
            using (SqlConnection myCon = new SqlConnection(SqlDatasource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myHeader = myCommand.ExecuteReader();
                    table.Load(myHeader);
                }
            }
            return new JsonResult(table);
        }

        [HttpPost("add_item")]
        public JsonResult add_item([FromForm] string item, [FromForm] string price)
        {
            int parsedPrice = int.Parse(price);
            string query = "insert into plat_list(checked, item , price) values (0, @item, @price)";
            string SqlDatasource = _configuration.GetConnectionString("wfdb");

            using (SqlConnection myCon = new SqlConnection(SqlDatasource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@item", item);
                    myCommand.Parameters.AddWithValue("@price", parsedPrice);
                    myCommand.ExecuteNonQuery();
                }
            }

            return new JsonResult("Added Successfully");
        }


        [HttpPost("delete_item")]
        public JsonResult delete_item([FromForm] string id)
        {
            string query = "delete from plat_list where id=@id";
            DataTable table = new DataTable();
            string SqlDatasource = _configuration.GetConnectionString("wfdb");
            SqlDataReader myHeader;
            using (SqlConnection myCon = new SqlConnection(SqlDatasource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@id", id);
                    myHeader = myCommand.ExecuteReader();
                    table.Load(myHeader);
                }
            }
            return new JsonResult("Deleted Successfully");
        }



    }


}
