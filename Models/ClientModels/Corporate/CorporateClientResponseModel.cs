namespace APBD_Final_Project.Models.ClientModels;

public class CorporateClientResponseModel : ClientModel
{
    public int ClientId { get; set; }
    public string CompanyName { get; set; }
    public string KRS { get; set; }
}