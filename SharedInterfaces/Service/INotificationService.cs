﻿using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedInterfaces.Service
{
    public interface INotificationService
    {
        public List<Notification> HomeNotifications();
        public List<Notification> GetAllNotification();
        public List<Notification> GetNotificationBySentDate(DateTime? dateTime, int senderId, int receiverId, int departmentId);
        public string CreateANotification(Notification notification);
        public List<Notification> GetNotificationByEmployeeIdAndDepartmentId(int userId, int departmentId);
        Task DeleteNoti(int notiId);
    }
}
