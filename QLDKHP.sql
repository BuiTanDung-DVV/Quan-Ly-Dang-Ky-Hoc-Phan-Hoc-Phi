-- XÓA VÀ TẠO LẠI DATABASE
USE master;
IF EXISTS (SELECT name FROM sys.databases WHERE name = 'QLDKHP')
BEGIN
    ALTER DATABASE QLDKHP SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE QLDKHP;
END
CREATE DATABASE QLDKHP;
USE QLDKHP;

-- 1. BẢNG KHOA
CREATE TABLE Departments (
    DeptID INT IDENTITY(1,1) PRIMARY KEY,
    Code VARCHAR(20) UNIQUE NOT NULL,
    Name NVARCHAR(100) NOT NULL,
    Office NVARCHAR(100)
);

-- 2. BẢNG GIẢNG VIÊN
CREATE TABLE Lecturers (
    LecturerID INT IDENTITY(1,1) PRIMARY KEY,
    LecturerCode VARCHAR(20) UNIQUE NOT NULL,
    FullName NVARCHAR(100) NOT NULL,
    Email VARCHAR(100),
    DeptID INT FOREIGN KEY REFERENCES Departments(DeptID)
);

-- 3. BẢNG NGÀNH HỌC
CREATE TABLE Majors (
    MajorID INT IDENTITY(1,1) PRIMARY KEY,
    Code VARCHAR(20) UNIQUE NOT NULL,
    Name NVARCHAR(100) NOT NULL,
    DeptID INT FOREIGN KEY REFERENCES Departments(DeptID)
);

-- 4. BẢNG SINH VIÊN
CREATE TABLE Students (
    StudentID INT IDENTITY(1,1) PRIMARY KEY,
    StudentCode VARCHAR(20) UNIQUE NOT NULL,
    FullName NVARCHAR(100) NOT NULL,
    Gender NVARCHAR(10),
    DateOfBirth DATE,
    Email VARCHAR(100),
    Phone VARCHAR(15),
    Address NVARCHAR(255),
    DeptID INT FOREIGN KEY REFERENCES Departments(DeptID),
    AdmissionYear INT,
    Status NVARCHAR(50) DEFAULT N'Đang học'
);

-- 5. BẢNG KỲ HỌC
CREATE TABLE AcademicTerms (
    TermID INT IDENTITY(1,1) PRIMARY KEY,
    Code VARCHAR(20) UNIQUE NOT NULL,
    Name NVARCHAR(100) NOT NULL,
    StartDate DATE NOT NULL,
    EndDate DATE NOT NULL,
    IsCurrent BIT DEFAULT 0
);

-- 6. BẢNG MÔN HỌC
CREATE TABLE Courses (
    CourseID INT IDENTITY(1,1) PRIMARY KEY,
    Code VARCHAR(20) UNIQUE NOT NULL,
    Name NVARCHAR(100) NOT NULL,
    Credits INT NOT NULL,
    TuitionPerCredit DECIMAL(10,2) NOT NULL,
    DeptID INT FOREIGN KEY REFERENCES Departments(DeptID)
);

-- 7. BẢNG LỚP HỌC PHẦN
CREATE TABLE ClassSections (
    SectionID INT IDENTITY(1,1) PRIMARY KEY,
    SectionCode VARCHAR(20) UNIQUE NOT NULL,
    CourseID INT FOREIGN KEY REFERENCES Courses(CourseID),
    TermID INT FOREIGN KEY REFERENCES AcademicTerms(TermID),
    LecturerID INT FOREIGN KEY REFERENCES Lecturers(LecturerID),
    Schedule NVARCHAR(100),
    Room NVARCHAR(50),
    MaxStudents INT DEFAULT 60
);

-- 8. BẢNG ĐĂNG KÝ HỌC
CREATE TABLE Enrollments (
    EnrollmentID INT IDENTITY(1,1) PRIMARY KEY,
    StudentID INT FOREIGN KEY REFERENCES Students(StudentID),
    SectionID INT FOREIGN KEY REFERENCES ClassSections(SectionID),
    RegisterDate DATE DEFAULT GETDATE(),
    Status NVARCHAR(50) DEFAULT N'Đang học'
);

-- 9. BẢNG HÓA ĐƠN HỌC PHÍ (CẬP NHẬT)
CREATE TABLE Invoices (
    InvoiceID INT IDENTITY(1,1) PRIMARY KEY,
    StudentID INT FOREIGN KEY REFERENCES Students(StudentID),
    TermID INT FOREIGN KEY REFERENCES AcademicTerms(TermID),
    TotalAmount DECIMAL(12,2) NOT NULL DEFAULT 0,
    CreatedDate DATETIME DEFAULT GETDATE(),
    DueDate DATETIME NULL,
    Status NVARCHAR(50) DEFAULT N'Chưa thanh toán',
    IsPaid BIT DEFAULT 0
);

-- 10. BẢNG CHI TIẾT HÓA ĐƠN
CREATE TABLE InvoiceDetails (
    InvoiceDetailID INT IDENTITY(1,1) PRIMARY KEY,
    InvoiceID INT FOREIGN KEY REFERENCES Invoices(InvoiceID),
    SectionID INT FOREIGN KEY REFERENCES ClassSections(SectionID),
    Amount DECIMAL(12,2) NOT NULL
);

-- 11. BẢNG THANH TOÁN
CREATE TABLE Payments (
    PaymentID INT IDENTITY(1,1) PRIMARY KEY,
    InvoiceID INT FOREIGN KEY REFERENCES Invoices(InvoiceID),
    PaymentDate DATETIME DEFAULT GETDATE(),
    AmountPaid DECIMAL(12,2) NOT NULL,
    Method NVARCHAR(50),
    Note NVARCHAR(255)
);

-- 12. BẢNG NGƯỜI DÙNG HỆ THỐNG
CREATE TABLE Users (
    UserID INT IDENTITY(1,1) PRIMARY KEY,
    Username VARCHAR(50) UNIQUE NOT NULL,
    PasswordHash VARCHAR(255) NOT NULL,
    Role NVARCHAR(50),
    LinkedStudentID INT NULL FOREIGN KEY REFERENCES Students(StudentID),
    LinkedLecturerID INT NULL FOREIGN KEY REFERENCES Lecturers(LecturerID)
);

-- 13. BẢNG LOG THAY ĐỔI HÓA ĐƠN
CREATE TABLE InvoiceChangeLogs (
    LogID INT IDENTITY(1,1) PRIMARY KEY,
    StudentID INT,
    TermID INT,
    OldAmount DECIMAL(12,2),
    NewAmount DECIMAL(12,2),
    Action NVARCHAR(50),
    ChangeDate DATETIME DEFAULT GETDATE(),
    ChangedBy NVARCHAR(100)
);

-- THÊM CONSTRAINT
ALTER TABLE ClassSections 
ADD CONSTRAINT UK_ClassSections_Schedule_Room_Term 
UNIQUE (TermID, Schedule, Room);


-- XÓA TẤT CẢ DỮ LIỆU
-- Tắt ràng buộc khóa ngoại tạm thời
EXEC sp_msforeachtable 'ALTER TABLE ? NOCHECK CONSTRAINT ALL';
DELETE FROM Payments;
DELETE FROM InvoiceDetails;
DELETE FROM Enrollments;
DELETE FROM Users;
DELETE FROM InvoiceChangeLogs;
DELETE FROM Invoices;
DELETE FROM ClassSections;
DELETE FROM Students;
DELETE FROM Lecturers;
DELETE FROM Courses;
DELETE FROM Majors;
DELETE FROM AcademicTerms;
DELETE FROM Departments;
EXEC sp_msforeachtable 'DBCC CHECKIDENT(''?'', RESEED, 0)';
-- Bật lại ràng buộc khóa ngoại
EXEC sp_msforeachtable 'ALTER TABLE ? WITH CHECK CHECK CONSTRAINT ALL';

-- Xóa cursor cũ nếu tồn tại (chạy trước khi tạo trigger)
IF CURSOR_STATUS('global', 'enrollment_cursor') >= 0
BEGIN
    CLOSE enrollment_cursor;
    DEALLOCATE enrollment_cursor;
