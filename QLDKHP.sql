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

-- THÊM 20 dòng dữ liệu cho bảng Departments
INSERT INTO Departments (Code, Name, Office) VALUES
('CNTT', N'Công nghệ thông tin', N'Tòa A1-101'),
('QTKD', N'Quản trị kinh doanh', N'Tòa B2-202'),
('NNA', N'Ngôn ngữ Anh', N'Tòa C3-303'),
('KT', N'Kế toán', N'A1-105'),
('TCKT', N'Tài chính-Kế toán', N'A1-106'),
('SP', N'Sư phạm', N'B2-204'),
('Luật', N'Luật', N'B2-205'),
('KHMT', N'Khoa học máy tính', N'A1-107'),
('MKT', N'Marketing', N'B2-206'),
('QTNH', N'Quản trị nhà hàng', N'B2-207'),
('QTKS', N'Quản trị khách sạn', N'B2-208'),
('GDTC', N'Giáo dục thể chất', N'C3-304'),
('ĐT', N'Điện tử', N'A1-108'),
('CNDL', N'Công nghệ điện lạnh', N'A1-109'),
('KNN', N'Kỹ năng nghề nghiệp', N'C3-305'),
('QLGD', N'Quản lý giáo dục', N'C3-306'),
('PTTH', N'Phát thanh truyền hình', N'B2-209'),
('TQ', N'Tiếng Trung', N'C3-307'),
('VH', N'Văn học', N'C3-308'),
('LS', N'Lịch sử', N'C3-309');

-- THÊM 20 dòng dữ liệu cho bảng Lecturers 
-- (DeptID từ 1 đến 5, điều chỉnh cho hợp lý với Departments đã thêm)
INSERT INTO Lecturers (LecturerCode, FullName, Email, DeptID) VALUES
('GV001', N'Nguyễn Văn Hòa', 'hoa.nguyen@univ.edu.vn', 1),
('GV002', N'Trần Thị Hạnh', 'hanh.tran@univ.edu.vn', 2),
('GV003', N'Lê Minh Tâm', 'tam.le@univ.edu.vn', 3),
('GV004', N'Phạm Thanh Hà', 'ha.pham@univ.edu.vn', 4),
('GV005', N'Vũ Văn An', 'an.vu@univ.edu.vn', 2),
('GV006', N'Phan Minh Tuấn', 'tuan.phan@univ.edu.vn', 1),
('GV007', N'Nguyễn Quốc Bình', 'binh.nguyen@univ.edu.vn', 3),
('GV008', N'Lê Công Dũng', 'dung.le@univ.edu.vn', 2),
('GV009', N'Bùi Thị Thu', 'thu.bui@univ.edu.vn', 4),
('GV010', N'Hồ Văn Sơn', 'son.ho@univ.edu.vn', 1),
('GV011', N'Đặng Minh Châu', 'chau.dang@univ.edu.vn', 2),
('GV012', N'Ngô Thị Hạnh', 'hanh.ngo@univ.edu.vn', 3),
('GV013', N'Hoàng Văn Lực', 'luc.hoang@univ.edu.vn', 4),
('GV014', N'Lý Thị Mai', 'mai.ly@univ.edu.vn', 1),
('GV015', N'Phạm Văn Thảo', 'thao.pham@univ.edu.vn', 3),
('GV016', N'Nguyễn Minh Tùng', 'tung.nguyen@univ.edu.vn', 2),
('GV017', N'Lê Quốc Tuấn', 'tuan.le@univ.edu.vn', 5),
('GV018', N'Bùi Thị Hoa', 'hoa.bui@univ.edu.vn', 5),
('GV019', N'Phan Quốc Vũ', 'vu.phan@univ.edu.vn', 3),
('GV020', N'Trịnh Thị Lan', 'lan.trinh@univ.edu.vn', 2);

-- THÊM 20 dòng dữ liệu cho bảng Majors 
-- (DeptID từ 1 đến 5)
INSERT INTO Majors (Code, Name, DeptID) VALUES
('CNTT01', N'Kỹ thuật phần mềm', 1),
('CNTT02', N'Hệ thống thông tin', 1),
('CNTT03', N'Mạng máy tính', 1),
('CNTT04', N'Trí tuệ nhân tạo', 1),
('QTKD01', N'Quản trị Marketing', 2),
('QTKD02', N'Quản trị Nhân sự', 2),
('QTKD03', N'Kinh doanh quốc tế', 2),
('QTKD04', N'Quản trị dự án', 2),
('NNA01', N'Tiếng Anh thương mại', 3),
('NNA02', N'Tiếng Anh du lịch', 3),
('NNA03', N'Ngôn ngữ Anh Biên phiên dịch', 3),
('NNA04', N'Giảng dạy tiếng Anh', 3),
('KT01', N'Kế toán doanh nghiệp', 4),
('KT02', N'Kế toán tài chính', 4),
('KT03', N'Kiểm toán', 4),
('KT04', N'Kế toán quản trị', 4),
('TCKT01', N'Tài chính doanh nghiệp', 5),
('TCKT02', N'Tài chính ngân hàng', 5),
('TCKT03', N'Tài chính công', 5),
('TCKT04', N'Đầu tư tài chính', 5);

