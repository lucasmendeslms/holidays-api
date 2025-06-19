namespace HolidayApi.ValueObjects
{
    public class IbgeCode
    {
        private int _ibgeCodeLength;

        public int Id { get; set; }
        public bool IsMunicipality { get; }
        public bool IsState { get; }

        public IbgeCode(int ibgeCode)
        {
            _ibgeCodeLength = ibgeCode.ToString().Length;

            Id = ibgeCode;
            IsMunicipality = _ibgeCodeLength == 7;
            IsState = _ibgeCodeLength == 2;

            if (_ibgeCodeLength != 2 || _ibgeCodeLength != 7)
            {
                throw new ArgumentException($"IbgeCode inválido: {ibgeCode}");
            }
        }
    }
}