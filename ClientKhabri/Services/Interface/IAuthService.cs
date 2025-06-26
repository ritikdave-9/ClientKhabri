using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientKhabri.Services.Interface
{
    public interface IAuthService
    {
        Task SignUpAsync();
        Task LoginAsync();


    }
}
