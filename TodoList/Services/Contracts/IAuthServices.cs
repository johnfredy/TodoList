﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoList.Services.Contracts
{
    public interface IAuthService
    {
        Task<bool> ValidateLoginAsync(string username, string password);
        string GenerateToken(DateTime fechaActual, string username, TimeSpan tiempoValidez);
    }
}