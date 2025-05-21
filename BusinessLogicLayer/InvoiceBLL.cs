using DataAccessLayer;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BusinessLogicLayer
{
    public static class InvoiceBLL
    {

        public static async Task<Invoice> GetInvoiceByIdAsync(int invoiceId)
        {
            // Gọi DAL lấy dữ liệu hóa đơn
            return await InvoiceDAL.GetInvoiceByIdAsync(invoiceId);
        }

        public static async Task<List<InvoiceItemView>> GetInvoiceItemsForDisplay(int invoiceId)
        {
            var items = new List<InvoiceItemView>();


            // Chi tiết phòng
            var roomDetails = await InvoiceRoomDetailDAL.GetRoomDetailsByInvoiceId(invoiceId);
            if (roomDetails != null)
            {
                foreach (var room in roomDetails)
                {
                    //  var roomName = await RoomDAL.GetRoomNameById(room.RoomID);

                    items.Add(new InvoiceItemView
                    {
                        Type = "Phòng",
                        Name = room.RoomID + "",
                        UnitPrice = room.RoomPrice,
                        Quantity = room.Nights
                        // Total sẽ tự tính trong property
                    });
                }
            }
            else
            {
                // Xử lý khi roomDetails null, ví dụ:
                MessageBox.Show("Không có dữ liệu phòng.");
            }

            // Chi tiết dịch vụ
            var serviceDetails = await InvoiceServiceDetailDAL.GetServiceDetailsByInvoiceId(invoiceId);
            if (roomDetails != null)
            {
                foreach (var service in serviceDetails)
                {
                    var serviceName = await ServiceDAL.GetServiceByID(service.ServiceID);

                    items.Add(new InvoiceItemView
                    {
                        Type = "Dịch vụ",
                        Name = serviceName.ServiceName,
                        UnitPrice = service.ServicePrice,
                        Quantity = service.Quantity
                        // Total sẽ tự tính trong property
                    });

                }
            }
            else
            {
                // Xử lý khi roomDetails null, ví dụ:
                MessageBox.Show("Không có dữ liệu phòng.");
            }
            return items;

        }

        public static async Task<Invoice> CreateInvoiceForBooking(int bookingId)
        {
            // 1. Lấy dữ liệu booking
            Booking booking = await BookingDAL.GetBookingByIdAsync(bookingId);
            if (booking == null)
            {
                MessageBox.Show("❌ Không tìm thấy booking.");
                return null;
            }

            // 2. Lấy danh sách RoomID
            List<int> roomIds = await BookingRoomDAL.GetRoomIdsByBookingId(bookingId);

            // 3. Tính số đêm
            int nights = (booking.CheckOutDate - booking.CheckInDate).Days;

            // 4. Tính RoomTotal
            double roomTotal = 0;
            foreach (int roomId in roomIds)
            {
                double? pricePerNight = await RoomPriceDAL.GetPriceByRoomId(roomId, booking.CheckInDate);
                if (pricePerNight.HasValue)
                {
                    roomTotal += pricePerNight.Value * nights;
                }
                else
                {
                    MessageBox.Show($"⚠ Không tìm thấy giá phòng cho phòng {roomId} vào ngày {booking.CheckInDate:yyyy-MM-dd}");
                }
            }

            // 5. Tính ServiceTotal
            double serviceTotal = 0;
            var bookingServices = await BookingServiceDAL.GetServicesByBookingId(bookingId);
            if (bookingServices != null)
            {
                foreach (var service in bookingServices)
                {
                    int serviceId = service.ServiceID;
                    int quantity = service.Quantity;
                    DateTime usedDate = service.UsedDate;

                    double? price = await ServicePriceDAL.GetPriceByServiceId(serviceId, usedDate);
                    if (price.HasValue)
                    {
                        serviceTotal += price.Value * quantity;
                    }
                    else
                    {
                        MessageBox.Show($"⚠ Không tìm thấy giá cho dịch vụ {serviceId} vào ngày {usedDate:yyyy-MM-dd}");
                    }
                }
            }

            // 6. Tính tổng
            double surcharge = 0; // logic phụ thu nếu có
            double vat = 0.10 * (roomTotal + serviceTotal + surcharge);
            double totalPayment = roomTotal + serviceTotal + surcharge + vat;

            // 7. Tạo và lưu Invoice, lấy InvoiceID
            Invoice invoice = new Invoice
            {
                BookingID = bookingId,
                InvoiceDate = DateTime.Now,
                RoomTotal = roomTotal,
                ServiceTotal = serviceTotal,
                VAT = vat,
                Surcharge = surcharge,
                TotalPayment = totalPayment
            };

            int newInvoiceId = await InvoiceDAL.InsertInvoiceAsync(invoice);
            if (newInvoiceId == 0)
            {
                MessageBox.Show("❌ Lỗi khi tạo hóa đơn.");
                return null;
            }
            invoice.InvoiceID = newInvoiceId;

            // 8. Lưu chi tiết phòng
            foreach (int roomId in roomIds)
            {
                double? pricePerNight = await RoomPriceDAL.GetPriceByRoomId(roomId, booking.CheckInDate);
                if (pricePerNight.HasValue)
                {
                    await InvoiceRoomDetailDAL.InsertInvoiceRoomDetailAsync(new InvoiceRoomDetail(
                        0, // ID auto-increment
                        invoice.InvoiceID,
                        roomId,
                        pricePerNight.Value,
                        nights
                    ));
                }
            }

            // 9. Lưu chi tiết dịch vụ
            if (bookingServices != null)
            {
                foreach (var service in bookingServices)
                {
                    double? price = await ServicePriceDAL.GetPriceByServiceId(service.ServiceID, service.UsedDate);
                    if (price.HasValue)
                    {
                        var detail = new InvoiceServiceDetail
                        {
                            InvoiceID = invoice.InvoiceID,
                            ServiceID = service.ServiceID,
                            ServicePrice = price.Value,
                            Quantity = service.Quantity
                        };

                        await InvoiceServiceDetailDAL.InsertInvoiceServiceDetailAsync(detail);

                    }
                }
            }

            return invoice;
        }
    }

    }

