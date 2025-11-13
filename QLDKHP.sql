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

-- CHÈN DỮ LIỆU MẪU
INSERT INTO Departments (Code, Name, Office) VALUES 
('CNTT', N'Công nghệ thông tin', N'Tòa A1-101'),
('QTKD', N'Quản trị kinh doanh', N'Tòa B2-202'),
('NNA', N'Ngôn ngữ Anh', N'Tòa C3-303');

INSERT INTO Lecturers (LecturerCode, FullName, Email, DeptID) VALUES
('GV001', N'Nguyễn Văn Hòa', 'hoa.nguyen@univ.edu.vn', 1),
('GV002', N'Trần Thị Hạnh', 'hanh.tran@univ.edu.vn', 2),
('GV003', N'Lê Minh Tâm', 'tam.le@univ.edu.vn', 3);

INSERT INTO Students (StudentCode, FullName, Gender, DateOfBirth, Email, Phone, Address, DeptID, AdmissionYear) VALUES
('SV001', N'Nguyễn Thị Mai', N'Nữ', '2003-04-12', 'mai.nguyen@stu.edu.vn', '0901234567', N'Hà Nội', 1, 2021),
('SV002', N'Phạm Minh Đức', N'Nam', '2002-11-30', 'duc.pham@stu.edu.vn', '0907654321', N'Hải Phòng', 2, 2020),
('SV003', N'Lê Ngọc Anh', N'Nữ', '2003-08-20', 'anh.le@stu.edu.vn', '0989123456', N'Đà Nẵng', 3, 2021);

INSERT INTO AcademicTerms (Code, Name, StartDate, EndDate, IsCurrent) VALUES
('HK231', N'Học kỳ 1 - Năm học 2023-2024', '2023-09-01', '2024-01-15', 0),
('HK232', N'Học kỳ 2 - Năm học 2023-2024', '2024-02-15', '2024-06-30', 1);

INSERT INTO Courses (Code, Name, Credits, TuitionPerCredit, DeptID) VALUES
('CT101', N'Lập trình cơ bản', 3, 450000, 1),
('CT201', N'Cơ sở dữ liệu', 3, 450000, 1),
('QT101', N'Nguyên lý quản trị', 3, 420000, 2),
('EN101', N'Tiếng Anh giao tiếp 1', 2, 400000, 3);

INSERT INTO ClassSections (SectionCode, CourseID, TermID, LecturerID, Schedule, Room, MaxStudents) VALUES
('CT101-L01', 1, 2, 1, N'Thứ 2 - Tiết 1,2,3', N'A1-201', 60),
('CT101-L02', 1, 2, 3, N'Thứ 4 - Tiết 4,5,6', N'A1-203', 60),
('CT201-L01', 2, 2, 1, N'Thứ 4 - Tiết 1,2,3', N'A1-202', 60),
('QT101-L01', 3, 2, 2, N'Thứ 3 - Tiết 2,3,4', N'B2-101', 60),
('EN101-L01', 4, 2, 3, N'Thứ 5 - Tiết 1,2', N'C3-201', 50),
('EN101-L02', 4, 2, 3, N'Thứ 6 - Tiết 3,4', N'C3-202', 50);

INSERT INTO Users (Username, PasswordHash, Role, LinkedStudentID, LinkedLecturerID) VALUES
('sv_mai', '123456', N'Sinh viên', 1, NULL),
('sv_duc', '123456', N'Sinh viên', 2, NULL),
('gv_hoa', '123456', N'Giảng viên', NULL, 1),
('admin', 'admin123', N'Quản trị', NULL, NULL);

-- THÊM 20 DỮ LIỆU VÀO BẢNG Departments
INSERT INTO Departments (Code, Name, Office) VALUES
('CNTT2', N'Công nghệ thông tin 2', N'Tòa A2-101'),
('KT',   N'Kế toán',                  N'Tòa B1-101'),
('TMDT', N'Thương mại điện tử',       N'Tòa D1-103'),
('SP',   N'Sư phạm',                  N'Tòa E3-205'),
('VL',   N'Vật lý',                   N'Tòa F2-102'),
('H',    N'Hóa học',                  N'Tòa G1-301'),
('S',    N'Sinh học',                 N'Tòa H2-201'),
('DS',   N'Dược sĩ',                  N'Tòa I2-207'),
('YL',   N'Y học lâm sàng',           N'Tòa J1-105'),
('TQ',   N'Tiếng Trung',              N'Tòa K3-309'),
('L',    N'Luật',                     N'Tòa L2-203'),
('MKT',  N'Marketing',                N'Tòa M1-401'),
('NNA2', N'Ngôn ngữ Anh 2',           N'Tòa C3-304'),
('QTKD2',N'Quản trị kinh doanh 2',    N'Tòa B2-203'),
('KDL',  N'Kinh doanh quốc tế',       N'Tòa N2-104'),
('XDDD', N'Xây dựng dân dụng',        N'Tòa O2-208'),
('DT',   N'Điện tử',                  N'Tòa P1-101'),
('TTTV', N'Thư viện',                 N'Tòa Q1-101'),
('SPTD', N'Sư phạm thể dục',          N'Tòa R2-107'),
('NHKS', N'Nhà hàng khách sạn',       N'Tòa S3-305');

