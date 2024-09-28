namespace PetFamily.Domain.Volunteer;

public record Social
{
    public Social(string name, string link)
    {
        Name = name;
        Link = link;
    }
    public string Name { get; private set; }
    public string Link { get; private set; }
    
    public static Social Create(string name, string link) => new Social(name, link);
}