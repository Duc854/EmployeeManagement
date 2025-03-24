using DataAccess.Repository;
using Models.Models;
using SharedInterfaces.Repository;
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
        private readonly IEmployeeRepository _employeeRepo;
        public NotificationService() 
        {
            _notificationRepository = new NotificationRepository();
            _employeeRepo = new EmployeeRepository();
        }

        public string CreateANotification(Notification notification)
        {
            return _notificationRepository.CreateANotification(notification);
        }

        public async Task DeleteNoti(int notiId)
        {
            await _notificationRepository.DeleteNoti(notiId);
        }

        public List<Notification> GetAllNotification()
        {
            try
            {
                return _notificationRepository.GetAllNotification();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi lấy thông báo: " + ex.Message);
                return new List<Notification>();
            }
        }

        public List<Notification> GetNotificationByEmployeeIdAndDepartmentId(int userId, int departmentId)
        {
            Employee employee = _employeeRepo.GetEmployeeByUserId(userId);

            List<Notification> notifications = _notificationRepository.GetNotificationByEmployeeIdAndDepartmentId(employee.EmployeeId, departmentId);

            return notifications;
        }

        public List<Notification> GetNotificationBySentDate(DateTime? dateTime, int senderId, int receiverId, int departmentId)
        {
            try
            {
                return _notificationRepository.GetNotificationBySentDate(dateTime, senderId, receiverId, departmentId);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi tìm thông báo: " + ex.Message);
                return new List<Notification>();
            }
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