-- Thêm 20 dòng vào bảng AcademicTerms
INSERT INTO AcademicTerms (Code, Name, StartDate, EndDate, IsCurrent) VALUES
('HK2021A', N'Học kỳ 1 - Năm học 2021-2022', '2021-09-01', '2022-01-15', 0),
('HK2021B', N'Học kỳ 2 - Năm học 2021-2022', '2022-02-10', '2022-07-01', 0),
('HK2022A', N'Học kỳ 1 - Năm học 2022-2023', '2022-09-01', '2023-01-15', 0),
('HK2022B', N'Học kỳ 2 - Năm học 2022-2023', '2023-02-10', '2023-07-01', 0),
('HK2023A', N'Học kỳ 1 - Năm học 2023-2024', '2023-09-01', '2024-01-15', 0),
('HK2023B', N'Học kỳ 2 - Năm học 2023-2024', '2024-02-10', '2024-07-01', 1),
('HK2024A', N'Học kỳ 1 - Năm học 2024-2025', '2024-09-01', '2025-01-15', 0),
('HK2024B', N'Học kỳ 2 - Năm học 2024-2025', '2025-02-10', '2025-07-01', 0),
('HE2022A', N'Học kỳ hè 2022', '2022-06-01', '2022-08-01', 0),
('HE2023A', N'Học kỳ hè 2023', '2023-06-01', '2023-08-01', 0),
('HE2024A', N'Học kỳ hè 2024', '2024-06-01', '2024-08-01', 0),
('HK2025A', N'Học kỳ 1 - Năm học 2025-2026', '2025-09-01', '2026-01-15', 0),
('HK2025B', N'Học kỳ 2 - Năm học 2025-2026', '2026-02-10', '2026-07-01', 0),
('HK2026A', N'Học kỳ 1 - Năm học 2026-2027', '2026-09-01', '2027-01-15', 0),
('HK2026B', N'Học kỳ 2 - Năm học 2026-2027', '2027-02-10', '2027-07-01', 0),
('HK2027A', N'Học kỳ 1 - Năm học 2027-2028', '2027-09-01', '2028-01-15', 0),
('HK2027B', N'Học kỳ 2 - Năm học 2027-2028', '2028-02-10', '2028-07-01', 0),
('HK2028A', N'Học kỳ 1 - Năm học 2028-2029', '2028-09-01', '2029-01-15', 0),
('HK2028B', N'Học kỳ 2 - Năm học 2028-2029', '2029-02-10', '2029-07-01', 0),
('HE2026A', N'Học kỳ hè 2026', '2026-06-01', '2026-08-01', 0);

-- Thêm 20 dòng vào bảng Courses (DeptID chọn các giá trị hợp lệ từ bảng Department, tuỳ thuộc số lượng DeptID đã tạo, giả sử từ 1 đến 5)
INSERT INTO Courses (Code, Name, Credits, TuitionPerCredit, DeptID) VALUES
('CT101', N'Lập trình cơ bản', 3, 450000, 1),
('CT102', N'Nhập môn CNTT', 2, 400000, 1),
('CT103', N'Toán rời rạc', 3, 420000, 1),
('CT104', N'Kỹ thuật lập trình', 3, 450000, 1),
('CT105', N'Mạng máy tính', 3, 460000, 1),
('QTKD101', N'Cơ sở quản trị', 3, 420000, 2),
('QTKD102', N'Quản trị tài chính', 3, 430000, 2),
('QTKD103', N'Quản trị nhân sự', 3, 420000, 2),
('QTKD104', N'Quản trị chiến lược', 3, 440000, 2),
('QTKD105', N'Marketing căn bản', 3, 420000, 2),
('EN101', N'Tiếng Anh giao tiếp 1', 2, 400000, 3),
('EN102', N'Tiếng Anh giao tiếp 2', 2, 400000, 3),
('EN103', N'Ngữ pháp tiếng Anh', 2, 400000, 3),
('EN104', N'Viết tiếng Anh', 2, 400000, 3),
('KT101', N'Kế toán căn bản', 3, 410000, 4),
('KT102', N'Kế toán tài chính', 3, 420000, 4),
('KT103', N'Kiểm toán', 3, 410000, 4),
('TCKT101', N'Tài chính căn bản', 2, 400000, 5),
('TCKT102', N'Tài chính ngân hàng', 3, 420000, 5),
('TCKT103', N'Tài chính doanh nghiệp', 3, 420000, 5);

