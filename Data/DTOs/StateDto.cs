using HolidayApi.Data.Entities;

namespace HolidayApi.Data.DTOs
{
    public class StateDto
    {
        public int IbgeCode { get; }
        public string StateCode { get; }
        public string Name { get; }

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

        public static implicit operator State(StateDto dto)
        {
            return new State
            {
                Name = dto.Name,
                IbgeCode = dto.IbgeCode,
                StateCode = dto.StateCode
            };
        }
    }


}