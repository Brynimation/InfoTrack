namespace InfoTrackBackend.Contracts;

public class SolicitorResultsResponseDto
{
    public string Name { get; set; }

    public SolicitorResultsResponseDto(string Name) 
    {
        this.Name = Name;
    }
}
