﻿

namespace BO;



public class InvalidInputException : Exception
{
    public InvalidInputException(string message) : base(message) { }

}

public class AlreadyDoneException : Exception
{
    public AlreadyDoneException(string message) : base(message) { }

}

public class ImpossibleException : Exception
{
    public ImpossibleException(string message) : base(message) { }

}

public class NotFoundException : Exception
{
    public NotFoundException(string message) : base(message) { }

}

public class SoldOutException : Exception
{
    public SoldOutException(string message) : base(message) { }

}

public class DalException : Exception
{
    public DalException(string message,Exception innerException) : base(message,innerException) { }

}
