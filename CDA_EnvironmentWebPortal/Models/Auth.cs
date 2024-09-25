using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;
using System.Net.NetworkInformation;

namespace CDA_EnvironmentWebPortal.Models;
public class UserLoginViewModel
{
	[Required(ErrorMessage = "Username is required")]
	//[EmailAddress(ErrorMessage = "Invalid email address")]
	public string Email { get; set; }

	[Required(ErrorMessage = "Password is required")]
	[DataType(DataType.Password)]
	[MaxLength(256)]
	public string Password { get; set; }

	[Display(Name = "Remember me")]
	public bool RememberMe { get; set; }
}

public class UserDto
{
    public int userid { get; set; }
    [Required(ErrorMessage = "Username is required")]
    [MaxLength(256)]
    public string Username { get; set; }
    [Required(ErrorMessage = "Password is required")]
    [MaxLength(256)]
    [DataType(DataType.Password)]
    public string Password { get; set; }
    [Required(ErrorMessage = "Department is required")]
    [MaxLength(150)]
    public string Department { get; set; }
    [Required(ErrorMessage = "CNIC is required")]
    [MaxLength(13,ErrorMessage = "CNIC 13 digits")]
    public string CNIC { get; set; }
    [Required(ErrorMessage = "Mobile Number is required")]
    [MaxLength(11, ErrorMessage = "Mobile 11 digits")]
    public string Mobile { get; set; }
    [Required(ErrorMessage = "Zone is required")]
    public int ZoneId { get; set; }
  
    public int SectorId { get; set; }
   
    public int SubSectorId { get; set; }
    
    public int BeatId { get; set; }
   
    public int ScaleTypeId { get; set; }
    
    public int ScaleNoId { get; set; }
   
    public int EmployeeTypeId { get; set; }
   
    public int DesignationId { get; set; }
   
    public int WingId { get; set; }
   
    public int DirectorateId { get; set; }

    public int DivisionId { get; set; }
    public int RoleID { get; set; }
}




public class EditUserDto
{
    public int userid { get; set; }
    [Required(ErrorMessage = "Username is required")]
    [MaxLength(256)]
    public string Username { get; set; }

    [Required(ErrorMessage = "Department is required")]
    [MaxLength(150)]
    public string Department { get; set; }
    [Required(ErrorMessage = "CNIC is required")]
    [MaxLength(13, ErrorMessage = "CNIC 13 digits")]
    public string CNIC { get; set; }
    [Required(ErrorMessage = "Mobile is required")]
    [MaxLength(11, ErrorMessage = "Mobile 11 digits")]
    public string Mobile { get; set; }
    [Required(ErrorMessage = "Zone is required")]
    public int ZoneId { get; set; }

    public int SectorId { get; set; }

    public int SubSectorId { get; set; }

    public int BeatId { get; set; }

    public int ScaleTypeId { get; set; }

    public int ScaleNoId { get; set; }

    public int EmployeeTypeId { get; set; }

    public int DesignationId { get; set; }

    public int WingId { get; set; }

    public int DirectorateId { get; set; }

    public int DivisionId { get; set; }
    public int RoleID { get; set; }
}

public class User
{

	public int UserId { get; set; }
	public string Username { get; set; }
	public byte[] PasswordHash { get; set; }
	public byte[] PasswordSalt { get; set; }

	public string Department { get; set; }
	public string CNIC { get; set; }
	public string Mobile { get; set; }
	public int ZoneId { get; set; }
	public int SectorId { get; set; }
	public int SubSectorId { get; set; }
	public int BeatId { get; set; }
	public int ScaleTypeId { get; set; }
	public int ScaleNoId { get; set; }
	public int EmployeeTypeId { get; set; }
	public int DesignationId { get; set; }
	public int WingId { get; set; }
	public int DirectorateId { get; set; }
	public int DivisionId { get; set; }
  

}

public class GetEmployee
{
    public int EID { get; set; }
    public string EmployeeName { get; set; }
    public string EmployeeCNIC { get; set; }
    public string EmployeeMobile { get; set; }
    public string EmployeeType { get; set; }
    public string Zone { get; set; }
    public string Sector { get; set; }
    public string SubSector { get; set; }
    public string Beat { get; set; }
    public string ScaleType { get; set; }
    public string ScaleNo { get; set; }
    public string Wing { get; set; }
    public string Directorate { get; set; }
    public string Division { get; set; }
    public int SID { get; set; }

