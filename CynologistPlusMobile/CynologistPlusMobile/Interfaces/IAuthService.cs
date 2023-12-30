using CynologistPlusMobile.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CynologistPlusMobile.Interfaces
{
    public interface IAuthService
    {
        Task<bool> SignIn(string username, string password);
        int GetCynologistIdFromToken();
        Task<Cynologist> GetCynologistById(int id);
    }
}
