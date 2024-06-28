using APBD_Final_Project.DbContexts;
using APBD_Final_Project.DbContexts.Abstract;
using APBD_Final_Project.Entities;
using APBD_Final_Project.Exceptions.SoftwareExceptions;
using APBD_Final_Project.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace APBD_Final_Project.Repositories;

public class SoftwareRepository(IApplicationContext context) : ISoftwareRepository
{
    public async Task<Software> GetSoftwareById(int softwareId)
    {
        Software? software = await context.Software
            .FirstOrDefaultAsync(s => s.SoftwareId == softwareId);
        
        if(software == null)
            throw new SoftwareNotFoundException(softwareId);
        
        return software;
    }
}