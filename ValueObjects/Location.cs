namespace HolidayApi.ValueObjects
{
    public class Location
    {
        private int _ibgeCodeLength;

        public int IbgeCode { get; set; }
        public bool IsMunicipality { get; }
        public bool IsState { get; }

        public Location(int ibgeCode)
        {
            _ibgeCodeLength = ibgeCode.ToString().Length;

            IbgeCode = ibgeCode;
            IsMunicipality = _ibgeCodeLength == 7;
            IsState = _ibgeCodeLength == 2;

            if (_ibgeCodeLength != 2 || _ibgeCodeLength != 7)
            {
                throw new ArgumentException($"IbgeCode inválido: {ibgeCode}");
            }
        }
    }
}