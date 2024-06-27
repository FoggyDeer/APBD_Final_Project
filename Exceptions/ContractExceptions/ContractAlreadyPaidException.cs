namespace APBD_Final_Project.Exceptions.ContractExceptions;

public class ContractAlreadyPaidException(int contractId) : Exception($"Contract with id: '{contractId}' is already paid");