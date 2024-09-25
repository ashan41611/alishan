using CDA_EnvironmentWebPortal.Data.Repository;
using CDA_EnvironmentWebPortal.Models;

namespace CDA_EnvironmentWebPortal.Data;

public interface IBaseDataRepository
{
    Task<IEnumerable<Sector>> GetSectorsByZoneId(int zoneId);
    Task<IEnumerable<SubSector>> GetSubSectorsBySectorId(int sectorId);
    Task<IEnumerable<Beat>> GetBeatsBySubSectorId(int subSectorId);
    Task<IEnumerable<Directorate>> GetDirectoratesByWingId(int wingId);
    Task<IEnumerable<Division>> GetDivisionsByDirectorateId(int directorateId);
    Task<IEnumerable<Role>> GetAllRoles();
}
public class BaseDataRepository : IBaseDataRepository
{
    private readonly IGenericRepository _genericRepository;

    public BaseDataRepository(IGenericRepository genericRepository)
    {
        _genericRepository = genericRepository;
    }

    public async Task<IEnumerable<Sector>> GetSectorsByZoneId(int zoneId)
    {
        return await _genericRepository.GetAll<Sector>("SELECT * FROM Sectors WHERE ZoneId = @ZoneId", new { ZoneId = zoneId });
    }

    public async Task<IEnumerable<SubSector>> GetSubSectorsBySectorId(int sectorId)
    {
        return await _genericRepository.GetAll<SubSector>("SELECT * FROM SubSectors WHERE SectorId = @SectorId", new { SectorId = sectorId });
    }

    public async Task<IEnumerable<Beat>> GetBeatsBySubSectorId(int subSectorId)
    {
        return await _genericRepository.GetAll<Beat>("SELECT * FROM Beats WHERE SubSectorId = @SubSectorId", new { SubSectorId = subSectorId });
    }

    public async Task<IEnumerable<Directorate>> GetDirectoratesByWingId(int wingId)
    {
        return await _genericRepository.GetAll<Directorate>("SELECT * FROM Directorates WHERE WingId = @WingId", new { WingId = wingId });
    }

    public async Task<IEnumerable<Division>> GetDivisionsByDirectorateId(int directorateId)
    {
        return await _genericRepository.GetAll<Division>("SELECT * FROM Divisions WHERE DirectorateId = @DirectorateId", new { DirectorateId = directorateId });
    }

    public async Task<IEnumerable<Role>> GetAllRoles()
    {
        return await _genericRepository.GetAll<Role>("SELECT * FROM Roles");
    }


}