END


-- CHÈN DỮ LIỆU MẪU 
INSERT INTO Departments (Code, Name, Office) VALUES 
('CNTT', N'Công nghệ thông tin', N'Tòa A - Phòng 101'),
('QTKD', N'Quản trị kinh doanh', N'Tòa B - Phòng 101'),
('KT', N'Kế toán', N'Tòa B - Phòng 201'),
('NNA', N'Ngôn ngữ Anh', N'Tòa C - Phòng 101'),
('TODHH', N'Toán ứng dụng', N'Tòa D - Phòng 101'),
('ATTT', N'An toàn thông tin', N'Tòa A - Phòng 201'),
('TCNH', N'Tài chính ngân hàng', N'Tòa B - Phòng 301'),
('TQ', N'Tiếng Trung', N'Tòa C - Phòng 201'),
('VL', N'Vật lý', N'Tòa D - Phòng 201'),
('H', N'Hóa học', N'Tòa D - Phòng 301'),
('S', N'Sinh học', N'Tòa D - Phòng 401'),
('DS', N'Dược sĩ', N'Tòa E - Phòng 101'),
('YL', N'Y học lâm sàng', N'Tòa E - Phòng 201'),
('L', N'Luật', N'Tòa F - Phòng 101'),
('SP', N'Sư phạm', N'Tòa F - Phòng 201'),
('DLKH', N'Du lịch khách sạn', N'Tòa G - Phòng 101'),
('TMDT', N'Thương mại điện tử', N'Tòa A - Phòng 301'),
('MKT', N'Marketing', N'Tòa B - Phòng 401'),
('NNPT', N'Ngôn ngữ Pháp', N'Tòa C - Phòng 301'),
('QTCL', N'Quản trị chất lượng', N'Tòa G - Phòng 201'),
('QLNN', N'Quản lý nhà nước', N'Tòa F - Phòng 301'),
('QTDN', N'Quản trị doanh nghiệp', N'Tòa B - Phòng 501'),
('BDQT', N'Bất động sản quốc tế', N'Tòa G - Phòng 301'),
('CNTT2', N'Công nghệ thông tin 2', N'Tòa A - Phòng 401'),
('QTKD2', N'Quản trị kinh doanh 2', N'Tòa B - Phòng 601'),
('NNA2', N'Ngôn ngữ Anh 2', N'Tòa C - Phòng 401'),
('KDL', N'Kinh doanh quốc tế', N'Tòa H - Phòng 101'),
('XDDD', N'Xây dựng dân dụng', N'Tòa I - Phòng 101'),
('DT', N'Điện tử', N'Tòa J - Phòng 101'),
('TTTV', N'Thư viện', N'Tòa K - Phòng 101'),
('SPTD', N'Sư phạm thể dục', N'Tòa L - Phòng 101'),
('NHKS', N'Nhà hàng khách sạn', N'Tòa M - Phòng 101'),
('KTXH', N'Kinh tế xã hội', N'Tòa N - Phòng 101');

INSERT INTO Majors (Code, Name, DeptID) VALUES
('CNTT01', N'Công nghệ thông tin', 1),
('QTKD01', N'Quản trị kinh doanh', 2),
('KT01', N'Kế toán', 3),
('NNA01', N'Ngôn ngữ Anh', 4),
('TODHH01', N'Toán ứng dụng', 5),
('ATTT01', N'An toàn thông tin', 6),
('TCNH01', N'Tài chính ngân hàng', 7),
('TQ01', N'Tiếng Trung', 8),
('DLKH01', N'Du lịch khách sạn', 16),
('MKT01', N'Marketing', 18);

INSERT INTO Lecturers (LecturerCode, FullName, Email, DeptID) VALUES
-- CNTT
('GV001', N'PGS.TS. Nguyễn Văn Hòa', 'hoa.nv@univ.edu.vn', 1),
('GV002', N'TS. Trần Thị Minh', 'minh.tt@univ.edu.vn', 1),
('GV003', N'ThS. Lê Văn Phúc', 'phuc.lv@univ.edu.vn', 1),
-- QTKD
('GV004', N'TS. Phạm Thị Lan', 'lan.pt@univ.edu.vn', 2),
('GV005', N'PGS.TS. Hoàng Văn Đức', 'duc.hv@univ.edu.vn', 2),
-- Kế toán
('GV006', N'ThS. Võ Thị Mai', 'mai.vt@univ.edu.vn', 3),
-- Ngoại ngữ
('GV007', N'TS. Lê Minh Tâm', 'tam.lm@univ.edu.vn', 4),
('GV008', N'ThS. Sarah Johnson', 'sarah.j@univ.edu.vn', 4),
('GV009', N'ThS. Lý Hồng Lan', 'lan.lh@univ.edu.vn', 8),
-- Toán
('GV010', N'GS.TS. Đỗ Minh Đức', 'duc.dm@univ.edu.vn', 5),
('GV011', N'TS. Bùi Thị Hương', 'huong.bt@univ.edu.vn', 5),
-- ATTT
('GV012', N'TS. Trần Văn Dũng', 'dung.tv@univ.edu.vn', 6),
-- Y Dược
('GV013', N'PGS.TS. Nguyễn Quốc Anh', 'anh.nq@univ.edu.vn', 12),
('GV014', N'BS.CKI. Trần Văn Sơn', 'son.tv@univ.edu.vn', 13),
-- Khác
('GV015', N'ThS. Phan Thị Hoa', 'hoa.pt@univ.edu.vn', 16),
('GV016', N'TS. Nguyễn Thu Hà', 'ha.nt@univ.edu.vn', 7),
('GV017', N'ThS. Tạ Minh Tùng', 'tung.ta@univ.edu.vn', 14),
('GV018', N'TS. Ngô Đức Vinh', 'vinh.ngo@univ.edu.vn', 9),
('GV019', N'ThS. Hoàng Hồng Đăng', 'dang.hoang@univ.edu.vn', 10),
('GV020', N'TS. Nguyễn Đình Tài', 'tai.nguyen@univ.edu.vn', 11),
('GV021', N'ThS. Lý Thùy Dương', 'duong.ly@univ.edu.vn', 15),
('GV022', N'TS. Vũ Quang Huy', 'huy.vu@univ.edu.vn', 17),
('GV023', N'ThS. Lê Thị Thảo', 'thao.le@univ.edu.vn', 18),
('GV024', N'TS. Phạm Hồng Sơn', 'son.pham@univ.edu.vn', 19),
('GV025', N'ThS. Huỳnh Văn Minh', 'minh.huynh@univ.edu.vn', 20),
('GV026', N'TS. Đặng Thị Hoa', 'hoa.dang@univ.edu.vn', 21),
('GV027', N'ThS. Bùi Văn Thắng', 'thang.bui@univ.edu.vn', 22),
('GV028', N'TS. Ngô Văn Đồng', 'dong.ngo@univ.edu.vn', 23),
('GV029', N'ThS. Trịnh Đức Anh', 'anh.trinh@univ.edu.vn', 24),
('GV030', N'TS. Phạm Thùy My', 'my.pham@univ.edu.vn', 25),
('GV031', N'ThS. Dương Quang Đạt', 'dat.duong@univ.edu.vn', 26),
('GV032', N'TS. Nguyễn Thu Trang', 'trang.nguyen@univ.edu.vn', 27),
('GV033', N'ThS. Lê Minh Phát', 'phat.le@univ.edu.vn', 28);

