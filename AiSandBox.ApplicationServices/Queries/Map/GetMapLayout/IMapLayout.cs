namespace AiSandBox.ApplicationServices.Queries.Maps.GetMapLayout;

public interface IMapLayout
{
    MapLayoutResponse GetFromMemory(Guid guid);

    MapLayoutResponse GetFromFile(Guid guid);
}
