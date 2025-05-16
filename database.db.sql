BEGIN TRANSACTION;
CREATE TABLE IF NOT EXISTS Booking (
    BookingID INTEGER PRIMARY KEY AUTOINCREMENT,
    GuestID INTEGER,
    FullName TEXT,
    TotalPrice REAL,
    Checkin TEXT,
    Checkout TEXT,
    FOREIGN KEY (GuestID) REFERENCES Guest(GuestID)
);
CREATE TABLE IF NOT EXISTS BookingRoom (
    BookingRoomID INTEGER PRIMARY KEY AUTOINCREMENT,
    BookingID INTEGER,
    RoomID INTEGER,
    FOREIGN KEY (BookingID) REFERENCES Booking(BookingID),
    FOREIGN KEY (RoomID) REFERENCES RoomInfo(RoomID)
);
CREATE TABLE IF NOT EXISTS Guest (
    GuestID INTEGER PRIMARY KEY AUTOINCREMENT,
    FullName TEXT NOT NULL,
    PhoneNumber TEXT,
    Email TEXT,
    GuestPrivateInf TEXT
);
CREATE TABLE IF NOT EXISTS RoomInfo (
    RoomID INTEGER PRIMARY KEY AUTOINCREMENT,
    RoomType TEXT NOT NULL,
    Capacity INTEGER,
    Description TEXT
);
CREATE TABLE IF NOT EXISTS RoomInvoice (
    RoomInvoiceID INTEGER PRIMARY KEY AUTOINCREMENT,
    BookingID INTEGER,
    RoomPriceTotal REAL,
    Datetime TEXT,
    FOREIGN KEY (BookingID) REFERENCES Booking(BookingID)
);
CREATE TABLE IF NOT EXISTS RoomPrice (
    PriceID INTEGER PRIMARY KEY AUTOINCREMENT,
    RoomID INTEGER,
    Price REAL,
    StartDate TEXT,
    EndDate TEXT,
    FOREIGN KEY (RoomID) REFERENCES RoomInfo(RoomID)
);
CREATE TABLE IF NOT EXISTS ServiceInfo (
    ServiceID INTEGER PRIMARY KEY AUTOINCREMENT,
    ServiceName TEXT,
    Descrip TEXT
);
CREATE TABLE IF NOT EXISTS ServiceInvoice (
    ServiceInvoiceID INTEGER PRIMARY KEY AUTOINCREMENT,
    ServicePrice REAL,
    Service INTEGER,
    Datetime TEXT,
    GuestID INTEGER,
    BookingID INTEGER,
    FOREIGN KEY (Service) REFERENCES ServiceInfo(ServiceID),
    FOREIGN KEY (GuestID) REFERENCES Guest(GuestID),
    FOREIGN KEY (BookingID) REFERENCES Booking(BookingID)
);
CREATE TABLE IF NOT EXISTS ServicePrice (
    ServicePriceID INTEGER PRIMARY KEY AUTOINCREMENT,
    ServiceID INTEGER,
    ServicePrice REAL,
    StartDate TEXT,
    EndDate TEXT,
    FOREIGN KEY (ServiceID) REFERENCES ServiceInfo(ServiceID)
);
CREATE TABLE IF NOT EXISTS StayPeriod (
    StayPeriodID INTEGER PRIMARY KEY AUTOINCREMENT,
    BookingID INTEGER,
    GuestID INTEGER,
    CheckinActual TEXT,
    CheckoutActual TEXT,
    FOREIGN KEY (BookingID) REFERENCES Booking(BookingID),
    FOREIGN KEY (GuestID) REFERENCES Guest(GuestID)
);
CREATE TABLE IF NOT EXISTS "Users" (
	"Username"	TEXT NOT NULL UNIQUE,
	"Password"	TEXT NOT NULL,
	"Role"	TEXT NOT NULL,
	PRIMARY KEY("Username")
);
INSERT INTO "Booking" ("BookingID","GuestID","FullName","TotalPrice","Checkin","Checkout") VALUES (1,1,'Nguyen Van A',500.0,'2025-05-12 14:00:00','2025-05-13 12:00:00'),
 (2,2,'Tran Thi B',1500.0,'2025-05-12 16:00:00','2025-05-14 11:30:00'),
 (3,5,'Hoang Van E',10.0,'2025-05-15 00:00:00','2025-05-15 00:00:00'),
 (4,1,'Nguyen Van A',500.0,'2025-05-12 14:00:00','2025-05-13 12:00:00'),
 (5,2,'Tran Thi B',1500.0,'2025-05-12 16:00:00','2025-05-14 11:30:00'),
 (6,5,'Hoang Van E',10.0,'2025-05-15 00:00:00','2025-05-15 00:00:00');
