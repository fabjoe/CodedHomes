﻿using CodedHomes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodedHomes.Data
{
    public class ApplicationUnit : IDisposable
    {
        private DataContext _context = new DataContext();
        private IRepository<Home> _homes = null;
        private IRepository<User> _users = null;
        public IRepository<Home> Homes
        {
            get
            {
                if (this._homes == null)
                {
                    this._homes = new GenericRepository<Home>(_context);
                }
                return this._homes;

            }
        }
        public IRepository<User> Users
        {
            get
            {
                if (this._users == null)
                {
                    this._users = new GenericRepository<User>(_context);
                }
                return this._users;

            }
        }

        public void SaveChanges()
        {
            this._context.SaveChanges();
        }
        public void Dispose()
        {
            if(this._context != null)
            {
                this._context.Dispose();
            }
        }
    }
}
