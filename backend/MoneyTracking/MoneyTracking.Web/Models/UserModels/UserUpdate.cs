namespace MoneyTracking.Web.Models.UserModels
{
    public record UserUpdate(int Id, string NewLogin, string NewPassword, string NewEmail) { }
}