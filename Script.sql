BEGIN TRANSACTION;
CREATE TABLE IF NOT EXISTS "Booking" (
	"BookingID"	INTEGER,
	"GuestID"	INTEGER,
	"FullName"	TEXT,
	"TotalPrice"	REAL,
	"Checkin"	TEXT,
	"Checkout"	TEXT,
	PRIMARY KEY("BookingID" AUTOINCREMENT),
	FOREIGN KEY("GuestID") REFERENCES "Guest"("GuestID")
);
CREATE TABLE IF NOT EXISTS "BookingRoom" (
	"BookingID"	INTEGER,
	"RoomID"	INTEGER,
	FOREIGN KEY("BookingID") REFERENCES "Booking"("BookingID"),
	FOREIGN KEY("RoomID") REFERENCES "RoomInfo"("RoomID")
);
CREATE TABLE IF NOT EXISTS "Guest" (
	"GuestID"	INTEGER,
	"FullName"	TEXT,
	"PhoneNumber"	TEXT,
	"Email"	TEXT,
	"GuestPrivateInf"	TEXT,
	PRIMARY KEY("GuestID" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "RoomInfo" (
	"RoomID"	INTEGER,
	"RoomType"	TEXT,
	"Capacity"	INTEGER,
	"Description"	TEXT,
	PRIMARY KEY("RoomID" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "RoomInvoice" (
	"RoomInvoiceID"	INTEGER,
	"BookingID"	INTEGER,
	"RoomID"	INTEGER,
	"RoomPrice"	REAL,
	"Datetime"	TEXT,
	PRIMARY KEY("RoomInvoiceID" AUTOINCREMENT),
	FOREIGN KEY("BookingID") REFERENCES "Booking"("BookingID"),
	FOREIGN KEY("RoomID") REFERENCES "RoomInfo"("RoomID")
);
CREATE TABLE IF NOT EXISTS "RoomPrice" (
	"RoomPriceID"	INTEGER,
	"RoomType"	TEXT,
	"Price"	REAL,
	"StartDate"	TEXT,
	"EndDate"	TEXT,
	PRIMARY KEY("RoomPriceID" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "ServiceInfo" (
	"ServiceID"	INTEGER,
	"ServiceName"	TEXT,
	"Descrip"	TEXT,
	PRIMARY KEY("ServiceID" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "ServiceInvoice" (
	"ServiceInvoiceID"	INTEGER,
	"ServicePrice"	REAL,
	"Service"	INTEGER,
	"Datetime"	TEXT,
	"GuestID"	INTEGER,
	"BookingID"	INTEGER,
	PRIMARY KEY("ServiceInvoiceID" AUTOINCREMENT),
	FOREIGN KEY("BookingID") REFERENCES "Booking"("BookingID"),
	FOREIGN KEY("GuestID") REFERENCES "Guest"("GuestID"),
	FOREIGN KEY("Service") REFERENCES "ServiceInfo"("ServiceID")
);
CREATE TABLE IF NOT EXISTS "ServicePrice" (
	"ServicePriceID"	INTEGER,
	"ServiceID"	INTEGER,
	"ServicePrice"	REAL,
	"StartDate"	TEXT,
	"EndDate"	TEXT,
	PRIMARY KEY("ServicePriceID" AUTOINCREMENT),
	FOREIGN KEY("ServiceID") REFERENCES "ServiceInfo"("ServiceID")
);
CREATE TABLE IF NOT EXISTS "StayPeriod" (
	"BookingID"	INTEGER,
	"GuestID"	INTEGER,
	"CheckinActual"	TEXT,
	"CheckoutActual"	TEXT,
	FOREIGN KEY("BookingID") REFERENCES "Booking"("BookingID"),
	FOREIGN KEY("GuestID") REFERENCES "Guest"("GuestID")
);
CREATE TABLE IF NOT EXISTS "User" (
	"Name"	TEXT,
	"Pass"	TEXT,
	"Role"	TEXT,
	PRIMARY KEY("Name")
);
INSERT INTO "Booking" VALUES (1,1,'Nguyen Van A',1500000.0,'2025-05-01','2025-05-03');
INSERT INTO "Booking" VALUES (2,2,'Tran Thi B',2400000.0,'2025-06-10','2025-06-12');
INSERT INTO "Booking" VALUES (3,3,'Le Van C',1500000.0,'2025-07-15','2025-07-17');
INSERT INTO "Booking" VALUES (4,4,'Pham Thi D',3600000.0,'2025-08-20','2025-08-23');
INSERT INTO "Booking" VALUES (5,5,'Hoang Van E',4000000.0,'2025-09-01','2025-09-04');
INSERT INTO "BookingRoom" VALUES (1,1);
INSERT INTO "BookingRoom" VALUES (2,2);
INSERT INTO "BookingRoom" VALUES (3,3);
INSERT INTO "BookingRoom" VALUES (4,4);
INSERT INTO "BookingRoom" VALUES (5,5);
INSERT INTO "Guest" VALUES (1,'Nguyen Van A','0901234567','a.nguyen@example.com','CMND: 123456789');
INSERT INTO "Guest" VALUES (2,'Tran Thi B','0912345678','b.tran@example.com','Passport: B1234567');
INSERT INTO "Guest" VALUES (3,'Le Van C','0923456789','c.le@example.com','CCCD: 012345678901');
INSERT INTO "Guest" VALUES (4,'Pham Thi D','0934567890','d.pham@example.com','CMND: 987654321');
INSERT INTO "Guest" VALUES (5,'Hoang Van E','0945678901','e.hoang@example.com','Passport: C9876543');
INSERT INTO "RoomInfo" VALUES (1,'Standard',2,'Phòng tiêu chuẩn cho 2 người');
INSERT INTO "RoomInfo" VALUES (2,'Deluxe',4,'Phòng cao cấp với ban công');
INSERT INTO "RoomInfo" VALUES (3,'Suite',6,'Phòng suite có phòng khách');
INSERT INTO "RoomInfo" VALUES (4,'Family',5,'Phòng gia đình rộng rãi');
INSERT INTO "RoomInfo" VALUES (5,'VIP',2,'Phòng VIP view biển');
INSERT INTO "RoomInvoice" VALUES (1,1,1,500000.0,'2025-05-01');
INSERT INTO "RoomInvoice" VALUES (2,2,2,800000.0,'2025-06-10');
INSERT INTO "RoomInvoice" VALUES (3,3,3,1500000.0,'2025-07-15');
INSERT INTO "RoomInvoice" VALUES (4,4,4,1200000.0,'2025-08-20');
INSERT INTO "RoomInvoice" VALUES (5,5,5,2000000.0,'2025-09-01');
INSERT INTO "RoomPrice" VALUES (1,'Standard',500000.0,'2025-01-01','2025-12-31');
INSERT INTO "RoomPrice" VALUES (2,'Deluxe',800000.0,'2025-01-01','2025-12-31');
INSERT INTO "RoomPrice" VALUES (3,'Suite',1500000.0,'2025-01-01','2025-12-31');
INSERT INTO "RoomPrice" VALUES (4,'Family',1200000.0,'2025-01-01','2025-12-31');
INSERT INTO "RoomPrice" VALUES (5,'VIP',2000000.0,'2025-01-01','2025-12-31');
INSERT INTO "ServiceInfo" VALUES (1,'Spa','Dịch vụ spa thư giãn');
INSERT INTO "ServiceInfo" VALUES (2,'Gym','Phòng tập gym hiện đại');
INSERT INTO "ServiceInfo" VALUES (3,'BBQ','Tiệc BBQ ngoài trời');
INSERT INTO "ServiceInfo" VALUES (4,'Karaoke','Phòng karaoke chất lượng cao');
INSERT INTO "ServiceInfo" VALUES (5,'Bể bơi','Bể bơi ngoài trời');
INSERT INTO "ServiceInvoice" VALUES (1,300000.0,1,'2025-05-02',1,1);
INSERT INTO "ServiceInvoice" VALUES (2,150000.0,2,'2025-06-11',2,2);
INSERT INTO "ServiceInvoice" VALUES (3,500000.0,3,'2025-07-16',3,3);
INSERT INTO "ServiceInvoice" VALUES (4,200000.0,4,'2025-08-21',4,4);
INSERT INTO "ServiceInvoice" VALUES (5,100000.0,5,'2025-09-02',5,5);
INSERT INTO "ServicePrice" VALUES (1,1,300000.0,'2025-01-01','2025-12-31');
INSERT INTO "ServicePrice" VALUES (2,2,150000.0,'2025-01-01','2025-12-31');
INSERT INTO "ServicePrice" VALUES (3,3,500000.0,'2025-01-01','2025-12-31');
INSERT INTO "ServicePrice" VALUES (4,4,200000.0,'2025-01-01','2025-12-31');
INSERT INTO "ServicePrice" VALUES (5,5,100000.0,'2025-01-01','2025-12-31');
INSERT INTO "StayPeriod" VALUES (1,1,'2025-05-01 14:00','2025-05-03 12:00');
INSERT INTO "StayPeriod" VALUES (2,2,'2025-06-10 15:00','2025-06-12 11:00');
INSERT INTO "StayPeriod" VALUES (3,3,'2025-07-15 13:00','2025-07-17 10:00');
INSERT INTO "StayPeriod" VALUES (4,4,'2025-08-20 16:00','2025-08-23 12:00');
INSERT INTO "StayPeriod" VALUES (5,5,'2025-09-01 14:30','2025-09-04 11:30');
INSERT INTO "User" VALUES ('admin','admin123','Admin');
INSERT INTO "User" VALUES ('reception1','rec123','Receptionist');
INSERT INTO "User" VALUES ('manager','mgr123','Manager');
INSERT INTO "User" VALUES ('staff1','staff123','Staff');
INSERT INTO "User" VALUES ('guest','guest123','Guest');
COMMIT;
