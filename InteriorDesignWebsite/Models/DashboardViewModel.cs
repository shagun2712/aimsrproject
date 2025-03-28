using InteriorDesignWebsite.Models;

public class DashboardViewModel
{
    public List<ContactForm> ContactForms { get; set; } = new List<ContactForm>();
    public List<string> ImageUrls { get; set; } = new List<string>();
}
