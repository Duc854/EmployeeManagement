﻿using Microsoft.EntityFrameworkCore;
using Models.Models;
using SharedInterfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class NotificationRepository : INotificationRepository

    {
        private readonly EmployeeManagementContext _context;
        public NotificationRepository() 
        {
            _context = new EmployeeManagementContext();
        }

        public async Task AddNotificationAsync(Notification notification)
        {
            await _context.Notifications
                .AddAsync(notification);

            await _context.SaveChangesAsync();
        }

        public string CreateANotification(Notification notification)
        {
            try
            {
                _context.Notifications
                    .Add(notification);
                _context.SaveChanges();

                return "Gửi thông báo thành công";
            }
            catch (Exception ex)
            {
                return $"Lỗi khi gửi thông báo: {ex.Message}";
            }
        }

        public async Task DeleteAllNotifications()
        {
            var leaves = await _context.Notifications
                .ToListAsync();
            _context.RemoveRange(leaves);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteNoti(int notiId)
        {
            var noti = await _context.Notifications
                .FirstOrDefaultAsync(x => x.NotificationId == notiId);

            if (noti == null)
                return;
            _context.Notifications
                .Remove(noti);

            await _context.SaveChangesAsync();
        }

        public List<Notification> GetAllNotification()
        {
            List<Notification> notis = new List<Notification>();
            try
            {
                notis = _context.Notifications
                    .AsNoTracking()
                    .OrderByDescending(x => x.SentDate)
                    .ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
 
            return notis;
        }

        public List<Notification> GetNotificationByEmployeeIdAndDepartmentId(int empId, int departmentId)
        {
            List<Notification> notis = new List<Notification>();

            try
            {
                notis = _context.Notifications
                    .Where(x => x.DepartmentId == departmentId || 
                            x.ReceiverId == empId || 
                            (x.DepartmentId == null && x.ReceiverId == null))
                    .ToList();
            }
            catch (Exception ex)
            { 

                throw ex;
            }

            return notis;
        }

        public List<Notification> GetNotificationBySentDate(DateTime? dateTime, int senderId, int receiverId, int departmentId)
        {
            List<Notification> notis = new List<Notification>();

            try
            {
                notis = _context.Notifications
                    .AsNoTracking()
                    .Where(x => (x.SentDate.HasValue && dateTime != null && x.SentDate.Value.Date == dateTime.Value.Date) || 
                        (x.SenderId == senderId) ||
                        (x.ReceiverId != null && x.ReceiverId == receiverId) ||
                        (x.DepartmentId != null && x.DepartmentId == departmentId))
                    .ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return notis;
        }

        public List<Notification> HomeNotifications()
        {
            try
            {
                Console.WriteLine("Đang lấy thông báo...");
                var notifications = _context.Notifications.Where(n => n.DepartmentId == null && n.ReceiverId == null)
                                            .OrderByDescending(n => n.SentDate)
                                            .Take(10)
                                            .ToList();
                Console.WriteLine($"Lấy được {notifications.Count} thông báo.");
                return notifications;
            }
            catch(Exception ex) 
            {
                throw;
            }
        }
    }
}
