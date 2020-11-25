using APICommercialOptimiser.Entities;
using APICommercialOptimiser.Models;
using System.Collections.Generic;


namespace APICommercialOptimiser.Services
{
    public interface IUserService
    {
        AuthenticateResponse Authenticate(AuthenticateRequest model);
        IEnumerable<User> GetAll();
        User GetById(int id);
    }
}
