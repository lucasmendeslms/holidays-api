using System.Text.Json.Serialization;
using HolidayApi.Data.ExternalModels;

public class MunicipalityModelResponse
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("nome")]
    public required string Name { get; set; }

    [JsonPropertyName("microrregiao")]
    public required Microregion Microregion { get; set; }

    [JsonPropertyName("regiao-imediata")]
    public required ImmediateRegion ImmediateRegion { get; set; }
}

public class Microregion
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("nome")]
    public required string Name { get; set; }

    [JsonPropertyName("mesorregiao")]
    public required Mesoregion Mesoregion { get; set; }
}

public class Mesoregion
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("nome")]
    public required string Name { get; set; }

    [JsonPropertyName("UF")]
    public required StateModelResponse State { get; set; }
}

public class ImmediateRegion
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("nome")]
    public required string Name { get; set; }

    [JsonPropertyName("regiao-intermediaria")]
    public required IntermediateRegion IntermediateRegion { get; set; }
}

public class IntermediateRegion
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("nome")]
    public required string Name { get; set; }

    [JsonPropertyName("UF")]
    public required StateModelResponse State { get; set; }
}