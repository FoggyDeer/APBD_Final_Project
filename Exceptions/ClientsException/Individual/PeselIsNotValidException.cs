namespace APBD_Final_Project.Exceptions.ClientsException.Individual;

public class PeselIsNotValidException(string pesel) : Exception($"Client with PESEL: 'pesel' already exists");