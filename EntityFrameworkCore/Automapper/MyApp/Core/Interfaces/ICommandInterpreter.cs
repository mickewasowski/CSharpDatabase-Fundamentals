using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Core.Interfaces
{
    public interface ICommandInterpreter
    {
        string Read(string[] inputArgs);
    }
}
