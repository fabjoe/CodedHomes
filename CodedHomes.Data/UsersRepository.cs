﻿using CodedHomes.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodedHomes.Data
{
    public class UsersRepository : GenericRepository<User>
    {
        public UsersRepository(DbContext context) : base(context)
        {

        }
    }
}
