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
                documentName = "Doc1",
                documentURL = "http://www.onedrive.com/file1"
            });
            listFiles.Add(new File()
            {
                documentName = "Doc1",
                documentURL = "http://www.onedrive.com/file1"
            });

            return Json(listFiles);
        }
    }
}
