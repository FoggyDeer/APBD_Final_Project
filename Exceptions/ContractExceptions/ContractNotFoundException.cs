namespace APBD_Final_Project.Exceptions.ContractExceptions;

public class ContractNotFoundException(int contractId) : Exception($"Contract with id: '{contractId}' not found");