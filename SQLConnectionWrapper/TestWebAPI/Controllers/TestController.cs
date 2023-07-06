using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestWebAPI.Contexts;
using Dapper;
using System.Diagnostics;
using Npgsql;

namespace TestWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private Test2Context _context2;
        private TestContext _context1;
        public TestController(Test2Context context2, TestContext context)
        {
            _context2 = context2;
            _context1 = context;
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> Test()
        {
            Stopwatch cc = new Stopwatch();
            cc.Start();
            for(int i = 0; i < 100000; i++)
            {
                using (NpgsqlConnection connection = new NpgsqlConnection("User ID=postgres;Password=Europe1234;Host=localhost;Port=5432;Database=CampaignsDB;Pooling=true;Minimum Pool Size=0;Maximum Pool Size=30;Connection Lifetime=0;Timeout = 100"))
                {
                    connection.Open();
                    connection.Query<string>("select * from \"Campaigns\"");
                }
                //_context1.RunQuery<IEnumerable<string>>(x => x.Query<string>("select * from \"Campaigns\""));
            }
            cc.Stop();
            Console.WriteLine(cc.Elapsed.ToString());
            return Ok();
        }
    }
}
