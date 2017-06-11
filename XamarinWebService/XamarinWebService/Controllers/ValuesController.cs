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
                documentURL = "https://1drv.ms/b/s!AjnPnLvwh--0ggX-34NFuuM9MS56"
            });
            listFiles.Add(new File()
            {
                documentName = "Hybrid_Apps_vs_Native_Apps",
                documentURL = "https://1drv.ms/b/s!AjnPnLvwh--0ggobqoUsuYP-Hyds"
            });

            listFiles.Add(new File()
            {
                documentName = "Patrones_de_diseño",
                documentURL = "https://1drv.ms/b/s!AjnPnLvwh--0gglpObWyTT_J8bWO"
            });

            return Json(listFiles);
        }
    }
}
