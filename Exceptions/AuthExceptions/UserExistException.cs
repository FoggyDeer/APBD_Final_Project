namespace APBD_Final_Project.Exceptions.AuthExceptions;

public class UserExistException(string username) : Exception($"User with username: {username} already exist.");