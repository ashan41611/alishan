using CDA_EnvironmentWebPortal.Data;
using CDA_EnvironmentWebPortal.Data.Repository;
using CDA_EnvironmentWebPortal.Models;
using Microsoft.AspNetCore.Mvc;

namespace CDA_EnvironmentWebPortal.Controllers
{
    public class DropDown : Controller
    {
        private readonly IBaseDataRepository _baseDataRepository;
        public DropDown(IBaseDataRepository baseDataRepository)
        {
            _baseDataRepository=baseDataRepository;
        }


        [HttpGet]
        public async Task<JsonResult> GetSectors(int zoneId)
        {
            var sectors = await _baseDataRepository.GetSectorsByZoneId(zoneId);
            return Json(sectors);
        }

        [HttpGet]
        public async Task<JsonResult> GetSubSectors(int sectorId)
        {
            var subSectors = await _baseDataRepository.GetSubSectorsBySectorId(sectorId);
            return Json(subSectors);
        }

        [HttpGet]
        public async Task<JsonResult> GetBeats(int subSectorId)
        {
            var beats = await _baseDataRepository.GetBeatsBySubSectorId(subSectorId);
            return Json(beats);
        }

        [HttpGet]
        public async Task<JsonResult> GetDirectorates(int wingId)
        {
            var directorates = await _baseDataRepository.GetDirectoratesByWingId(wingId);
            return Json(directorates);
        }

        [HttpGet]
        public async Task<JsonResult> GetDivisions(int directorateId)
        {
            var divisions = await _baseDataRepository.GetDivisionsByDirectorateId(directorateId);
            return Json(divisions);
        }
           [HttpGet]
        public async Task<JsonResult> GetAllRoles()
        {
            var Role = await _baseDataRepository.GetAllRoles();
            return Json(Role);
        }



    }
}
