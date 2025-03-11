using DataAccess.Repository;
using Models.Models;
using SharedInterfaces.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Service
{
    public class NotificationService : INotificationService
    {
        private readonly NotificationRepository _notificationRepository;
        public NotificationService() 
        {
            _notificationRepository = new NotificationRepository();
        }

        public List<Notification> HomeNotifications()
        {
            try
            {
                return _notificationRepository.HomeNotifications();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi lấy thông báo: " + ex.Message);
                return new List<Notification>();
            }
        }
    }
}
