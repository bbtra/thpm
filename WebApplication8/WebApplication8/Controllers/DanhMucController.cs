using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication8.Models;

namespace WebApplication8.Controllers
{
    public class DanhMucController : ApiController
    {
        CSDLTestEntities db =new CSDLTestEntities();
        [HttpGet]
        public List<DanhMuc> LayDM()
        {
            return db.DanhMucs.ToList();
        }
    }
}
