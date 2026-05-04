namespace BuildingStore.Web.Services;

public interface IUserSessionService
{
    int? CurrentUserId { get; set; }
}