INSERT INTO "BookingRoom" ("BookingRoomID","BookingID","RoomID") VALUES (1,1,1),
 (2,2,2),
 (3,2,3),
 (4,2,4),
 (5,3,2),
 (6,3,3),
 (7,1,1),
 (8,2,2),
 (9,2,3),
 (10,2,4),
 (11,3,2),
 (12,3,3);
INSERT INTO "Guest" ("GuestID","FullName","PhoneNumber","Email","GuestPrivateInf") VALUES (1,'Nguyen Van A','0901234567','a.nguyen@example.com','CMND: 123456789'),
 (2,'Tran Thi B','0912345678','b.tran@example.com','Passport: B1234567'),
 (3,'Le Van C','0923456789','c.le@example.com','CCCD: 012345678901'),
 (4,'Pham Thi D','0934567890','d.pham@example.com','CMND: 987654321'),
 (5,'Hoang Van E','0945678901','e.hoang@example.com','Passport: C9876543'),
 (6,'Nguyen Van A','0901234567','a.nguyen@example.com','CMND: 123456789'),
 (7,'Tran Thi B','0912345678','b.tran@example.com','Passport: B1234567'),
 (8,'Le Van C','0923456789','c.le@example.com','CCCD: 012345678901'),
 (9,'Pham Thi D','0934567890','d.pham@example.com','CMND: 987654321'),
 (10,'Hoang Van E','0945678901','e.hoang@example.com','Passport: C9876543');
INSERT INTO "RoomInfo" ("RoomID","RoomType","Capacity","Description") VALUES (1,'Single',1,'Phòng đơn, giường đơn, thích hợp cho 1 người'),
 (2,'Single',1,'Phòng đơn có ban công, view đẹp'),
 (3,'Double',2,'Phòng đôi với giường đôi hoặc 2 giường đơn'),
 (4,'Double',2,'Phòng đôi, có bàn làm việc và tivi'),
 (5,'Family',4,'Phòng gia đình rộng rãi, 2 giường đôi'),
 (6,'Family',4,'Phòng gia đình, có bếp nhỏ và sofa'),
 (7,'Single',1,'Phòng đơn, giường đơn, thích hợp cho 1 người'),
 (8,'Single',1,'Phòng đơn có ban công, view đẹp'),
 (9,'Double',2,'Phòng đôi với giường đôi hoặc 2 giường đơn'),
 (10,'Double',2,'Phòng đôi, có bàn làm việc và tivi'),
 (11,'Family',4,'Phòng gia đình rộng rãi, 2 giường đôi'),
 (12,'Family',4,'Phòng gia đình, có bếp nhỏ và sofa');
INSERT INTO "RoomPrice" ("PriceID","RoomID","Price","StartDate","EndDate") VALUES (1,1,500.0,'2025-01-01','2025-12-31'),
 (2,2,600.0,'2025-01-01','2025-12-31'),
 (3,3,800.0,'2025-01-01','2025-12-31'),
 (4,4,850.0,'2025-01-01','2025-12-31'),
 (5,5,1200.0,'2025-01-01','2025-12-31'),
 (6,1,500.0,'2025-01-01','2025-12-31'),
 (7,2,600.0,'2025-01-01','2025-12-31'),
 (8,3,800.0,'2025-01-01','2025-12-31'),
 (9,4,850.0,'2025-01-01','2025-12-31'),
 (10,5,1200.0,'2025-01-01','2025-12-31');
INSERT INTO "Users" ("Username","Password","Role") VALUES ('admin','123456','admin'),
 ('reception1','rec123','Receptionist');
COMMIT;
