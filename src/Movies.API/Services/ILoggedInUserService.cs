using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.API.Services
{
    public interface ILoggedInUserService
    {
        string getName();
        string getUserId();
    }
}
