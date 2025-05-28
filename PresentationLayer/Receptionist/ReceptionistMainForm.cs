using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Entities;
using BusinessLogicLayer;
using NLog;

namespace PresentationLayer.Receptionist
{
    
    public partial class ReceptionistMainForm : Form
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        public List<RoomCard> rooms = new List<RoomCard>();
        public ReceptionistMainForm()
        {
            InitializeComponent();
        }
        public void LoadRoomCards(List<RoomCard> roomCards)
        {
            flpRoomList.Controls.Clear();


            foreach (var roomcard in roomCards)
            {
                flpRoomList.Controls.Add(roomcard);
            }
        }
        

        private async void ReceptionistMainForm_Load(object sender, EventArgs e)
        {
            // Lấy danh sách loại phòng từ Database
            List<string> roomTypes = await RoomBLL.GetRoomType();
            // Lấy danh sách phòng từ Database
            List<RoomInfo> roominfos = await RoomBLL.GetRoomInfo();
            foreach (var roominfo in roominfos)
            {
                RoomCard roomCard = new RoomCard(roominfo.RoomID, roominfo.RoomType, roominfo.Capacity, roominfo.Description);
                rooms.Add(roomCard);
            }

            // Load filter Roomtype
            foreach (var roomType in roomTypes)
            {
                clbRoomType.Items.Add(roomType);
            }
            // Load filter Roomtype
            DateTime selectedDate = dateTimePicker1.Value;
            rooms = await SetAvaiable.SetAvailable(rooms,selectedDate, selectedDate);
            if (rooms != null)
            {
                LoadRoomCards(rooms);
            }
            else
            {
                MessageBox.Show("❌ Lỗi khi lấy thông tin phòng");
            }

            //setup value cho DateTimePicker
            dateTimePicker1.Value = DateTime.Today;
            dateTimePicker2.Value = dateTimePicker1.Value.AddDays(1);
        }

        // Lấy danh sách loại phòng đã chọn
        private List<string> GetSelectedRoomTypes()
        {
            List<string> selectedTypes = new List<string>();
            foreach (var item in clbRoomType.CheckedItems)
            {
                selectedTypes.Add(item.ToString());
            }
            return selectedTypes;
        }
        
        //Thay đổi filter Ngày
        private async void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            var selectedStartDate = dateTimePicker1.Value;
            var selectedEndDate = dateTimePicker2.Value;
            rooms = await SetAvaiable.SetAvailable(rooms,selectedStartDate,selectedEndDate.AddDays(1));
            // Reset trạng thái chọn phòng
            foreach (var room in rooms)
            {
                room.isSelected = false;
                room.BackColor = Color.White; // hoặc màu mặc định của bạn
            }
            LoadRoomCards(rooms);
        }
        //Thay đổi filter Ngày
        private async void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            var selectedStartDate = dateTimePicker1.Value;
            var selectedEndDate = dateTimePicker2.Value;
            rooms = await SetAvaiable.SetAvailable(rooms, selectedStartDate, selectedEndDate);
            // Reset trạng thái chọn phòng
            foreach (var room in rooms)
            {
                room.isSelected = false;
                room.BackColor = Color.White; // hoặc màu mặc định của bạn
            }
            LoadRoomCards(rooms);
        }

        private void btnBooking_Click(object sender, EventArgs e)
        {
            BookingForm bookingForm = new BookingForm();
            bookingForm.CheckinTime = new DateTime(dateTimePicker1.Value.Year, dateTimePicker1.Value.Month, dateTimePicker1.Value.Day, 14, 0, 0);
            bookingForm.CheckoutTime = new DateTime(dateTimePicker2.Value.Year, dateTimePicker2.Value.Month, dateTimePicker2.Value.Day, 12, 0, 0);
            bookingForm.RoomIDs = new List<int>();
            foreach (var room in rooms)
            {
                if (room.isSelected)
                {
                    bookingForm.RoomIDs.Add(room.RoomID);
                }
            }
            bookingForm.ShowDialog();

        }

        private void clbRoomType_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<string> selectedRoomTypes = GetSelectedRoomTypes();

            rooms = FilterRoomTypeBLL.FilterByRoomType(rooms, selectedRoomTypes);
        }

        private void btnCheckin_Click(object sender, EventArgs e)
        {
            BookingList listBooking = new BookingList();
            listBooking.ShowDialog();

        }

        private void btnCheckout_Click(object sender, EventArgs e)
        {
            CheckinList listCheckin = new CheckinList();
            listCheckin.ShowDialog();
        }
    }
}
