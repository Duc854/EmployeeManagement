﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Models;

namespace SharedInterfaces.Repository
{
    public interface IUserRepository
    {
        public User GetUserByUsername(string username);
        public void AddUser(User user);
        public void UpdateUser(User user);
        public User GetUserById(int userId);
        public List<User> GetAllUser();
        public Task<User> AddUserBackup(User user);
        Task DeleteAllUsers();
        List<User> GetAllAdmin();
    }
}
