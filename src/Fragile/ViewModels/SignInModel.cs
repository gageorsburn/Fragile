﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fragile.ViewModels
{
    public class SignInModel
    {
        public string Email
        {
            get;
            set;
        }

        public string Password
        {
            get;
            set;
        }

        public string ResetPasswordToken
        {
            get;
            set;
        }
    }
}
