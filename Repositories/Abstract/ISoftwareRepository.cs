using APBD_Final_Project.Entities;

namespace APBD_Final_Project.Repositories.Abstract;

public interface ISoftwareRepository
{
    Task<Software> GetSoftwareById(int id);
}