-- Thêm 20 dòng vào bảng ClassSections (CourseID từ 1 đến 20, TermID từ 1 đến 20, LecturerID từ 1 đến 20)
INSERT INTO ClassSections (SectionCode, CourseID, TermID, LecturerID, Schedule, Room, MaxStudents) VALUES
('CT101-L01', 1, 1, 1, N'Thứ 2 - Tiết 1,2,3', N'A1-201', 60),
('CT102-L01', 2, 2, 2, N'Thứ 3 - Tiết 1,2,3', N'A1-202', 60),
('CT103-L01', 3, 3, 3, N'Thứ 4 - Tiết 1,2,3', N'A1-203', 60),
('CT104-L01', 4, 4, 4, N'Thứ 5 - Tiết 1,2,3', N'A1-204', 60),
('CT105-L01', 5, 5, 5, N'Thứ 6 - Tiết 1,2,3', N'A1-205', 60),
('QTKD101-L01', 6, 6, 6, N'Thứ 7 - Tiết 1,2,3', N'B2-201', 60),
('QTKD102-L01', 7, 7, 7, N'Thứ 2 - Tiết 4,5,6', N'B2-202', 60),
('QTKD103-L01', 8, 8, 8, N'Thứ 3 - Tiết 4,5,6', N'B2-203', 60),
('QTKD104-L01', 9, 9, 9, N'Thứ 4 - Tiết 4,5,6', N'B2-204', 60),
('QTKD105-L01', 10, 10, 10, N'Thứ 5 - Tiết 4,5,6', N'B2-205', 60),
('EN101-L01', 11, 11, 11, N'Thứ 2 - Tiết 7,8', N'C3-201', 50),
('EN102-L01', 12, 12, 12, N'Thứ 3 - Tiết 7,8', N'C3-202', 50),
('EN103-L01', 13, 13, 13, N'Thứ 4 - Tiết 7,8', N'C3-203', 50),
('EN104-L01', 14, 14, 14, N'Thứ 5 - Tiết 7,8', N'C3-204', 50),
('KT101-L01', 15, 15, 15, N'Thứ 6 - Tiết 7,8', N'A1-206', 60),
('KT102-L01', 16, 16, 16, N'Thứ 7 - Tiết 4,5,6', N'A1-207', 60),
('KT103-L01', 17, 17, 17, N'Thứ 3 - Tiết 1,2,3', N'A1-208', 60),
('TCKT101-L01', 18, 18, 18, N'Thứ 2 - Tiết 1,2,3', N'A1-209', 60),
('TCKT102-L01', 19, 19, 19, N'Thứ 4 - Tiết 4,5,6', N'A1-210', 60),
('TCKT103-L01', 20, 20, 20, N'Thứ 5 - Tiết 1,2,3', N'A1-211', 60);

-- Thêm 20 dòng vào bảng Users (liên kết student hoặc lecturer hoặc để null với role 'admin')
INSERT INTO Users (Username, PasswordHash, Role, LinkedStudentID, LinkedLecturerID) VALUES
('user001', 'pw001', N'Sinh viên', 1, NULL),
('user002', 'pw002', N'Sinh viên', 2, NULL),
('user003', 'pw003', N'Sinh viên', 3, NULL),
('user004', 'pw004', N'Sinh viên', 4, NULL),
('user005', 'pw005', N'Sinh viên', 5, NULL),
('user006', 'pw006', N'Sinh viên', 6, NULL),
('user007', 'pw007', N'Sinh viên', 7, NULL),
('user008', 'pw008', N'Sinh viên', 8, NULL),
('user009', 'pw009', N'Sinh viên', 9, NULL),
('user010', 'pw010', N'Sinh viên', 10, NULL),
('gv001', 'pw011', N'Giảng viên', NULL, 1),
('gv002', 'pw012', N'Giảng viên', NULL, 2),
('gv003', 'pw013', N'Giảng viên', NULL, 3),
('gv004', 'pw014', N'Giảng viên', NULL, 4),
('gv005', 'pw015', N'Giảng viên', NULL, 5),
('gv006', 'pw016', N'Giảng viên', NULL, 6),
('gv007', 'pw017', N'Giảng viên', NULL, 7),
('gv008', 'pw018', N'Giảng viên', NULL, 8),
('admin01', 'adminpw01', N'Quản trị', NULL, NULL),
('admin02', 'adminpw02', N'Quản trị', NULL, NULL);

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
