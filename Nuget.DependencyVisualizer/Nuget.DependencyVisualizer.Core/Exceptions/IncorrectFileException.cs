using System;

namespace Nuget.DependencyVisualizer.Core.Exceptions
{
    public class IncorrectFileException : Exception
    {
        public IncorrectFileException(string message) : base(message)
        {
            
        }
    }
}