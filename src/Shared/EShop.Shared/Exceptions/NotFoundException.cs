namespace EShop.Shared.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string message) : base(message)
    {
        
    }

    public NotFoundException(string name,object key) : base($@"The {name} Entity with key {key} was not found.")
    {
        
    }
}