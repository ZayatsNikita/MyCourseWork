﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using BL.dtoModels.Combined;

namespace BL.Services.Abstract
{
    public interface IFullUserServices
    { 
        public void Create(FullUser fullUser);
        public FullUser Read(
            string login,
            string Password
            );

        FullUser Read(int workerNumber);
        void Delete(FullUser fullUser);
        void Update(FullUser user);
    }
}