-- THÊM 20 DỮ LIỆU VÀO BẢNG Lecturers (giả sử DeptID từ 1-20 tồn tại)
INSERT INTO Lecturers (LecturerCode, FullName, Email, DeptID) VALUES
('GV004', N'Trần Phúc Nguyên',     'nguyen.tran@univ.edu.vn',  4),
('GV005', N'Bùi Thanh Bình',       'binh.bui@univ.edu.vn',     5),
('GV006', N'Doãn Thị Lệ',         'le.doan@univ.edu.vn',      6),
('GV007', N'Tạ Minh Tùng',         'tung.ta@univ.edu.vn',      7),
('GV008', N'Ngô Đức Vinh',         'vinh.ngo@univ.edu.vn',     8),
('GV009', N'Hoàng Hồng Đăng',      'dang.hoang@univ.edu.vn',   9),
('GV010', N'Nguyễn Đình Tài',      'tai.nguyen@univ.edu.vn',  10),
('GV011', N'Lý Thùy Dương',        'duong.ly@univ.edu.vn',    11),
('GV012', N'Vũ Quang Huy',         'huy.vu@univ.edu.vn',      12),
('GV013', N'Lê Thị Thảo',          'thao.le@univ.edu.vn',     13),
('GV014', N'Phạm Hồng Sơn',        'son.pham@univ.edu.vn',    14),
('GV015', N'Huỳnh Văn Minh',       'minh.huynh@univ.edu.vn',  15),
('GV016', N'Đặng Thị Hoa',         'hoa.dang@univ.edu.vn',    16),
('GV017', N'Bùi Văn Thắng',        'thang.bui@univ.edu.vn',   17),
('GV018', N'Ngô Văn Đồng',         'dong.ngo@univ.edu.vn',    18),
('GV019', N'Trịnh Đức Anh',        'anh.trinh@univ.edu.vn',   19),
('GV020', N'Phạm Thùy My',         'my.pham@univ.edu.vn',     1),
('GV021', N'Dương Quang Đạt',      'dat.duong@univ.edu.vn',   2),
('GV022', N'Lê Minh Phát',         'phat.le@univ.edu.vn',     3),
('GV023', N'Nguyễn Thu Trang',     'trang.nguyen@univ.edu.vn',4);

-- THÊM 20 DỮ LIỆU VÀO BẢNG Students (DeptID từ 1-20)
INSERT INTO Students (StudentCode, FullName, Gender, DateOfBirth, Email, Phone, Address, DeptID, AdmissionYear) VALUES
('SV004', N'Vũ Hoàng Hà', N'Nam', '2001-05-20', 'ha.vu@stu.edu.vn', '0982000001', N'Hà Nội',     4, 2019),
('SV005', N'Lê Thu Huyền', N'Nữ', '2002-01-19', 'huyen.le@stu.edu.vn', '0982000002', N'Hải Dương', 5, 2020),
('SV006', N'Lê Hoàng Anh', N'Nam', '2003-03-18', 'anh.le@stu.edu.vn', '0982000003', N'Quảng Ninh', 6, 2021),
('SV007', N'Phan Lan Chi', N'Nữ', '2001-06-25', 'chi.phan@stu.edu.vn', '0982000004', N'Nam Định',    7, 2019),
('SV008', N'Đỗ Đức Mạnh', N'Nam', '2002-09-15', 'manh.do@stu.edu.vn', '0982000005', N'Bắc Giang',   8, 2020),
('SV009', N'Lý Thị Mai', N'Nữ', '2003-10-01', 'mai.ly@stu.edu.vn', '0982000006', N'Lào Cai',        9, 2021),
('SV010', N'Ngô Minh Quân', N'Nam', '2002-12-03', 'quan.ngo@stu.edu.vn', '0982000007', N'Bắc Ninh', 10, 2020),
('SV011', N'Dương Quang Dũng', N'Nam', '2001-02-14', 'dung.duong@stu.edu.vn', '0982000008', N'Hưng Yên', 11, 2019),
('SV012', N'Hoàng Văn Phúc', N'Nam', '2003-07-16', 'phuc.hoang@stu.edu.vn', '0982000009', N'Nghệ An', 12, 2021),
('SV013', N'Phan Thị Thu', N'Nữ', '2002-05-23', 'thu.phan@stu.edu.vn', '0982000010', N'Thanh Hóa', 13, 2020),
('SV014', N'Lưu Kiều Trang', N'Nữ', '2002-10-11', 'trang.luu@stu.edu.vn', '0982000011', N'Hà Tĩnh', 14, 2020),
('SV015', N'Lê Quang Vinh', N'Nam', '2003-08-17', 'vinh.le@stu.edu.vn', '0982000012', N'Quảng Trị', 15, 2021),
('SV016', N'Đặng Văn Bảo', N'Nam', '2001-03-09', 'bao.dang@stu.edu.vn', '0982000013', N'Bình Thuận', 16, 2019),
('SV017', N'Nguyễn Thị Kim', N'Nữ', '2001-10-21', 'kim.nguyen@stu.edu.vn', '0982000014', N'Bến Tre', 17, 2019),
('SV018', N'Doãn Xuân Phú', N'Nam', '2002-12-14', 'phu.doan@stu.edu.vn', '0982000015', N'Bạc Liêu', 18, 2020),
('SV019', N'Lý Xuân Hạnh', N'Nữ', '2003-06-22', 'hanh.ly@stu.edu.vn', '0982000016', N'An Giang', 19, 2021),
('SV020', N'Vũ Tiến Đạt', N'Nam', '2001-07-10', 'dat.vu@stu.edu.vn', '0982000017', N'Tây Ninh', 20, 2019),
('SV021', N'Phạm Kiều Oanh', N'Nữ', '2002-02-12', 'oanh.pham@stu.edu.vn', '0982000018', N'Đồng Tháp', 2, 2020),
('SV022', N'Ngô Tấn Minh', N'Nam', '2003-11-01', 'minh.ngo@stu.edu.vn', '0982000019', N'Quảng Nam', 3, 2021),
('SV023', N'Lê Thị Bích', N'Nữ', '2002-05-30', 'bich.le@stu.edu.vn', '0982000020', N'Sóc Trăng', 4, 2020);

