using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using XamarinWebService.Models;

namespace XamarinWebService.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        // GET api/values
        [HttpGet]
        [Route("getFiles")]
        public JsonResult Get()
        {
            var listFiles = new List<File>();
            listFiles.Add(new File()
            {
                documentName = "Arquitectura_SOA",
                documentURL = "https://drive.google.com/open?id=0B_l1vsi8qmsveGdCdkdINnM4LTQ"
            });
            listFiles.Add(new File()
            {
                documentName = "Database Management Systems 3rd Edition",
                documentURL = "https://drive.google.com/open?id=0B9aJA_iV4kHYM2dieHZhMHhyRVE"
            });

            listFiles.Add(new File()
            {
                documentName = "Patrones_de_diseño",
                documentURL = "https://drive.google.com/open?id=0B_l1vsi8qmsvYlM3cml4UllBZVE"
            });

            return Json(listFiles);
        }
    }
}
