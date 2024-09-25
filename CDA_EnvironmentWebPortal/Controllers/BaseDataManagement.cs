using CDA_EnvironmentWebPortal.Data.Repository;
using CDA_EnvironmentWebPortal.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace CDA_EnvironmentWebPortal.Controllers
{
   
    public class BaseDataManagement : Controller
    {

        private readonly IGenericRepository _genericRepository;

        public BaseDataManagement(IGenericRepository genericRepository)
        {
            _genericRepository=genericRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Zone()
        {
            return PartialView("~/Views/BaseDataManagement/Partial/Zone.cshtml");
        }
        [HttpGet]
        public async Task<IActionResult> AddEditZone(int? id)
        {
            if (id.HasValue)
            {
                var zone = await _genericRepository.GetAll<Zone>("SELECT * FROM Zones WHERE ZoneId = @ZoneId", new { ZoneId = id });
                return PartialView("_AddEditZone", zone.FirstOrDefault());
            }
            return PartialView("_AddEditZone", new Zone());
        }

        [HttpPost]
        public async Task<IActionResult> SaveZone(Zone zone)
        {
            SpResponseBack spResponseBack = new SpResponseBack();
            spResponseBack.Code = 400;
          
            if (ModelState.IsValid)
            {
               
                spResponseBack.Code = 400;
                spResponseBack.Msg="Something Went Wrong.";
                if (zone.ZoneId == 0)
                {
              var res1=      await _genericRepository.ExecuteStoredProcedureAsync<SpResponseBack>("AddEditZone", new {  ZoneName=zone.ZoneName, CreatedBy=0 });

                    if (res1 == null)
                    {
                        return Json(spResponseBack);
                    }
                    else
                    {
                        spResponseBack= res1.FirstOrDefault();
                        return Json(spResponseBack);
                    }


                }
                else
                {

                  var  res = await _genericRepository.ExecuteStoredProcedureAsync<SpResponseBack>("AddEditZone", new { ZoneId = zone.ZoneId ,ZoneName = zone.ZoneName, CreatedBy = 0 });

                    if (res == null)
                    {
                        return Json(spResponseBack);
                    }
                    else
                    {
                        spResponseBack= res.FirstOrDefault();
                        return Json(spResponseBack);
                    }
                }
              
              
            }
            spResponseBack.Msg="Error";
            return Json(spResponseBack);
        }



        [HttpPost]
        public async Task<IActionResult> Zone([FromForm] DataTablesRequest request)
        {


            var result = await _genericRepository.GetZone(request);
            var totalRecords = result.Count();
            var response = new
            {
                draw = request.Draw,
                recordsTotal = totalRecords,
                recordsFiltered = totalRecords,
                data = result.ToList()
            };


            return Ok(response);

        }


        public IActionResult Sector()
        {
            return PartialView("~/Views/BaseDataManagement/Partial/Sector.cshtml");
        }
           public IActionResult SubSector()
        {
            return PartialView("~/Views/BaseDataManagement/Partial/SubSector.cshtml");
        }
               public IActionResult Beat()
        {
            return PartialView("~/Views/BaseDataManagement/Partial/Beat.cshtml");
        }
                   public IActionResult ScaleType()
        {
            return PartialView("~/Views/BaseDataManagement/Partial/ScaleType.cshtml");
        }
                        public IActionResult ScaleNo()
        {
            return PartialView("~/Views/BaseDataManagement/Partial/ScaleNo.cshtml");
        }
                          public IActionResult EmployeeType()
        {
            return PartialView("~/Views/BaseDataManagement/Partial/EmployeeType.cshtml");
        }
                              public IActionResult Designation()
        {
            return PartialView("~/Views/BaseDataManagement/Partial/Designation.cshtml");
        }

                                   public IActionResult Wing()
        {
            return PartialView("~/Views/BaseDataManagement/Partial/Wing.cshtml");
        }




        public IActionResult Directorate()
        {
            return PartialView("~/Views/BaseDataManagement/Partial/Directorate.cshtml");
        }
        public IActionResult Division()
        {
            return PartialView("~/Views/BaseDataManagement/Partial/Division.cshtml");
        }
       




    }
}