-- THÊM 20 DỮ LIỆU VÀO BẢNG AcademicTerms
INSERT INTO AcademicTerms (Code, Name, StartDate, EndDate, IsCurrent) VALUES
('HK233', N'Học kỳ phụ 2023-2024', '2024-07-01', '2024-08-15', 0),
('HK241', N'Học kỳ 1 - Năm học 2024-2025', '2024-09-01', '2025-01-15', 1),
('HK242', N'Học kỳ 2 - Năm học 2024-2025', '2025-02-15', '2025-06-30', 0),
('HK243', N'Học kỳ phụ 2024-2025', '2025-07-01', '2025-08-15', 0),
('HK251', N'Học kỳ 1 - Năm học 2025-2026', '2025-09-01', '2026-01-15', 0),
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
('HK201', N'Học kỳ hè 2020',             '2020-06-15', '2020-08-01', 0);

-- THÊM 20 DỮ LIỆU VÀO BẢNG Courses (DeptID lấy từ 1-20)
INSERT INTO Courses (Code, Name, Credits, TuitionPerCredit, DeptID) VALUES
('CT102', N'Lập trình nâng cao',   3, 500000, 1),
('DT101', N'Điện tử cơ bản',       4, 520000, 2),
('KT102', N'Kế toán tài chính',    3, 410000, 3),
('SP101', N'Tâm lý học giáo dục',  2, 430000, 4),
('VL101', N'Vật lý đại cương',     3, 400000, 5),
('H101',  N'Hóa đại cương',        3, 410000, 6),
('S101',  N'Sinh học cơ bản',      2, 420000, 7),
('DS101', N'Dược lý đại cương',    4, 600000, 8),
('YL101', N'Lâm sàng cơ bản',      3, 700000, 9),
('TQ101', N'Tiếng Trung sơ cấp',   2, 400000,10),
('L101',  N'Luật đại cương',       3, 410000,11),
('MKT101',N'Marketing căn bản',    3, 430000,12),
('NNA101',N'Ngữ pháp tiếng Anh',   2, 400000,13),
('QTKD101',N'Nguyên lý QTKD',      3, 420000,14),
('KDL101',N'Kinh doanh quốc tế',   3, 600000,15),
('XDDD101',N'Kết cấu công trình',  4, 450000,16),
('DT102', N'Điện tử nâng cao',     3, 580000,17),
('TTTV101',N'Quản lý thư viện',    2, 350000,18),
('SPTD101',N'Giáo dục thể chất',   2, 340000,19),
('NHKS101',N'Quản trị NHKS',       3, 500000,20);