INSERT INTO Students (StudentCode, FullName, Gender, DateOfBirth, Email, Phone, Address, DeptID, AdmissionYear) VALUES
-- Sinh viên năm 4 (2021)
('21CNTT001', N'Nguyễn Thị Mai', N'Nữ', '2003-04-12', '21cntt001@stu.edu.vn', '0901234567', N'Hà Nội', 1, 2021),
('21CNTT002', N'Trần Văn Hùng', N'Nam', '2003-08-25', '21cntt002@stu.edu.vn', '0907654321', N'Hải Phòng', 1, 2021),
('21QTKD001', N'Phạm Minh Đức', N'Nam', '2003-01-15', '21qtkd001@stu.edu.vn', '0982000001', N'TP.HCM', 2, 2021),
('21NNA001', N'Lê Ngọc Anh', N'Nữ', '2003-03-20', '21nna001@stu.edu.vn', '0989123456', N'Đà Nẵng', 4, 2021),
('21KT001', N'Vũ Thị Linh', N'Nữ', '2003-06-30', '21kt001@stu.edu.vn', '0982000002', N'Huế', 3, 2021),
-- Sinh viên năm 3 (2022)
('22CNTT001', N'Hoàng Thị Lan', N'Nữ', '2004-02-14', '22cntt001@stu.edu.vn', '0982000005', N'Hà Nội', 1, 2022),
('22CNTT002', N'Đỗ Văn Minh', N'Nam', '2004-07-22', '22cntt002@stu.edu.vn', '0982000006', N'Hải Phòng', 1, 2022),
('22ATTT001', N'Bùi Thị Hương', N'Nữ', '2004-05-18', '22attt001@stu.edu.vn', '0982000007', N'Đà Nẵng', 6, 2022),
('22QTKD001', N'Lê Văn Tùng', N'Nam', '2004-03-15', '22qtkd001@stu.edu.vn', '0982000008', N'Cần Thơ', 2, 2022),
('22NNA001', N'Phạm Thị Nga', N'Nữ', '2004-08-12', '22nna001@stu.edu.vn', '0982000009', N'Vinh', 4, 2022),
-- Sinh viên năm 2 (2023)
('23CNTT001', N'Trần Quốc Khánh', N'Nam', '2005-01-10', '23cntt001@stu.edu.vn', '0982000010', N'Hà Nội', 1, 2023),
('23CNTT002', N'Nguyễn Thị Thu', N'Nữ', '2005-04-25', '23cntt002@stu.edu.vn', '0982000011', N'Nam Định', 1, 2023),
('23QTKD001', N'Lý Văn Nam', N'Nam', '2005-06-08', '23qtkd001@stu.edu.vn', '0982000012', N'Thanh Hóa', 2, 2023),
('23DS001', N'Võ Minh Tuấn', N'Nam', '2005-09-18', '23ds001@stu.edu.vn', '0982000013', N'Nghệ An', 12, 2023),
('23TODHH001', N'Lưu Thị Hoa', N'Nữ', '2005-11-20', '23todhh001@stu.edu.vn', '0982000014', N'Quảng Bình', 5, 2023),
-- Sinh viên năm 1 (2024)
('24CNTT001', N'Đặng Văn Long', N'Nam', '2006-02-28', '24cntt001@stu.edu.vn', '0982000015', N'Hà Nội', 1, 2024),
('24CNTT002', N'Phan Thị Nhung', N'Nữ', '2006-07-14', '24cntt002@stu.edu.vn', '0982000016', N'Đà Nẵng', 1, 2024),
('24QTKD001', N'Ngô Minh Quân', N'Nam', '2006-05-30', '24qtkd001@stu.edu.vn', '0982000017', N'TP.HCM', 2, 2024),
('24NNA001', N'Trần Thị Bích', N'Nữ', '2006-10-12', '24nna001@stu.edu.vn', '0982000018', N'Hải Phòng', 4, 2024),
('24DS001', N'Lê Văn Phong', N'Nam', '2006-12-01', '24ds001@stu.edu.vn', '0982000019', N'Cần Thơ', 12, 2024),
-- Thêm một số sinh viên khác
('22VL001', N'Hoàng Văn Phúc', N'Nam', '2004-07-16', '22vl001@stu.edu.vn', '0982000020', N'Nghệ An', 9, 2022),
('23H001', N'Phan Thị Thu', N'Nữ', '2005-05-23', '23h001@stu.edu.vn', '0982000021', N'Thanh Hóa', 10, 2023),
('21YL001', N'Lưu Kiều Trang', N'Nữ', '2003-10-11', '21yl001@stu.edu.vn', '0982000022', N'Hà Tĩnh', 13, 2021),
('23L001', N'Lê Quang Vinh', N'Nam', '2005-08-17', '23l001@stu.edu.vn', '0982000023', N'Quảng Trị', 14, 2023),
('22SP001', N'Đặng Văn Bảo', N'Nam', '2004-03-09', '22sp001@stu.edu.vn', '0982000024', N'Bình Thuận', 15, 2022),
('21DLKH001', N'Nguyễn Thị Kim', N'Nữ', '2003-10-21', '21dlkh001@stu.edu.vn', '0982000025', N'Bến Tre', 16, 2021),
('23TMDT001', N'Doãn Xuân Phú', N'Nam', '2005-12-14', '23tmdt001@stu.edu.vn', '0982000026', N'Bạc Liêu', 17, 2023),
('22QTCL001', N'Lý Xuân Hạnh', N'Nữ', '2004-06-22', '22qtcl001@stu.edu.vn', '0982000027', N'An Giang', 20, 2022),
('21QLNN001', N'Vũ Tiến Đạt', N'Nam', '2003-07-10', '21qlnn001@stu.edu.vn', '0982000028', N'Tây Ninh', 21, 2021),
('24TCNH001', N'Phạm Kiều Oanh', N'Nữ', '2006-02-12', '24tcnh001@stu.edu.vn', '0982000029', N'Đồng Tháp', 7, 2024),
('23MKT001', N'Ngô Tấn Minh', N'Nam', '2005-11-01', '23mkt001@stu.edu.vn', '0982000030', N'Quảng Nam', 18, 2023),
('22TQ001', N'Lê Thị Bích Nga', N'Nữ', '2004-05-30', '22tq001@stu.edu.vn', '0982000031', N'Sóc Trăng', 8, 2022),
('24NNPT001', N'Trần Văn Hoàng', N'Nam', '2006-08-20', '24nnpt001@stu.edu.vn', '0982000032', N'Hồ Chí Minh', 19, 2024);

