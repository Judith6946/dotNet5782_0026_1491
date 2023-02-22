
namespace DO;


public class NotFoundException :Exception
{
    public NotFoundException(string message) : base(message) { }
    
}

public class AlreadyExistException : Exception
{
    public AlreadyExistException(string message) : base(message) { }

}

public class InvalidInputException : Exception
{
    public InvalidInputException(string message) : base(message) { }

}

public class DalConfigException : Exception
{
    public DalConfigException(string msg) : base(msg) { }
    public DalConfigException(string msg, Exception ex) : base(msg, ex) { }
}


public class DalXmlFormatException : Exception
{
    public DalXmlFormatException(string msg, Exception ex) : base(msg, ex) { }
    public DalXmlFormatException(string msg) : base(msg) { }

}

public class XMLFileLoadCreateException : Exception
{
    public XMLFileLoadCreateException(string msg, Exception ex) : base(msg, ex) { }
    public XMLFileLoadCreateException(string msg) : base(msg) { }

}
