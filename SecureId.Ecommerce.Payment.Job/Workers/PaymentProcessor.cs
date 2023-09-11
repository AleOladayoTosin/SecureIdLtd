using Hangfire;
using Microsoft.EntityFrameworkCore;
using SecureId.Ecommerce.ShoppingCart.Application.DTOs;
using SecureId.Ecommerce.ShoppingCart.Domain;
using SecureId.Ecommerce.ShoppingCart.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecureId.Ecommerce.Payment.Job.Workers
{
    public class PaymentProcessor
    {
        private readonly DataContext _context;
        public PaymentProcessor(DataContext context)
        {
           _context = context;
        }

        public async void ExecutePayment()
        {
            var orderHearderFromDb = await _context.orderHeaders
                .Where(u => u.PaymentStatus == PaymentType.PENDING.ToString() && u.Retries < 3).ToListAsync();
            if(orderHearderFromDb.Any() )
            {
                foreach (var item in orderHearderFromDb)
                {
                    BackgroundJob.Enqueue(() => UpdatePayment(item));
                }
            }

            
        }

        private void UpdatePayment(OrderHeader order)
        {
            try
            {
                order.PaymentStatus = PaymentType.SUCCESSFULL.ToString();
                 _context.SaveChanges();
            }
            catch (Exception)
            {
                order.PaymentStatus = PaymentType.PENDING.ToString();
                order.Retries++;
                _context.SaveChanges();
            }
        }
    }
}