INSERT INTO AcademicTerms (Code, Name, StartDate, EndDate, IsCurrent) VALUES
-- Năm học 2022-2023
('HK221', N'Học kỳ 1 - Năm học 2022-2023', '2022-09-01', '2023-01-20', 0),
('HK222', N'Học kỳ 2 - Năm học 2022-2023', '2023-02-15', '2023-06-30', 0),
-- Năm học 2023-2024
('HK231', N'Học kỳ 1 - Năm học 2023-2024', '2023-09-01', '2024-01-20', 0),
('HK232', N'Học kỳ 2 - Năm học 2023-2024', '2024-02-15', '2024-06-30', 0),
('HK233', N'Học kỳ hè - Năm học 2023-2024', '2024-07-01', '2024-08-30', 0),
-- Năm học 2024-2025 (hiện tại)
('HK241', N'Học kỳ 1 - Năm học 2024-2025', '2024-09-01', '2025-01-20', 1),
('HK242', N'Học kỳ 2 - Năm học 2024-2025', '2025-02-15', '2025-06-30', 0),
-- Tương lai
('HK251', N'Học kỳ 1 - Năm học 2025-2026', '2025-09-01', '2026-01-20', 0),
-- Thêm các kỳ khác để đủ ID
('HK301', N'Học kỳ 1 - Năm học 2030-2031', '2030-09-01', '2031-01-15', 0),
('HK302', N'Học kỳ 2 - Năm học 2030-2031', '2031-02-15', '2031-06-30', 0),
('HK303', N'Học kỳ phụ 2030-2031', '2031-07-01', '2031-08-15', 0),
('HK311', N'Học kỳ 1 - Năm học 2031-2032', '2031-09-01', '2032-01-15', 0),
('HK312', N'Học kỳ 2 - Năm học 2031-2032', '2032-02-15', '2032-06-30', 0),
('HK313', N'Học kỳ phụ 2031-2032', '2032-07-01', '2032-08-15', 0),
('HK321', N'Học kỳ 1 - Năm học 2032-2033', '2032-09-01', '2033-01-15', 0),
('HK322', N'Học kỳ 2 - Năm học 2032-2033', '2033-02-15', '2033-06-30', 0),
('HK323', N'Học kỳ phụ 2032-2033', '2033-07-01', '2033-08-15', 0),
('HK331', N'Học kỳ 1 - Năm học 2033-2034', '2033-09-01', '2034-01-15', 0),
('HK243', N'Học kỳ phụ 2024-2025', '2025-07-01', '2025-08-15', 0),
('HK252', N'Học kỳ 2 - Năm học 2025-2026', '2026-02-15', '2026-06-30', 0),
('HK253', N'Học kỳ phụ 2025-2026', '2026-07-01', '2026-08-15', 0),
('HK261', N'Học kỳ 1 - Năm học 2026-2027', '2026-09-01', '2027-01-15', 0),
('HK262', N'Học kỳ 2 - Năm học 2026-2027', '2027-02-15', '2027-06-30', 0),
('HK263', N'Học kỳ phụ 2026-2027', '2027-07-01', '2027-08-15', 0),
('HK271', N'Học kỳ 1 - Năm học 2027-2028', '2027-09-01', '2028-01-15', 0),
('HK272', N'Học kỳ 2 - Năm học 2027-2028', '2028-02-15', '2028-06-30', 0),
('HK273', N'Học kỳ phụ 2027-2028', '2028-07-01', '2028-08-15', 0),
('HK281', N'Học kỳ 1 - Năm học 2028-2029', '2028-09-01', '2029-01-15', 0),
('HK282', N'Học kỳ 2 - Năm học 2028-2029', '2029-02-15', '2029-06-30', 0),
('HK283', N'Học kỳ phụ 2028-2029', '2029-07-01', '2029-08-15', 0),
('HK291', N'Học kỳ 1 - Năm học 2029-2030', '2029-09-01', '2030-01-15', 0),
('HK292', N'Học kỳ 2 - Năm học 2029-2030', '2030-02-15', '2030-06-30', 0),
('HK293', N'Học kỳ phụ 2029-2030', '2030-07-01', '2030-08-15', 0),
('HK201', N'Học kỳ hè 2020', '2020-06-15', '2020-08-01', 0);

INSERT INTO Courses (Code, Name, Credits, TuitionPerCredit, DeptID) VALUES
-- Môn CNTT
('CT101', N'Lập trình cơ bản', 3, 450000, 1),
('CT102', N'Cấu trúc dữ liệu', 3, 450000, 1),
('CT201', N'Cơ sở dữ liệu', 3, 500000, 1),
('CT202', N'Lập trình web', 3, 500000, 1),
('CT301', N'Công nghệ phần mềm', 3, 550000, 1),
-- Môn ATTT
('AT101', N'Mật mã học cơ bản', 3, 550000, 6),
('AT201', N'An toàn mạng', 3, 600000, 6),
-- Môn QTKD
('QT101', N'Nguyên lý quản trị', 3, 420000, 2),
('QT201', N'Quản trị marketing', 3, 450000, 2),
('QT301', N'Quản trị chiến lược', 3, 480000, 2),
-- Môn Kế toán
('KT101', N'Nguyên lý kế toán', 3, 410000, 3),
('KT201', N'Kế toán tài chính', 3, 430000, 3),
-- Môn Ngoại ngữ
('EN101', N'Tiếng Anh cơ bản 1', 2, 400000, 4),
('EN102', N'Tiếng Anh cơ bản 2', 2, 400000, 4),
('EN201', N'Tiếng Anh giao tiếp', 3, 420000, 4),
('CH101', N'Tiếng Trung cơ bản', 2, 400000, 8),
-- Môn Toán
('MA101', N'Toán cao cấp A1', 3, 350000, 5),
('MA102', N'Toán cao cấp A2', 3, 350000, 5),
('MA201', N'Xác suất thống kê', 3, 380000, 5),
-- Môn Y Dược
('DS101', N'Hóa dược', 4, 600000, 12),
('DS201', N'Dược lý học', 4, 650000, 12),
('YL101', N'Giải phẫu học', 4, 700000, 13),
-- Môn đại cương
('PL101', N'Triết học Mác-Lênin', 2, 200000, 15),
('PL102', N'Kinh tế chính trị', 2, 200000, 15),
('PE101', N'Giáo dục thể chất', 1, 150000, 15),
-- Môn khác (để đủ 34 môn)
('TODHH101', N'Đại số tuyến tính', 3, 450000, 5),
('DLKH101', N'Nguyên lý du lịch', 3, 420000, 16),
('KTXH101', N'Kinh tế vi mô', 3, 430000, 33),
('QTCL101', N'Quản lý chất lượng ISO', 3, 480000, 20),
('ATTT101', N'Mật mã học cơ bản', 4, 550000, 6),
('QLNN101', N'Luật hành chính', 3, 400000, 21),
('TCNH101', N'Nguyên lý tài chính', 3, 460000, 7),
('BDQT101', N'Thị trường bất động sản', 3, 500000, 23),
('NNPT101', N'Tiếng Pháp cơ bản', 2, 400000, 19),
('QTDN101', N'Chiến lược kinh doanh', 3, 520000, 22);

INSERT INTO ClassSections (SectionCode, CourseID, TermID, LecturerID, Schedule, Room, MaxStudents) VALUES
-- HK232 (TermID = 2) - Lịch học KHÔNG trùng
('CT101-HK232-L01', 1, 2, 1, N'Thứ 2 - Tiết 1,2,3', N'A101', 60),
('CT101-HK232-L02', 1, 2, 2, N'Thứ 2 - Tiết 4,5,6', N'A102', 60),
('CT201-HK232-L01', 3, 2, 1, N'Thứ 3 - Tiết 1,2,3', N'A103', 60),
('QT101-HK232-L01', 8, 2, 4, N'Thứ 3 - Tiết 4,5,6', N'B101', 60),
('EN101-HK232-L01', 13, 2, 7, N'Thứ 4 - Tiết 1,2', N'C101', 50),
('EN101-HK232-L02', 13, 2, 8, N'Thứ 4 - Tiết 3,4', N'C102', 50),
-- Các kỳ khác
('TODHH101-L01', 25, 3, 10, N'Thứ 5 - Tiết 1,2,3', N'D101', 60),
('DLKH101-L01', 26, 4, 15, N'Thứ 5 - Tiết 4,5,6', N'G101', 55),
('KTXH101-L01', 27, 5, 16, N'Thứ 6 - Tiết 1,2,3', N'N101', 50),
('QTCL101-L01', 28, 6, 26, N'Thứ 6 - Tiết 4,5,6', N'G201', 45),
('ATTT101-L01', 29, 7, 12, N'Thứ 2 - Tiết 7,8,9,10', N'A201', 40),
('QLNN101-L01', 30, 8, 26, N'Thứ 3 - Tiết 7,8,9', N'F301', 50),
('TCNH101-L01', 31, 9, 16, N'Thứ 4 - Tiết 7,8,9', N'B301', 55),
('BDQT101-L01', 32, 10, 28, N'Thứ 5 - Tiết 7,8,9', N'G301', 60),
('NNPT101-L01', 33, 11, 24, N'Thứ 6 - Tiết 7,8', N'C301', 45),
('QTDN101-L01', 34, 12, 27, N'Thứ 2 - Tiết 10,11,12', N'B501', 50),
-- Thêm các lớp với lịch khác biệt
('CT102-L01', 2, 1, 2, N'Thứ 3 - Tiết 10,11,12', N'A104', 60),
('DT101-L01', 16, 2, 29, N'Thứ 4 - Tiết 7,8,9,10', N'J101', 60),
('KT102-L01', 17, 3, 6, N'Thứ 5 - Tiết 10,11,12', N'B201', 60),
('SP101-L01', 18, 4, 21, N'Thứ 6 - Tiết 10,11', N'F201', 50),
('VL101-L01', 19, 5, 20, N'Thứ 2 - Tiết 13,14', N'D201', 50),
('H101-L01', 20, 6, 19, N'Thứ 3 - Tiết 13,14,15', N'D301', 60),
('S101-L01', 21, 7, 18, N'Thứ 4 - Tiết 13,14', N'D401', 40),
('DS101-L01', 22, 8, 13, N'Thứ 5 - Tiết 13,14', N'E101', 60),
('YL101-L01', 23, 9, 14, N'Thứ 6 - Tiết 13,14', N'E201', 55),
('TQ101-L01', 24, 10, 9, N'Thứ 2 - Tiết 15,16', N'C201', 60),
('L101-L01', 25, 11, 17, N'Thứ 3 - Tiết 16,17,18', N'F101', 50),
('MKT101-L01', 26, 12, 23, N'Thứ 4 - Tiết 15,16', N'B401', 55),
('NNA101-L01', 27, 13, 8, N'Thứ 5 - Tiết 15,16,17', N'C401', 40),
('QTKD101-L01', 28, 14, 5, N'Thứ 6 - Tiết 15,16,17', N'B601', 53),
('KDL101-L01', 29, 15, 31, N'Thứ 2 - Tiết 18,19', N'H101', 58),
('XDDD101-L01', 30, 16, 32, N'Thứ 3 - Tiết 18,19,20', N'I101', 49),
('DT102-L01', 31, 17, 30, N'Thứ 4 - Tiết 18,19,20', N'J101', 60),
('TTTV101-L01', 32, 18, 1, N'Thứ 5 - Tiết 18,19', N'K101', 39),
('SPTD101-L01', 33, 19, 2, N'Thứ 6 - Tiết 18,19,20', N'L101', 45),
('NHKS101-L01', 34, 20, 3, N'Thứ 2 - Tiết 20,21', N'M101', 50);