-- THÊM 20 DỮ LIỆU VÀO BẢNG ClassSections 
INSERT INTO ClassSections (SectionCode, CourseID, TermID, LecturerID, Schedule, Room, MaxStudents) VALUES
('CT102-L01', 5, 1, 4, N'Thứ 2 - Tiết 1,2,3', N'A1-301', 60),
('DT101-L01', 6, 2, 5, N'Thứ 3 - Tiết 2,3,4', N'B1-301', 60),
('KT102-L01', 7, 3, 6, N'Thứ 4 - Tiết 1,2,3', N'C1-301', 60),
('SP101-L01', 8, 4, 7, N'Thứ 5 - Tiết 1,2',   N'D1-301', 50),
('VL101-L01', 9, 5, 8, N'Thứ 6 - Tiết 3,4',   N'E1-301', 50),
('H101-L01', 10, 6, 9, N'Thứ 2 - Tiết 1,2,3', N'G1-302', 60),
('S101-L01', 11, 7, 10,N'Thứ 3 - Tiết 5,6,7', N'H2-101', 40),
('DS101-L01',12, 8, 11,N'Thứ 5 - Tiết 2,3',   N'I2-218', 60),
('YL101-L01',13, 9, 12,N'Thứ 2 - Tiết 1,2',   N'J1-102', 55),
('TQ101-L01',14,10, 13,N'Thứ 3 - Tiết 1,2',   N'K3-310', 60),
('L101-L01', 15,11, 14,N'Thứ 4 - Tiết 1,2,3', N'L2-204', 50),
('MKT101-L01',16,12,15,N'Thứ 5 - Tiết 3,4',   N'M1-402', 55),
('NNA101-L01',17,13,16,N'Thứ 2 - Tiết 1,2,3', N'C3-305', 40),
('QTKD101-L01',18,14,17,N'Thứ 3 - Tiết 2,3,4',N'B2-203', 53),
('KDL101-L01',19,15,18,N'Thứ 5 - Tiết 1,2',   N'N2-105', 58),
('XDDD101-L01',20,16,19,N'Thứ 6 - Tiết 3,4',  N'O2-209', 49),
('DT102-L01', 1,17, 20,N'Thứ 2 - Tiết 1,2,3', N'P1-102', 60),
('TTTV101-L01',2,18, 1, N'Thứ 3 - Tiết 5,6,7',N'Q1-102', 39),
('SPTD101-L01',3,19, 2, N'Thứ 4 - Tiết 1,2,3',N'R2-108', 45),
('NHKS101-L01',4,20, 3, N'Thứ 5 - Tiết 3,4',  N'S3-306', 50);

-- Thêm 20 bản ghi vào bảng Users (liên kết khớp với Students và Lecturers đã có)
-- Chạy đoạn này sau khi đã chạy phần tạo bảng và chèn dữ liệu mẫu trong QLDKHP.sql

INSERT INTO Users (Username, PasswordHash, Role, LinkedStudentID, LinkedLecturerID) VALUES
-- 15 tài khoản Sinh viên (liên kết với StudentID 3..17)
('sv_003','123456', N'Sinh viên', 3, NULL),
('sv_004','123456', N'Sinh viên', 4, NULL),
('sv_005','123456', N'Sinh viên', 5, NULL),
('sv_006','123456', N'Sinh viên', 6, NULL),
('sv_007','123456', N'Sinh viên', 7, NULL),
('sv_008','123456', N'Sinh viên', 8, NULL),
('sv_009','123456', N'Sinh viên', 9, NULL),
('sv_010','123456', N'Sinh viên', 10, NULL),
('sv_011','123456', N'Sinh viên', 11, NULL),
('sv_012','123456', N'Sinh viên', 12, NULL),
('sv_013','123456', N'Sinh viên', 13, NULL),
('sv_014','123456', N'Sinh viên', 14, NULL),
('sv_015','123456', N'Sinh viên', 15, NULL),
('sv_016','123456', N'Sinh viên', 16, NULL),
('sv_017','123456', N'Sinh viên', 17, NULL),

-- 5 tài khoản Giảng viên (liên kết với LecturerID 4..8)
('gv_004','123456', N'Giảng viên', NULL, 4),
('gv_005','123456', N'Giảng viên', NULL, 5),
('gv_006','123456', N'Giảng viên', NULL, 6),
('gv_007','123456', N'Giảng viên', NULL, 7),
('gv_008','123456', N'Giảng viên', NULL, 8);
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
PRINT N'=== KẾT QUẢ KIỂM TRA ===';
SELECT 'INVOICES' as TableName, * FROM Invoices;
SELECT 'INVOICE_DETAILS' as TableName, * FROM InvoiceDetails;
SELECT 'PAYMENTS' as TableName, * FROM Payments;
SELECT 'PAYMENT_OVERVIEW' as TableName, * FROM vw_PaymentOverview;
SELECT 'CHANGE_LOGS' as TableName, * FROM InvoiceChangeLogs;

PRINT N'=== TRIGGER HOẠT ĐỘNG THÀNH CÔNG ===';
