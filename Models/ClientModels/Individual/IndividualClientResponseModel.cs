namespace APBD_Final_Project.Models.ClientModels;

public class IndividualClientResponseModel : ClientModel
{
    public int ClientId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Pesel { get; set; }
}