INSERT INTO Users (Username, PasswordHash, Role, LinkedStudentID, LinkedLecturerID) VALUES
-- Admin
('admin', 'admin123', N'Quản trị', NULL, NULL),
('admin.system', 'admin456', N'Quản trị', NULL, NULL),
-- Giảng viên
('gv.hoa', 'gv123', N'Giảng viên', NULL, 1),
('gv.minh', 'gv123', N'Giảng viên', NULL, 2),
('gv.phuc', 'gv123', N'Giảng viên', NULL, 3),
('gv.lan', 'gv123', N'Giảng viên', NULL, 4),
('gv.duc', 'gv123', N'Giảng viên', NULL, 5),
('gv.mai', 'gv123', N'Giảng viên', NULL, 6),
('gv.tam', 'gv123', N'Giảng viên', NULL, 7),
('gv.sarah', 'gv123', N'Giảng viên', NULL, 8),
-- Sinh viên K2021
('sv.21cntt001', 'sv123', N'Sinh viên', 1, NULL),
('sv.21cntt002', 'sv123', N'Sinh viên', 2, NULL),
('sv.21qtkd001', 'sv123', N'Sinh viên', 3, NULL),
('sv.21nna001', 'sv123', N'Sinh viên', 4, NULL),
('sv.21kt001', 'sv123', N'Sinh viên', 5, NULL),
-- Sinh viên K2022
('sv.22cntt001', 'sv123', N'Sinh viên', 6, NULL),
('sv.22cntt002', 'sv123', N'Sinh viên', 7, NULL),
('sv.22attt001', 'sv123', N'Sinh viên', 8, NULL),
('sv.22qtkd001', 'sv123', N'Sinh viên', 9, NULL),
('sv.22nna001', 'sv123', N'Sinh viên', 10, NULL),
-- Sinh viên K2023
('sv.23cntt001', 'sv123', N'Sinh viên', 11, NULL),
('sv.23cntt002', 'sv123', N'Sinh viên', 12, NULL),
('sv.23qtkd001', 'sv123', N'Sinh viên', 13, NULL),
('sv.23ds001', 'sv123', N'Sinh viên', 14, NULL),
('sv.23todhh001', 'sv123', N'Sinh viên', 15, NULL),
-- Sinh viên K2024
('sv.24cntt001', 'sv123', N'Sinh viên', 16, NULL),
('sv.24cntt002', 'sv123', N'Sinh viên', 17, NULL),
('sv.24qtkd001', 'sv123', N'Sinh viên', 18, NULL),
('sv.24nna001', 'sv123', N'Sinh viên', 19, NULL),
('sv.24ds001', 'sv123', N'Sinh viên', 20, NULL),
-- Thêm một số user khác
('sv_mai', '123456', N'Sinh viên', 1, NULL),
('sv_duc', '123456', N'Sinh viên', 2, NULL),
('gv_hoa', '123456', N'Giảng viên', NULL, 1),
('sv_024', '123456', N'Sinh viên', 24, NULL),
('sv_025', '123456', N'Sinh viên', 25, NULL),
('gv_024', '123456', N'Giảng viên', NULL, 24),
('admin_2', 'admin123', N'Quản trị', NULL, NULL),
('admin_3', 'mod123', N'Quản trị', NULL, NULL);

INSERT INTO Enrollments (StudentID, SectionID, RegisterDate, Status) VALUES
-- Đăng ký với lịch KHÔNG trùng lặp
(1, 1, '2024-01-15', N'Đang học'),    -- Mai - CT101 (Thứ 2 - Tiết 1,2,3)
(1, 3, '2024-01-15', N'Đang học'),    -- Mai - CT201 (Thứ 3 - Tiết 1,2,3)
(2, 2, '2024-01-16', N'Đang học'),    -- Hùng - CT101-L02 (Thứ 2 - Tiết 4,5,6)
(2, 4, '2024-01-16', N'Đang học'),    -- Hùng - QT101 (Thứ 3 - Tiết 4,5,6)
(3, 5, '2024-01-17', N'Đang học'),    -- Anh - EN101-L01 (Thứ 4 - Tiết 1,2)
(4, 6, '2024-01-18', N'Đang học'),    -- Linh - EN101-L02 (Thứ 4 - Tiết 3,4)
(24, 7, '2024-01-19', N'Đang học'),   -- ID 24
(25, 8, '2024-01-20', N'Đang học'),   -- ID 25
(26, 9, '2024-01-21', N'Đang học'),   -- ID 26
(27, 10, '2024-01-22', N'Đang học'),  -- ID 27
(28, 11, '2024-01-23', N'Đang học'),  -- ID 28
(29, 12, '2024-01-24', N'Đang học'),  -- ID 29
(30, 13, '2024-01-25', N'Đang học'),  -- ID 30
(31, 14, '2024-01-26', N'Đang học'),  -- ID 31
(32, 15, '2024-01-27', N'Đang học'),  -- ID 32
(33, 16, '2024-01-28', N'Đang học');  -- ID 33

INSERT INTO Invoices (StudentID, TermID, TotalAmount, CreatedDate, DueDate, Status, IsPaid) VALUES
(1, 2, 1850000, '2024-01-15', '2024-02-15', N'Chưa thanh toán', 0),
(2, 2, 1770000, '2024-01-16', '2024-02-16', N'Chưa thanh toán', 0),
(3, 2, 800000, '2024-01-17', '2024-02-17', N'Chưa thanh toán', 0),
(24, 3, 1350000, '2024-01-19', '2024-02-19', N'Chưa thanh toán', 0),
(25, 4, 1260000, '2024-01-20', '2024-02-20', N'Chưa thanh toán', 0),
(26, 5, 1290000, '2024-01-21', '2024-02-21', N'Chưa thanh toán', 0),
(27, 6, 1440000, '2024-01-22', '2024-02-22', N'Chưa thanh toán', 0),
(28, 7, 2200000, '2024-01-23', '2024-02-23', N'Chưa thanh toán', 0),
(29, 8, 1200000, '2024-01-24', '2024-02-24', N'Chưa thanh toán', 0),
(30, 9, 1380000, '2024-01-25', '2024-02-25', N'Chưa thanh toán', 0);

