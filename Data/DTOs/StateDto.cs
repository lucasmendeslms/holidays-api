using HolidayApi.Data.Entities;

namespace HolidayApi.Data.DTOs
{
    public class StateDto
    {
        public int IbgeCode { get; set; }
        public string StateCode { get; set; }
        public string Name { get; set; }

        public StateDto(int ibgeCode, string stateCode, string name)
        {
            IbgeCode = ibgeCode;
            StateCode = stateCode;
            Name = name;
        }

        public static implicit operator StateDto(State state)
        {
            return new StateDto(state.IbgeCode, state.StateCode, state.Name);
        }
    }


}