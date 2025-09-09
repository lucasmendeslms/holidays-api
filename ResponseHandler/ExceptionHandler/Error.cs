namespace HolidayApi.ResponseHandler
{
    public sealed record Error(int Code, string Description)
    {
        public static readonly Error InvalidIbgeCode = new(400, "Invalid ibge code");
        public static readonly Error InvalidDate = new(400, "Invalid date format. Please use 'YYYY-MM-DD' or 'MM-DD'.");

        public static readonly Error HolidayNotFound = new(404, "Holiday not found");
        public static readonly Error StateNotFound = new(404, "State not found");
        public static readonly Error MunicipalityNotFound = new(404, "Municipality not found");

        public static readonly Error HolidayConflict = new(409, "This holiday is already registered.");

        public static readonly Error HolidayUpdateFailed = new(500, "Failed to update holiday name.");
        public static readonly Error SaveStateFailed = new(500, "An error occurred while saving the State.");
        public static readonly Error IbgeServiceFailure = new(500, "Failed to retrieve data from IBGE API.");
        public static readonly Error SaveHolidayFailed = new(500, "An error occurred while saving the holiday.");
        public static readonly Error DeleteHolidayFailed = new(500, "An error occurred while deleting the holiday.");

        public static readonly Error IbgeDeserializationFailure = new(502, "Failed to parse the response from IBGE API.");

        public static readonly Error IbgeServiceUnavailable = new(503, "IBGE service is temporarily unavailable. Please try again later.");
    }
}