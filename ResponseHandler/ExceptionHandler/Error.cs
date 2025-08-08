namespace HolidayApi.ResponseHandler
{
    public sealed record Error(int Code, string Description)
    {
        public static readonly Error HolidayNotFound = new(404, "Holiday not found");
    }
}