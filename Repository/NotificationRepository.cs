using api.Data;
using api.Dtos;
using api.interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace api.Repository
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly ApplicationDbContext _context;

        public NotificationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Notification>> GetAllNotifications()
        {
            return await _context.Notifications.ToListAsync();
        }

        public async Task<Notification> GetNotificationById(int id)
        {
            var notification = await _context.Notifications.FindAsync(id);
            if (notification == null)
            {
                throw new KeyNotFoundException($"Notification with id {id} not found.");
            }
            return notification;
        }

        public async Task CreateNotification(CreateNotificationRequest request)
        {
            var notification = new Notification
            {
                // Map properties from request to notification
            };
            await _context.Notifications.AddAsync(notification);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateNotification(int id, UpdateNotificationRequest request)
        {
            var notification = await _context.Notifications.FindAsync(id);
            if (notification == null)
            {
                throw new KeyNotFoundException($"Notification with id {id} not found.");
            }
            // Map properties from request to notification
            _context.Notifications.Update(notification);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteNotification(int id)
        {
            var notification = await _context.Notifications.FindAsync(id);
            if (notification != null)
            {
                _context.Notifications.Remove(notification);
                await _context.SaveChangesAsync();
            }
        }
    }

    public interface INotificationRepository
    {
    }
}
