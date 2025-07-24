namespace HolidayApi.Data.DTOs
{
    public class MunicipalityReadDto
    {
        public int IbgeCode { get; }
        public string Name { get; }
        public StateDto State { get; }

        public MunicipalityReadDto(int ibgeCode, string name, StateDto state)
        {
            IbgeCode = ibgeCode;
            Name = name;
            State = new StateDto(state.IbgeCode, state.StateCode, state.Name);
        }
    }
}