INSERT INTO InvoiceDetails (InvoiceID, SectionID, Amount) VALUES
(1, 1, 1350000),  -- Mai - CT101
(1, 3, 1500000),  -- Mai - CT201
(2, 2, 1350000),  -- Hùng - CT101
(2, 4, 1260000),  -- Hùng - QT101
(3, 5, 800000),   -- Anh - EN101
(4, 7, 1350000),  -- ID 24
(5, 8, 1260000),  -- ID 25
(6, 9, 1290000),  -- ID 26
(7, 10, 1440000), -- ID 27
(8, 11, 2200000), -- ID 28
(9, 12, 1200000), -- ID 29
(10, 13, 1380000); -- ID 30

INSERT INTO Payments (InvoiceID, PaymentDate, AmountPaid, Method, Note) VALUES
(1, '2024-02-01', 925000, N'Chuyển khoản', N'Thanh toán 50%'),
(2, '2024-02-02', 1770000, N'Tiền mặt', N'Thanh toán đầy đủ'),
(3, '2024-02-03', 400000, N'Thẻ tín dụng', N'Thanh toán 50%'),
(4, '2024-02-04', 675000, N'Chuyển khoản', N'Thanh toán 50%'),
(5, '2024-02-05', 1260000, N'Tiền mặt', N'Thanh toán đầy đủ'),
(6, '2024-02-06', 500000, N'Thẻ tín dụng', N'Thanh toán một phần'),
(7, '2024-02-07', 1440000, N'Chuyển khoản', N'Thanh toán đầy đủ'),
(8, '2024-02-08', 1000000, N'Tiền mặt', N'Thanh toán một phần'),
(9, '2024-02-09', 1200000, N'Chuyển khoản', N'Thanh toán đầy đủ'),
(10, '2024-02-10', 690000, N'Thẻ tín dụng', N'Thanh toán 50%');

-- ==================================================================
-- TRIGGER TỰ ĐỘNG CẬP NHẬT HÓA ĐƠN (HOÀN CHỈNH)
-- ==================================================================
CREATE OR ALTER TRIGGER tr_AutoUpdateInvoice
ON Enrollments
AFTER INSERT, UPDATE, DELETE
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Tạo bảng tạm để lưu các StudentID và TermID cần cập nhật
    CREATE TABLE #StudentsToUpdate (
        StudentID INT,
        TermID INT
    );
    
    -- Thu thập thông tin từ các bản ghi được INSERT/UPDATE
    IF EXISTS(SELECT * FROM inserted)
    BEGIN
        INSERT INTO #StudentsToUpdate (StudentID, TermID)
        SELECT DISTINCT i.StudentID, cs.TermID
        FROM inserted i
        INNER JOIN ClassSections cs ON i.SectionID = cs.SectionID;
    END
    
    -- Thu thập thông tin từ các bản ghi được DELETE
    IF EXISTS(SELECT * FROM deleted)
    BEGIN
        INSERT INTO #StudentsToUpdate (StudentID, TermID)
        SELECT DISTINCT d.StudentID, cs.TermID
        FROM deleted d
        INNER JOIN ClassSections cs ON d.SectionID = cs.SectionID
        WHERE NOT EXISTS (
            SELECT 1 FROM #StudentsToUpdate stu 
            WHERE stu.StudentID = d.StudentID AND stu.TermID = cs.TermID
        );
    END
    
    -- Cập nhật hóa đơn cho từng StudentID, TermID
    DECLARE @StudentID INT, @TermID INT;
    DECLARE update_cursor CURSOR FOR
    SELECT DISTINCT StudentID, TermID FROM #StudentsToUpdate;
    
    OPEN update_cursor;
    FETCH NEXT FROM update_cursor INTO @StudentID, @TermID;
    
    WHILE @@FETCH_STATUS = 0
    BEGIN
        DECLARE @CalculatedAmount DECIMAL(12,2) = 0;
        DECLARE @InvoiceID INT = NULL;
        DECLARE @ExistingPaidAmount DECIMAL(12,2) = 0;
        DECLARE @OldAmount DECIMAL(12,2) = 0;
        
        -- Tính tổng học phí hiện tại từ các môn đã đăng ký (trạng thái hợp lệ)
        SELECT @CalculatedAmount = ISNULL(SUM(c.Credits * c.TuitionPerCredit), 0)
        FROM Enrollments e
        INNER JOIN ClassSections cs ON e.SectionID = cs.SectionID
        INNER JOIN Courses c ON cs.CourseID = c.CourseID
        WHERE e.StudentID = @StudentID 
            AND cs.TermID = @TermID
            AND e.Status IN (N'Đang học', N'Đã duyệt');
        
        -- Kiểm tra xem đã có hóa đơn chưa
        SELECT @InvoiceID = InvoiceID, @OldAmount = TotalAmount
        FROM Invoices 
        WHERE StudentID = @StudentID AND TermID = @TermID;
        
        -- Lấy tổng số tiền đã thanh toán (nếu có)
        IF @InvoiceID IS NOT NULL
        BEGIN
            SELECT @ExistingPaidAmount = ISNULL(SUM(AmountPaid), 0)
            FROM Payments 
            WHERE InvoiceID = @InvoiceID;
        END
        
        -- Xử lý theo tình huống
        IF @CalculatedAmount > 0
        BEGIN
            IF @InvoiceID IS NULL
            BEGIN
                -- Tạo hóa đơn mới
                INSERT INTO Invoices (StudentID, TermID, TotalAmount, CreatedDate, DueDate, Status, IsPaid)
                VALUES (@StudentID, @TermID, @CalculatedAmount, GETDATE(), 
                        DATEADD(MONTH, 1, GETDATE()),
                        CASE WHEN @CalculatedAmount <= @ExistingPaidAmount THEN N'Đã thanh toán' ELSE N'Chưa thanh toán' END,
                        CASE WHEN @CalculatedAmount <= @ExistingPaidAmount THEN 1 ELSE 0 END);
                
                SET @InvoiceID = SCOPE_IDENTITY();
                
                -- Log thay đổi
                INSERT INTO InvoiceChangeLogs (StudentID, TermID, OldAmount, NewAmount, Action, ChangedBy)
                VALUES (@StudentID, @TermID, 0, @CalculatedAmount, 'CREATE', 'SYSTEM_TRIGGER');
                
                -- Tạo chi tiết hóa đơn
                INSERT INTO InvoiceDetails (InvoiceID, SectionID, Amount)
                SELECT 
                    @InvoiceID,
                    cs.SectionID,
                    c.Credits * c.TuitionPerCredit
                FROM Enrollments e
                INNER JOIN ClassSections cs ON e.SectionID = cs.SectionID
                INNER JOIN Courses c ON cs.CourseID = c.CourseID
                WHERE e.StudentID = @StudentID 
                    AND cs.TermID = @TermID
                    AND e.Status IN (N'Đang học', N'Đã duyệt');
            END
            ELSE
            BEGIN
                -- Cập nhật hóa đơn hiện tại chỉ khi có thay đổi
                IF ABS(@CalculatedAmount - @OldAmount) > 0.01
                BEGIN
                    UPDATE Invoices 
                    SET TotalAmount = @CalculatedAmount,
                        Status = CASE 
                            WHEN @CalculatedAmount <= @ExistingPaidAmount THEN N'Đã thanh toán'
                            WHEN @ExistingPaidAmount > 0 THEN N'Thanh toán một phần'
                            ELSE N'Chưa thanh toán'
                        END,
                        IsPaid = CASE WHEN @CalculatedAmount <= @ExistingPaidAmount THEN 1 ELSE 0 END,
                        DueDate = CASE WHEN @CalculatedAmount > @ExistingPaidAmount THEN DATEADD(MONTH, 1, GETDATE()) ELSE DueDate END
                    WHERE InvoiceID = @InvoiceID;
                    
                    -- Log thay đổi
                    INSERT INTO InvoiceChangeLogs (StudentID, TermID, OldAmount, NewAmount, Action, ChangedBy)
                    VALUES (@StudentID, @TermID, @OldAmount, @CalculatedAmount, 'UPDATE', 'SYSTEM_TRIGGER');
                    
                    -- Xóa chi tiết cũ và tạo lại
                    DELETE FROM InvoiceDetails WHERE InvoiceID = @InvoiceID;
                    
                    -- Tạo chi tiết hóa đơn mới
                    INSERT INTO InvoiceDetails (InvoiceID, SectionID, Amount)
                    SELECT 
                        @InvoiceID,
                        cs.SectionID,
                        c.Credits * c.TuitionPerCredit
                    FROM Enrollments e
                    INNER JOIN ClassSections cs ON e.SectionID = cs.SectionID
                    INNER JOIN Courses c ON cs.CourseID = c.CourseID
                    WHERE e.StudentID = @StudentID 
                        AND cs.TermID = @TermID
                        AND e.Status IN (N'Đang học', N'Đã duyệt');
                END
            END
        END
        ELSE
        BEGIN
            -- Không còn môn học nào, xử lý hóa đơn
            IF @InvoiceID IS NOT NULL
            BEGIN
                IF @ExistingPaidAmount > 0
                BEGIN
                    -- Có tiền đã thanh toán, chỉ cập nhật amount về 0
                    UPDATE Invoices 
                    SET TotalAmount = 0, 
                        Status = N'Đã thanh toán', 
                        IsPaid = 1
                    WHERE InvoiceID = @InvoiceID;
                    
                    -- Log thay đổi
                    INSERT INTO InvoiceChangeLogs (StudentID, TermID, OldAmount, NewAmount, Action, ChangedBy)
                    VALUES (@StudentID, @TermID, @OldAmount, 0, 'UPDATE_TO_ZERO', 'SYSTEM_TRIGGER');
                    
                    -- Xóa chi tiết
                    DELETE FROM InvoiceDetails WHERE InvoiceID = @InvoiceID;
                END
                ELSE
                BEGIN
                    -- Log thay đổi trước khi xóa
                    INSERT INTO InvoiceChangeLogs (StudentID, TermID, OldAmount, NewAmount, Action, ChangedBy)
                    VALUES (@StudentID, @TermID, @OldAmount, 0, 'DELETE', 'SYSTEM_TRIGGER');
                    
                    -- Chưa có thanh toán, xóa hóa đơn hoàn toàn
                    DELETE FROM InvoiceDetails WHERE InvoiceID = @InvoiceID;
                    DELETE FROM Invoices WHERE InvoiceID = @InvoiceID;
                END
            END
        END
        
        FETCH NEXT FROM update_cursor INTO @StudentID, @TermID;
    END
    
    CLOSE update_cursor;
    DEALLOCATE update_cursor;
    DROP TABLE #StudentsToUpdate;
