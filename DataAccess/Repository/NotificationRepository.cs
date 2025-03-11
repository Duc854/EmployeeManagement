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
        public List<Notification> HomeNotifications()
        {
            try
            {
                Console.WriteLine("Đang lấy thông báo...");
                var notifications = _context.Notifications
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
