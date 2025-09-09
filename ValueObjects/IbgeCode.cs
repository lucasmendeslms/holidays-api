using HolidayApi.ResponseHandler;

namespace HolidayApi.ValueObjects
{
    public class IbgeCode
    {
        public int Id { get; }
        public bool IsMunicipality => Id.ToString().Length == 7;
        public bool IsState => Id.ToString().Length == 2;

        private IbgeCode(int ibgeCode)
        {
            Id = ibgeCode;
        }

        public static Result<IbgeCode> Validate(int ibgeCode)
        {
            int ibgeCodeLength = ibgeCode.ToString().Length;

            return ibgeCodeLength != 2 && ibgeCodeLength != 7
                ? Result<IbgeCode>.Failure(Error.InvalidIbgeCode)
                : Result<IbgeCode>.Success(new IbgeCode(ibgeCode));
        }
    }
}