END;

-- ==================================================================
-- TRIGGER KIỂM TRA SỈ SỐ VÀ VALIDATE
-- ==================================================================
CREATE OR ALTER TRIGGER tr_ValidateEnrollment
ON Enrollments
INSTEAD OF INSERT
AS
BEGIN
    SET NOCOUNT ON;
    
    DECLARE @StudentID INT, @SectionID INT, @RegisterDate DATE, @Status NVARCHAR(50);
    DECLARE @MaxStudents INT, @CurrentStudents INT;
    DECLARE @CourseName NVARCHAR(100), @StudentName NVARCHAR(100);
    
    DECLARE enrollment_cursor CURSOR FOR
    SELECT StudentID, SectionID, ISNULL(RegisterDate, GETDATE()), ISNULL(Status, N'Đang học') FROM inserted;
    
    OPEN enrollment_cursor;
    FETCH NEXT FROM enrollment_cursor INTO @StudentID, @SectionID, @RegisterDate, @Status;
    
    WHILE @@FETCH_STATUS = 0
    BEGIN
        -- Lấy thông tin cho thông báo lỗi
        SELECT @CourseName = c.Name
        FROM ClassSections cs
        INNER JOIN Courses c ON cs.CourseID = c.CourseID
        WHERE cs.SectionID = @SectionID;
        
        SELECT @StudentName = FullName FROM Students WHERE StudentID = @StudentID;
        
        -- Kiểm tra đã đăng ký chưa
        IF EXISTS(SELECT 1 FROM Enrollments 
                  WHERE StudentID = @StudentID 
                    AND SectionID = @SectionID
                    AND Status IN (N'Đang học', N'Đã duyệt'))
        BEGIN
            DECLARE @ErrorMsg1 NVARCHAR(500) = N'Sinh viên "' + @StudentName + N'" đã đăng ký môn "' + @CourseName + N'" rồi!';
            RAISERROR(@ErrorMsg1, 16, 1);
            RETURN;
        END
        
        -- Lấy sĩ số tối đa
        SELECT @MaxStudents = MaxStudents 
        FROM ClassSections 
        WHERE SectionID = @SectionID;
        
        -- Đếm số sinh viên hiện tại
        SELECT @CurrentStudents = COUNT(*) 
        FROM Enrollments 
        WHERE SectionID = @SectionID 
            AND Status IN (N'Đang học', N'Đã duyệt');
        
        -- Kiểm tra sĩ số
        IF @CurrentStudents >= @MaxStudents
        BEGIN
            DECLARE @ErrorMsg2 NVARCHAR(500) = N'Lớp học phần "' + @CourseName + N'" đã đầy! (' + 
                CAST(@CurrentStudents AS NVARCHAR(10)) + N'/' + CAST(@MaxStudents AS NVARCHAR(10)) + N')';
            RAISERROR(@ErrorMsg2, 16, 1);
            RETURN;
        END
        
        -- Kiểm tra trùng lịch học
        IF EXISTS(
            SELECT 1 
            FROM Enrollments e1
            INNER JOIN ClassSections cs1 ON e1.SectionID = cs1.SectionID
            INNER JOIN ClassSections cs2 ON cs2.SectionID = @SectionID
            WHERE e1.StudentID = @StudentID
                AND e1.Status IN (N'Đang học', N'Đã duyệt')
                AND cs1.TermID = cs2.TermID
                AND cs1.Schedule = cs2.Schedule
        )
        BEGIN
            DECLARE @ErrorMsg3 NVARCHAR(500) = N'Sinh viên "' + @StudentName + N'" đã có lịch học trùng với môn "' + @CourseName + N'"!';
            RAISERROR(@ErrorMsg3, 16, 1);
            RETURN;
        END
        
        -- Thêm đăng ký hợp lệ
        INSERT INTO Enrollments (StudentID, SectionID, RegisterDate, Status)
        VALUES (@StudentID, @SectionID, @RegisterDate, @Status);
        
        FETCH NEXT FROM enrollment_cursor INTO @StudentID, @SectionID, @RegisterDate, @Status;
    END
    
    CLOSE enrollment_cursor;
    DEALLOCATE enrollment_cursor;
END;

-- ==================================================================
-- VIEW THÔNG TIN THANH TOÁN TỔNG QUAN
-- ==================================================================
CREATE OR ALTER VIEW vw_PaymentOverview AS
SELECT 
    s.StudentID,
    s.StudentCode,
    s.FullName as StudentName,
    t.TermID,
    t.Name as TermName,
    i.InvoiceID,
    i.TotalAmount,
    ISNULL(payments.PaidAmount, 0) as PaidAmount,
    i.TotalAmount - ISNULL(payments.PaidAmount, 0) as RemainingAmount,
    CASE 
        WHEN i.TotalAmount - ISNULL(payments.PaidAmount, 0) <= 0 THEN N'✅ Đã thanh toán'
        WHEN ISNULL(payments.PaidAmount, 0) > 0 THEN N'⚠️ Thanh toán một phần'
        ELSE N'❌ Chưa thanh toán'
    END as PaymentStatus,
    i.CreatedDate,
    i.Status,
    i.IsPaid,
    i.DueDate
FROM Students s
INNER JOIN Invoices i ON s.StudentID = i.StudentID
INNER JOIN AcademicTerms t ON i.TermID = t.TermID
LEFT JOIN (
    SELECT InvoiceID, SUM(AmountPaid) as PaidAmount
    FROM Payments
    GROUP BY InvoiceID
) payments ON i.InvoiceID = payments.InvoiceID;

