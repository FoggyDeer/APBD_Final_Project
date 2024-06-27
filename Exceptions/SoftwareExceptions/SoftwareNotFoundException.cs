namespace APBD_Final_Project.Exceptions.SoftwareExceptions;

public class SoftwareNotFoundException(int softwareId) : Exception($"Software with id {softwareId} not found.");