    // Supervisor properties
    public string SupervisorName { get; set; }
    public string SupervisorDepartment { get; set; }
    public string SupervisorCNIC { get; set; }
    public string SupervisorMobile { get; set; }
    public string SupervisorZone { get; set; }
    public string SupervisorSector { get; set; }
    public string SupervisorSubSector { get; set; }
    public string SupervisorBeat { get; set; }
    public string SupervisorScaleType { get; set; }
    public string SupervisorWing { get; set; }
    public string SupervisorDirectorate { get; set; }
    public string SupervisorDivision { get; set; }

}

public class GetDutyRoasterVm
{
    public int SupervisorID { get; set; }
    public string SupervisorUsername { get; set; }
    public string SupervisorName { get; set; }
    public string SupervisorDepartment { get; set; }
    public string SupervisorCNIC { get; set; }
    public string SupervisorMobile { get; set; }
    public string SupervisorZone { get; set; }
    public string SupervisorSector { get; set; }
    public string SupervisorSubSector { get; set; }
    public string SupervisorBeat { get; set; }
    public string SupervisorScaleType { get; set; }
    public string SupervisorWing { get; set; }
    public string SupervisorDirectorate { get; set; }
    public string SupervisorDivision { get; set; }
    public int TotalRecord { get; set; }
    public List<GetEmployee> Members { get; set; }

}


public class DropdownViewModelEdit
{
    public EditUserDto User { get; set; }


    public List<Zone> Zones { get; set; }
    public List<Sector> Sectors { get; set; }
    public List<SubSector> SubSectors { get; set; }
    public List<Beat> Beats { get; set; }
    public List<ScaleType> ScaleTypes { get; set; }
    public List<ScaleNo> ScaleNos { get; set; }
    public List<EmployeeType> EmployeeTypes { get; set; }
    public List<Designation> Designations { get; set; }
    public List<Wing> Wings { get; set; }
    public List<Directorate> Directorates { get; set; }
    public List<Division> Divisions { get; set; }
    public IEnumerable<Role> Roles { get; set; }

}


public class SpResponseBack
{
    public int Code { get; set; }
    public string Msg { get; set; }
}

public class DropdownViewModel
{
    public UserDto User { get; set; }
 

    public List<Zone> Zones { get; set; }
    public List<Sector> Sectors { get; set; }
    public List<SubSector> SubSectors { get; set; }
    public List<Beat> Beats { get; set; }
    public List<ScaleType> ScaleTypes { get; set; }
    public List<ScaleNo> ScaleNos { get; set; }
    public List<EmployeeType> EmployeeTypes { get; set; }
    public List<Designation> Designations { get; set; }
    public List<Wing> Wings { get; set; }
    public List<Directorate> Directorates { get; set; }
    public List<Division> Divisions { get; set; }
    public IEnumerable<Role> Roles { get; set; }

}

public class Zone
{
    public int ZoneId { get; set; }
    public string ZoneName { get; set; }
}


public class Role
{
    public int id { get; set; }
    public string RoleName { get; set; }
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool IsActive { get; set; }

}

public class Sector
{
    public int SectorId { get; set; }
    public string SectorName { get; set; }
    public int ZoneId { get; set; }
}

public class SubSector
{
    public int SubSectorId { get; set; }
    public string SubSectorName { get; set; }
    public int SectorId { get; set; }
}

public class Beat
{
    public int BeatId { get; set; }
    public string BeatName { get; set; }
    public int SubSectorId { get; set; }
}

public class ScaleType
{
    public int ScaleTypeId { get; set; }
    public string ScaleTypeName { get; set; }
}

public class ScaleNo
{
    public int ScaleNoId { get; set; }
    public string ScaleNoName { get; set; }
}

public class EmployeeType
{
    public int EmployeeTypeId { get; set; }
    public string EmployeeTypeName { get; set; }
}

public class Designation
{
    public int DesignationId { get; set; }
    public string DesignationName { get; set; }
}

public class Wing
{
    public int WingId { get; set; }
    public string WingName { get; set; }
}

public class Directorate
{
    public int DirectorateId { get; set; }
    public string DirectorateName { get; set; }
    public int WingId { get; set; }
}

public class Division
{
    public int DivisionId { get; set; }
    public string DivisionName { get; set; }
    public int DirectorateId { get; set; }
}



public class DataTablesRequest
{
    public int Draw { get; set; }
    public int Start { get; set; }
    public int Length { get; set; }
    public Search Search { get; set; }
    public Column[] Columns { get; set; }
    public Order[] Order { get; set; }
}

public class Search
{
    public string Value { get; set; }
    public string Regex { get; set; }
}

public class Column
{
    public string Data { get; set; }
    public string Name { get; set; }
    public bool Searchable { get; set; }
    public bool Orderable { get; set; }
    public Search Search { get; set; }
}

public class Order
{
    public int Column { get; set; }
    public string Dir { get; set; }
}