-- ==================================================================
-- STORED PROCEDURES TÌM KIẾM
-- ==================================================================
CREATE OR ALTER PROC TKTTSinhVien @StudentCode VARCHAR(20)
AS
BEGIN
    SELECT StudentID, StudentCode, FullName, Gender, DateOfBirth, Email, Phone, Address, DeptID, AdmissionYear, Status
    FROM Students WHERE StudentCode = @StudentCode
END;

CREATE OR ALTER PROC TKTTSinhVien1 @FullName NVARCHAR(100)
AS
BEGIN
    SELECT StudentID, StudentCode, FullName, Gender, DateOfBirth, Email, Phone, Address, DeptID, AdmissionYear, Status
    FROM Students WHERE FullName = @FullName
END;

CREATE OR ALTER PROC TKTTSinhVien2 @DeptID INT
AS
BEGIN
    SELECT StudentID, StudentCode, FullName, Gender, DateOfBirth, Email, Phone, Address, DeptID, AdmissionYear, Status
    FROM Students WHERE DeptID = @DeptID
END;

CREATE OR ALTER PROC TKTTSinhVien3 @AdmissionYear INT
AS
BEGIN
    SELECT StudentID, StudentCode, FullName, Gender, DateOfBirth, Email, Phone, Address, DeptID, AdmissionYear, Status
    FROM Students WHERE AdmissionYear = @AdmissionYear
END;

CREATE OR ALTER PROC TKTTMonHoc @CourseCode VARCHAR(20)
AS
BEGIN
    SELECT CourseID, Code, Name, Credits, TuitionPerCredit, DeptID
    FROM Courses WHERE Code = @CourseCode
END;

CREATE OR ALTER PROC TKTTMonHoc1 @CourseName NVARCHAR(100)
AS
BEGIN
    SELECT CourseID, Code, Name, Credits, TuitionPerCredit, DeptID
    FROM Courses WHERE Name = @CourseName
END;

CREATE OR ALTER PROC TKTTMonHoc2 @DeptID INT
AS
BEGIN
    SELECT CourseID, Code, Name, Credits, TuitionPerCredit, DeptID
    FROM Courses WHERE DeptID = @DeptID
END;

-- ==================================================================
-- STORED PROCEDURES QUẢN LÝ DỮ LIỆU
-- ==================================================================
CREATE OR ALTER PROCEDURE ThemSinhVien
    @StudentCode VARCHAR(20),
    @FullName NVARCHAR(100),
    @Gender NVARCHAR(10),
    @DateOfBirth DATE,
    @Email VARCHAR(100),
    @Phone VARCHAR(15),
    @Address NVARCHAR(255),
    @DeptID INT,
    @AdmissionYear INT,
    @Status NVARCHAR(50) = N'Đang học'
AS
BEGIN
    INSERT INTO Students (StudentCode, FullName, Gender, DateOfBirth, Email, Phone, Address, DeptID, AdmissionYear, Status)
    VALUES (@StudentCode, @FullName, @Gender, @DateOfBirth, @Email, @Phone, @Address, @DeptID, @AdmissionYear, @Status)
END;

CREATE OR ALTER PROCEDURE SuaSinhVien
    @StudentID INT,
    @StudentCode VARCHAR(20),
    @FullName NVARCHAR(100),
    @Gender NVARCHAR(10),
    @DateOfBirth DATE,
    @Email VARCHAR(100),
    @Phone VARCHAR(15),
    @Address NVARCHAR(255),
    @DeptID INT,
    @AdmissionYear INT,
    @Status NVARCHAR(50)
AS
BEGIN
    UPDATE Students 
    SET StudentCode = @StudentCode, FullName = @FullName, Gender = @Gender,
        DateOfBirth = @DateOfBirth, Email = @Email, Phone = @Phone,
        Address = @Address, DeptID = @DeptID, AdmissionYear = @AdmissionYear, Status = @Status
    WHERE StudentID = @StudentID
END;

CREATE OR ALTER PROCEDURE XoaSinhVien
    @StudentID INT
AS
BEGIN
    DELETE FROM Students WHERE StudentID = @StudentID
END;

CREATE OR ALTER PROCEDURE ThemMonHoc
    @Code VARCHAR(20),
    @Name NVARCHAR(100),
    @Credits INT,
    @TuitionPerCredit DECIMAL(10,2),
    @DeptID INT
AS
BEGIN
    INSERT INTO Courses (Code, Name, Credits, TuitionPerCredit, DeptID)
    VALUES (@Code, @Name, @Credits, @TuitionPerCredit, @DeptID)
END

CREATE OR ALTER PROCEDURE SuaMonHoc
    @CourseID INT,
    @Code VARCHAR(20),
    @Name NVARCHAR(100),
    @Credits INT,
    @TuitionPerCredit DECIMAL(10,2),
    @DeptID INT
AS
BEGIN
    UPDATE Courses 
    SET Code = @Code, Name = @Name, Credits = @Credits,
        TuitionPerCredit = @TuitionPerCredit, DeptID = @DeptID
    WHERE CourseID = @CourseID
END

CREATE OR ALTER PROCEDURE XoaMonHoc
    @CourseID INT
AS
BEGIN
    DELETE FROM Courses WHERE CourseID = @CourseID
END

-- LOG HÓA ĐƠN
CREATE OR ALTER PROCEDURE ThemHoaDon
    @StudentID INT,
    @TermID INT,
    @TotalAmount DECIMAL(12,2),
    @IsPaid BIT = 0
AS
BEGIN
    INSERT INTO Invoices (StudentID, TermID, TotalAmount, IsPaid)
    VALUES (@StudentID, @TermID, @TotalAmount, @IsPaid)
END

CREATE OR ALTER PROCEDURE SuaHoaDon
    @InvoiceID INT,
    @StudentID INT,
    @TermID INT,
    @TotalAmount DECIMAL(12,2),
    @IsPaid BIT
AS
BEGIN
    UPDATE Invoices 
    SET StudentID = @StudentID, TermID = @TermID, 
        TotalAmount = @TotalAmount, IsPaid = @IsPaid
    WHERE InvoiceID = @InvoiceID
END

CREATE OR ALTER PROCEDURE XoaHoaDon
    @InvoiceID INT
AS
BEGIN
    DELETE FROM Invoices WHERE InvoiceID = @InvoiceID
END

-- XEM DỮ LIỆU
SELECT * FROM Departments;
SELECT * FROM Lecturers;
SELECT * FROM Students;
SELECT * FROM Courses;
SELECT * FROM ClassSections;
SELECT * FROM Enrollments;
SELECT * FROM Invoices;
SELECT * FROM Payments;
SELECT * FROM Users;
SELECT * FROM InvoiceChangeLogs;


-- ==================================================================
-- TEST DỮ LIỆU VÀ TRIGGER
-- ==================================================================

-- Test đăng ký sinh viên
PRINT N'=== TEST ĐĂNG KÝ SINH VIÊN ===';
INSERT INTO Enrollments (StudentID, SectionID, RegisterDate, Status)
VALUES
(1, 1, GETDATE(), N'Đang học'),  -- SV001 - CT101-L01
(1, 3, GETDATE(), N'Đang học'),  -- SV001 - CT201-L01
(2, 4, GETDATE(), N'Đang học'),  -- SV002 - QT101-L01
(3, 5, GETDATE(), N'Đang học');  -- SV003 - EN101-L01

-- Thêm 1 payment cho test
INSERT INTO Payments (InvoiceID, PaymentDate, AmountPaid, Method, Note)
SELECT TOP 1 InvoiceID, GETDATE(), 500000, N'Tiền mặt', N'Thanh toán một phần'
FROM Invoices WHERE StudentID = 1;

-- Kiểm tra kết quả
SELECT 'INVOICES' as TableName, * FROM Invoices;
SELECT 'INVOICE_DETAILS' as TableName, * FROM InvoiceDetails;
SELECT 'PAYMENTS' as TableName, * FROM Payments;
SELECT 'PAYMENT_OVERVIEW' as TableName, * FROM vw_PaymentOverview;
SELECT 'CHANGE_LOGS' as TableName, * FROM InvoiceChangeLogs;