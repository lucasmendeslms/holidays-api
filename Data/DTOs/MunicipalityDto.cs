using HolidayApi.Data.Entities;

namespace HolidayApi.Data.DTOs
{
    public class MunicipalityDto
    {
        public string Name { get; }
        public int IbgeCode { get; }
        public int StateId { get; }

        public MunicipalityDto(string name, int ibgeCode, int stateId)
        {
            Name = name;
            IbgeCode = ibgeCode;
            StateId = stateId;
        }

        public static implicit operator MunicipalityDto(Municipality municipality)
        {
            return new MunicipalityDto(municipality.Name, municipality.IbgeCode, municipality.StateId);
        }

        public static implicit operator Municipality(MunicipalityDto dto)
        {
            return new Municipality
            {
                Name = dto.Name,
                IbgeCode = dto.IbgeCode,
                StateId = dto.StateId
            };
        }
    }
}