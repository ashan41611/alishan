using CDA_EnvironmentWebPortal.Data;
using CDA_EnvironmentWebPortal.Data.Repository;
using CDA_EnvironmentWebPortal.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.Common;

namespace CDA_EnvironmentWebPortal.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IGenericRepository _genericRepository;
        private readonly IBaseDataRepository _baseDataRepository;
        public EmployeeController(IGenericRepository genericRepository, IBaseDataRepository baseDataRepository)
        {
            _genericRepository = genericRepository;
            _baseDataRepository=baseDataRepository;
          
        }

        public IActionResult UserManagement()
        {
            return View();
        }


        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Register()
        {
            var viewModel = new DropdownViewModel
            {
                Zones = await _genericRepository.GetAll<Zone>("SELECT * FROM Zones") ,
                ScaleTypes = await _genericRepository.GetAll<ScaleType>("SELECT * FROM ScaleTypes"),
                ScaleNos =     await _genericRepository.GetAll<ScaleNo>("SELECT * FROM ScaleNos"),
                EmployeeTypes = await _genericRepository.GetAll<EmployeeType>("SELECT * FROM EmployeeTypes"),
                Designations = await _genericRepository.GetAll<Designation>("SELECT * FROM Designations"),
                Wings =       await  _genericRepository.GetAll<Wing>("SELECT * FROM Wings"),
                Roles=await _baseDataRepository.GetAllRoles()
            };
            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> UserEdit(int id)
        {

            var user = await _genericRepository.GetAll<EditUserDto>(string.Format("SELECT * FROM Users where userid={0}", id));
            if (user == null)
            {
                return NotFound();
            }

        

            var viewModel = new DropdownViewModelEdit
            {
                User=user.FirstOrDefault(),
                Zones = await _genericRepository.GetAll<Zone>("SELECT * FROM Zones"),
                Sectors = await _genericRepository.GetAll<Sector>("SELECT * FROM Sectors"),
                SubSectors = await _genericRepository.GetAll<SubSector>("SELECT * FROM SubSectors"),
                Beats = await _genericRepository.GetAll<Beat>("SELECT * FROM beats"),
                ScaleTypes = await _genericRepository.GetAll<ScaleType>("SELECT * FROM ScaleTypes"),
                ScaleNos =     await _genericRepository.GetAll<ScaleNo>("SELECT * FROM ScaleNos"),
                EmployeeTypes = await _genericRepository.GetAll<EmployeeType>("SELECT * FROM EmployeeTypes"),
                Designations = await _genericRepository.GetAll<Designation>("SELECT * FROM Designations"),
                Wings =       await _genericRepository.GetAll<Wing>("SELECT * FROM Wings"),
                Directorates= await _genericRepository.GetAll<Directorate>("SELECT * FROM Directorates"),
                Divisions= await _genericRepository.GetAll<Division>("SELECT * FROM Divisions"),
                Roles=await _baseDataRepository.GetAllRoles()

            };
            return View(viewModel);
        }



        [HttpPost]
        public async Task<IActionResult> UserEdit(EditUserDto User)
        {

            if (ModelState.IsValid)
            {

             
                var createdUser = await  _genericRepository.ExecuteStoredProcedureAsync<EditUserDto>("UpdateUser",User);

                return RedirectToAction("UserManagement");
            }



            var viewModel = new DropdownViewModelEdit
            {
                User=User,
                Zones = await _genericRepository.GetAll<Zone>("SELECT * FROM Zones"),
                Sectors = await _genericRepository.GetAll<Sector>("SELECT * FROM Sectors"),
                SubSectors = await _genericRepository.GetAll<SubSector>("SELECT * FROM SubSectors"),
                Beats = await _genericRepository.GetAll<Beat>("SELECT * FROM beats"),
                ScaleTypes = await _genericRepository.GetAll<ScaleType>("SELECT * FROM ScaleTypes"),
                ScaleNos =     await _genericRepository.GetAll<ScaleNo>("SELECT * FROM ScaleNos"),
                EmployeeTypes = await _genericRepository.GetAll<EmployeeType>("SELECT * FROM EmployeeTypes"),
                Designations = await _genericRepository.GetAll<Designation>("SELECT * FROM Designations"),
                Wings =       await _genericRepository.GetAll<Wing>("SELECT * FROM Wings"),
                Directorates= await _genericRepository.GetAll<Directorate>("SELECT * FROM Directorates"),
                Divisions= await _genericRepository.GetAll<Division>("SELECT * FROM Divisions"),
                Roles=await _baseDataRepository.GetAllRoles()
            };
            return View(viewModel);
        }








        [HttpPost]
        public async Task<IActionResult> Register(UserDto User)
        {

            if (ModelState.IsValid)
            {

                User.Username = User.Username.ToLower();

                if (await _genericRepository.UserExists(User.Username))
                {
                    var viewModel1 = new DropdownViewModel
                    {
                       User=User,
                        Zones = await _genericRepository.GetAll<Zone>("SELECT * FROM Zones"),
                        ScaleTypes = await _genericRepository.GetAll<ScaleType>("SELECT * FROM ScaleTypes"),
                        ScaleNos =     await _genericRepository.GetAll<ScaleNo>("SELECT * FROM ScaleNos"),
                        EmployeeTypes = await _genericRepository.GetAll<EmployeeType>("SELECT * FROM EmployeeTypes"),
                        Designations = await _genericRepository.GetAll<Designation>("SELECT * FROM Designations"),
                        Wings =       await _genericRepository.GetAll<Wing>("SELECT * FROM Wings"),
                        Roles=await _baseDataRepository.GetAllRoles()
                    };
                    return View(viewModel1);
                }

                var userToCreate = new User
                {
                    Username = User.Username,
                    Department = User.Department,
                    CNIC = User.CNIC,
                    Mobile = User.Mobile,
                    ZoneId = User.ZoneId,
                    SectorId = User.SectorId,
                    SubSectorId = User.SubSectorId,
                    BeatId = User.BeatId,
                    ScaleTypeId = User.ScaleTypeId,
                    ScaleNoId = User.ScaleNoId,
                    EmployeeTypeId = User.EmployeeTypeId,
                    DesignationId = User.DesignationId,
                    WingId = User.WingId,
                    DirectorateId = User.DirectorateId,
                    DivisionId = User.DivisionId
                };

                var createdUser = await _genericRepository.Register(userToCreate, User.Password);
                return RedirectToAction("UserManagement");
            }



            var viewModel = new DropdownViewModel
            {
                User=User,
                Zones = await _genericRepository.GetAll<Zone>("SELECT * FROM Zones"),
                ScaleTypes = await _genericRepository.GetAll<ScaleType>("SELECT * FROM ScaleTypes"),
                ScaleNos =     await _genericRepository.GetAll<ScaleNo>("SELECT * FROM ScaleNos"),
                EmployeeTypes = await _genericRepository.GetAll<EmployeeType>("SELECT * FROM EmployeeTypes"),
                Designations = await _genericRepository.GetAll<Designation>("SELECT * FROM Designations"),
                Wings =       await _genericRepository.GetAll<Wing>("SELECT * FROM Wings"),
                   Roles=await _baseDataRepository.GetAllRoles()
            };
            return View(viewModel);
        }



        [HttpGet]
        public async Task<IActionResult> GetEmployeeDutyRoster() {

            var dutyRoaster = await _genericRepository.GetDutyRoaster();

            return View(dutyRoaster);


        }


        [HttpPost]
        public async Task<IActionResult> GetDutyRoster([FromForm] DataTablesRequest request)
        {
        

      var result=      await _genericRepository.GetDutyRoasterV1(request);
        var    totalRecords = result.FirstOrDefault().TotalRecord;
            var response = new
            {
                draw = request.Draw,
                recordsTotal = totalRecords,
                recordsFiltered = totalRecords,
                data = result.ToList()
            };


            return Ok(  response );
           
        }

        [HttpPost]
        public async Task<IActionResult> GetMembers([FromForm] DataTablesRequest request,int supervisorId)
        {


            var result = await _genericRepository.GetDutyRoasterEmployeeV1(request,supervisorId);
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


    }
}
